using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GunAndArm GunAndArm;
    public GameObject Player;
    public TextMeshProUGUI Name;

    public Animator EnemyAnimator;


    [Header("Wander")]
    public float WanderRange = 10f;
    public float WanderSpeed = 2f;
    private Vector3 wanderTarget;
    private Vector3 Initialposition;
    private bool hasWanderTarget = false;
    private Rigidbody rb;


    [Header("Chase and Shoot")]
    public float ShotRange =25f;
    public float ChasingRange =10f;

    [Header("health")]
    public float Maxhealth = 100f;
    public float Health;
    public float BulletDamage = 20f;
    float distanceToPlayer;
    public Collider HitboxCollider;
    public GameObject Blood;

    [Header("Fire")]
    public Transform Firepoint;
    public Bullet EnemyBullet;
    public float FireRate;
    private float Firetimer;
    public float Fireinterval =1;
    public GameObject FireModeArm;
    public GameObject LA;
    public GameObject RA;

    public enum State
    {
        Wandering,
        Chasing,
        Alterted,
        Shooting,
    }

    public State EnemyState = State.Wandering;

    private void Start()
    {
        rb =GetComponent<Rigidbody>();
        Health = Maxhealth;
        Initialposition = transform.position;
    }

    public void Update()
    {
        EnemyStateControl();

        switch (EnemyState)
        {
            case State.Wandering:
                Wander();
                break;
            case State.Alterted:
                Wander();
                break;
            case State.Shooting:
                Shoot();
                break;
        }

        HandleHealth();
    }

    void EnemyStateControl()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Wander > alert
        if ( EnemyState == State.Wandering && Health < 100)
        {
            EnemyState = State.Alterted;
            FireModeArm.SetActive(false);
            LA.SetActive(true);
            RA.SetActive(true);
            return;
        }

        // Shot> alerted
        if (EnemyState == State.Shooting && distanceToPlayer > ShotRange)
        {
            EnemyState = State.Alterted;
            FireModeArm.SetActive(false);
            LA.SetActive(true);
            RA.SetActive(true);
            return;
        }

        //alter > shoot
        if(EnemyState == State.Alterted && distanceToPlayer <= ShotRange)
        {
            EnemyState = State.Shooting;
            FireModeArm.SetActive(true);
            LA.SetActive(false);
            RA.SetActive(false);
            return;
        }
    }


    private void Wander()
    {
        // spawn destination
        if (!hasWanderTarget)
        {
            Vector3 randomOffset;
            Vector3 potentialTarget;
            float minDistance = 5f; 

            // Keep generating until it's far enough
            do
            {
                randomOffset = new Vector3(
                    Random.Range(-WanderRange, WanderRange),
                    0f,
                    Random.Range(-WanderRange, WanderRange)
                );
                potentialTarget = Initialposition + randomOffset;
            }
            while (Vector3.Distance(transform.position, potentialTarget) < minDistance);

            wanderTarget = potentialTarget;
            hasWanderTarget = true;
        }
        else 
        {
            //rotate
            Vector3 direction = wanderTarget - transform.position;
            direction.y = 0f;
            float rotationSpeed = 180f;

            if (direction.sqrMagnitude > 1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            //move
            if (Vector3.Distance(transform.position, wanderTarget) >= 1f)
            {
                rb.MovePosition(rb.position + direction.normalized * WanderSpeed * Time.deltaTime);
            }

            // Reached destination
            if (Vector3.Distance(transform.position, wanderTarget) < 1f)
            {
                StartCoroutine(WanderWait(2f));
            }
        }
    }

    private IEnumerator WanderWait (float waittime)
    {
        yield return new WaitForSeconds(waittime);
        hasWanderTarget = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 wallNormal = contact.normal;

            RegenerateWanderTargetOpposite(wallNormal);
        }
    }

    void RegenerateWanderTargetOpposite(Vector3 wallNormal)
    {
        // Push away from wall
        Vector3 oppositeDirection = Vector3.Reflect(transform.forward, wallNormal);
        oppositeDirection.y = 0f;

        float distance = Random.Range(5f, WanderRange);

        wanderTarget = transform.position + oppositeDirection.normalized * distance;
        wanderTarget.y = Initialposition.y;

        hasWanderTarget = true;

        StopAllCoroutines(); // stop waiting coroutine if any
    }


    private void Shoot()
    {
        //facing player
        Vector3 direction = Player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360f * Time.deltaTime
            );
        }

        Firetimer += Time.deltaTime;
        
        //shoot
        if (Firetimer >= Fireinterval)
        { 
            Rigidbody rb = Instantiate(EnemyBullet, Firepoint.position, Firepoint.rotation).GetComponent<Rigidbody>();
            Vector3 shootDirection = (Player.transform.position - Firepoint.position).normalized;
            rb.linearVelocity = shootDirection * 200f;
            Firetimer = 0;
        }

        //Chase
        if(distanceToPlayer <= ShotRange && distanceToPlayer >= ChasingRange)
        {
            Chase(Player.transform.position);
        }
    }

    private void Chase(Vector3 Target)
    {
        //rotate
        Vector3 direction = Target - transform.position;
        direction.y = 0f;
        float rotationSpeed = 180f;

        if (direction.sqrMagnitude > 1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //move
        if (Vector3.Distance(transform.position, wanderTarget) >= 1f)
        {
            rb.MovePosition(rb.position + direction.normalized * WanderSpeed * Time.deltaTime);
        }

    }

    void HandleHealth()
    {
        if (Health <= 0) 
        { Destroy(gameObject); }

    }


    //health
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("EnemyGetHit");
            TakeDamage(BulletDamage);
            Instantiate(Blood, other.transform.position, other.transform.rotation);
        }

    }
    void TakeDamage(float amount)
    {
        Health -= amount;
        Health = Mathf.Clamp(Health, 0f, Maxhealth);
    }
    //scan
    void Showscannednames()
    {
        if (GunAndArm.isScannerModeOn)
        {
            //add a function here to detect if the players camera is pointing at the enemy's body 
            Name.gameObject.SetActive(true);
        }

    }

}
