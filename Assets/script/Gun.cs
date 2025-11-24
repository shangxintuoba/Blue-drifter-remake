using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Gun : MonoBehaviour
{
    public Bulletcase Bulletcaseprefab;
    public Bullet Bulletprefab;
    
    public Transform Firepoint;
    public Transform Casepoint;


    public int bulletnumber = 6;
    private int bulletmagnumber;

    public float bulletspeed = 300f;
    public float reloadtime = 1.5f;

    //bulletcase count 
    public int BulletcaseNumber =0;

    //cameara shake
    public CinemachineImpulseSource Impulse;


    public Animator Animator;

    public enum State
    {
        Idle,
        Reloading,
        Aiming,
        Charging,
    }
    public State Currentstate;


    private void Start()
    {
        bulletmagnumber = bulletnumber;
        Currentstate = State.Idle;

    }

    public void Update()
    {
        Aim();
        Charge();
        Reload();
    }


    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && (Currentstate != State.Charging || Currentstate != State.Reloading))
        {
            Animator.SetBool("isReloading", true);
            bulletmagnumber = bulletnumber;
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
        if(Input.GetMouseButtonDown(1)&& Currentstate !=State.Reloading)
        {
            if (Currentstate == State.Aiming)
            {
                Currentstate = State.Idle;
                Animator.SetBool("isAiming", false);
                return;
            }            
            else
            {
                Currentstate = State.Aiming;
                Animator.SetBool("isAiming", true);
                return;
            }
        }
    }


    public void Charge()
    {

        if (Input.GetMouseButtonDown(0) && Currentstate == State.Aiming)
        {
            if (bulletmagnumber > 0) 
            {
                Animator.SetBool("isCharging", true);
                Currentstate = State.Charging;
            }
        }
        else if (Input.GetMouseButtonUp(0) && Currentstate == State.Charging)
        {
            if (bulletmagnumber > 0) 
            {               
                Fire();
                Animator.SetBool("Shooting", true);

            }
        }
    }

    public void Fire()
    {
        //bulletcase
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);

        //bullet
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletspeed;
        BulletcaseNumber++;
        bulletmagnumber--;
        Impulse.GenerateImpulse();

    }

    public void ResetShooingbool()
    {
        Animator.SetBool("Shooting", false);
        Currentstate = State.Aiming;
        Animator.SetBool("isCharging", false);
    }
}
