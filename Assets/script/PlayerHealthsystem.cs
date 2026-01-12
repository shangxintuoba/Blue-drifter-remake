using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthsystem : MonoBehaviour
{
    public float Maxhealth =100f;
    public float Health;
    public Image healthfilter;
    public Color healthycolor;
    public Color Deathcolor;

    public GameObject DeathScene;
    public Collider HitboxCollider;

    public Bullet Bullet;
    public float BulletDamage;

    private float Healthtimer;
    private float HealthGenerateinterval = 5f;
   

    private void Start()
    {   
        Health = Maxhealth; 
    }


    private void Update()
    {
        HandlerHealthfilter();
        HealthRegenerate();
        FallingToWater();
        DeathandRespawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(BulletDamage);

        }
    }

    void TakeDamage(float amount)
    {
        Health -= amount;
        Healthtimer = 0;
        Health = Mathf.Clamp(Health, 0f, Maxhealth);
    }


    void HandlerHealthfilter()
    {
        float healthLossPercent = 1f - (Health / Maxhealth);
        {
            healthfilter.color = Color.Lerp(healthycolor,Deathcolor,healthLossPercent);
        }

    }

    void HealthRegenerate()
    {
        Healthtimer += Time.deltaTime;

        if (Healthtimer >= HealthGenerateinterval )
        {
            Health += Time.deltaTime * 5f;
            Health = Mathf.Clamp(Health, 0f, Maxhealth);
        }
    }

    void FallingToWater()
    {
        if (transform.position.y <= -30f)            
        { 
            Health = 0;
            return;
        }

    }

    void DeathandRespawn()
    {
        if(Health <=0)
        {
          DeathScene.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
