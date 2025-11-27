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
    //public float reloadtime = 1.5f;

    //bulletcase count 
    public int BulletcaseNumber =0;

    //cameara shake
    public CinemachineImpulseSource Impulse;

    //Aim Charger
    private float charger = 0;

    public Animator Animator;

    public enum State
    {
        Idle,
        Reloading,
        Aiming,
    }
    public State Currentstate;




    private void Start()
    {
        currentbulletnumber = bulletnumber;
        Currentstate = State.Idle;


    }

    public void Update()
    {
        Aim();
        Reload();
        Fire();
        HandleRunningAnimation();

    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && Currentstate != State.Reloading)
        {
            Animator.SetBool("isReloading", true);
            currentbulletnumber = bulletnumber;
            Currentstate = State.Reloading;
        }
    }

    public void FinishReload()
    {
        Animator.SetBool("isReloading", false);
        Currentstate = State.Idle;
    }

    public void Aim()
    {
        if(Input.GetMouseButton(1)&& Currentstate !=State.Reloading)
        {
            charger += Time.deltaTime *10f;
            Currentstate = State.Aiming;
        }
        else
        {
            charger -= Time.deltaTime * 10f;
            Currentstate = State.Idle;
        }

        charger = Mathf.Clamp(charger, 0, 1);
        Animator.SetFloat("AimingTime", charger);
    }


    public void Fire()
    {
        if (Input.GetMouseButtonDown(0) /*&& Currentstate == State.Aiming*/ && currentbulletnumber>0)
        { 
        //bulletcase
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);

        //bullet
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Firepoint.rotation).GetComponent<Rigidbody>();
        rb.linearVelocity = Firepoint.forward * bulletspeed;
        BulletcaseNumber++;
        currentbulletnumber--;
        Impulse.GenerateImpulse();
        }

    }

    public void ResetShooingbool()
    {
        Animator.SetBool("Shooting", false);
        Currentstate = State.Aiming;
        Animator.SetBool("isCharging", false);
    }

    public void HandleRunningAnimation()
    {


    }
}
