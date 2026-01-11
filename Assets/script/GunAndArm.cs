using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GunAndArm : MonoBehaviour
{
    public Bulletcase Bulletcaseprefab;
    public Bullet Bulletprefab;
    
    public Transform Firepoint;
    public Transform Casepoint;


    public int bulletnumber = 12;
    private int currentbulletnumber;

    public float bulletspeed = 300f;
    public float shotrate = 0.5f;
    private float shotratetimer;

    //bulletcase count 
    public int BulletcaseNumber =0;

    //Fire effect
    public CinemachineImpulseSource Impulse;
    public ParticleSystem ParticleSystem;

    //Aim Charger
    private float charger = 0;

    public Animator Animator;

    //run animation lerp timer
    private float targetspeed =0;


    private bool isAiming;
    private bool isReloading;

    //Hitchcock
    private bool isPointingEnemy;
    RaycastHit hit;
    private float targetHitchcock;
    private float Hitchcockchangerate;
    public CinemachineImpulseSource trumble;


    //scanner
    public bool isScannerModeOn;

    private void Start()
    {
        currentbulletnumber = bulletnumber;

    }

    public void Update()
    {
        Aim();
        DetectEnemy();
        Reload();
        Fire();
        HandleRunningAnimation();
        ScannerModeToggle();
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentbulletnumber < bulletnumber)
        {
            Animator.SetBool("isReloading", true);
            currentbulletnumber = bulletnumber;
            isReloading =true;
        }
    }

    public void FinishReload()
    {
        Animator.SetBool("isReloading", false);
        isReloading = false;
    }

    public void Aim()
    {
        if(Input.GetMouseButton(1)&& !isReloading && !isScannerModeOn)
        {
            charger += Time.deltaTime *10f;

            isAiming = true;
        }
        else
        {
            charger -= Time.deltaTime * 10f;
            isAiming = false;
        }

        charger = Mathf.Clamp(charger, 0, 1);
        Animator.SetFloat("AimingTime", charger);
    }


    public void Fire()
    {
        if (Input.GetMouseButtonDown(0) && isAiming && !isReloading && currentbulletnumber>0 && shotratetimer <=0)
        { 
            //bulletcase
            Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);

            //bullet
            Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Firepoint.rotation).GetComponent<Rigidbody>();
            rb.linearVelocity = Firepoint.forward * bulletspeed;
            BulletcaseNumber++;
            currentbulletnumber--;
            Impulse.GenerateImpulse();
            ParticleSystem.Play();
            shotratetimer = shotrate;
        }
        shotratetimer -= Time.deltaTime;

    }

    public void HandleRunningAnimation()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetspeed = 2f;
            }
            else
            {
                targetspeed = 1f;
            }
        }
        else
        {
            targetspeed = 0f;
        }

        float current = Animator.GetFloat("RunningSpeed");
        Animator.SetFloat("RunningSpeed", Mathf.MoveTowards(current, targetspeed, 5f * Time.deltaTime));
    }

    public void DetectEnemy()
    {
        if (Input.GetMouseButton(1))
        {
            if(Physics.Raycast(Firepoint.position, Firepoint.forward, out hit, 200f) && hit.collider.CompareTag("Enemy"))
            {
                targetHitchcock = 1;
                Hitchcockchangerate = 0.2f;
              

            }
            else
            {
                targetHitchcock = 0f;
                Hitchcockchangerate = 0.8f;
            }
        }
        else
        {
            targetHitchcock = 0f;
            Hitchcockchangerate = 3f;
        }

        float current = Animator.GetFloat("HitchCock");
        Animator.SetFloat("HitchCock", Mathf.MoveTowards(current, targetHitchcock, Hitchcockchangerate * Time.deltaTime));
        if (current>=0.1f) trumble.GenerateImpulse();
    }


    void ScannerModeToggle()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            if (!isScannerModeOn)
            {
                isScannerModeOn = true;
            }
            else 
            {
                isScannerModeOn = false;
            }
        }
    }

}
