using System.Collections;
using TMPro;
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


    //Chase and shot
    private float SearchRange;
    private float ShotRange;

    [Header("health")]
    public float Maxhealth = 100f;
    public float Health;
    public float BulletDamage;
    float distanceToPlayer;

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
        Initialposition = transform.position;
    }

    public void Update()
    {
        switch (EnemyState)
        {
            case State.Wandering:
                Wander();
                break;
            case State.Alterted:
                Wander();
                break;

            case State.Chasing:
                Chase();
                break;

            case State.Shooting:
                Shoot();
                break;
        }
    }

    void EnemyStateControl()
    {
        distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        // Trigger alert 
        if (Health < 100 && EnemyState == State.Wandering)
        {
            EnemyState = State.Chasing;
            return;
        }

        // Enter shooting range
        if (distanceToPlayer <= ShotRange)
        {
            EnemyState = State.Shooting;
            return;
        }

        // Chasing player
        if (distanceToPlayer <= SearchRange && distanceToPlayer > ShotRange)
        {
            EnemyState = State.Chasing;
            return;
        }

        // Lost player and alerted
        if (distanceToPlayer > SearchRange)
        {
            EnemyState = State.Alterted;
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

    private void Shoot()
    {

    }
    private void Chase()
    {


    }


    //health
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(BulletDamage);
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
