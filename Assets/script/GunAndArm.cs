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


    public int bulletnumber = 6;
    private int bulletmagnumber;

    public float bulletspeed = 300f;
    public float reloadtime = 1.5f;

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
        bulletmagnumber = bulletnumber;
        Currentstate = State.Idle;


    }

    public void Update()
    {
        Aim();
        Reload();
        
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && Currentstate != State.Reloading)
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
        if(Input.GetMouseButton(1)&& Currentstate !=State.Reloading)
        {
            charger += Time.deltaTime *10f;
        }
        else
        {
            charger -= Time.deltaTime * 10f;
        }

        charger = Mathf.Clamp(charger, 0, 1);
        Animator.SetFloat("AimingTime", charger);
    }


    public void Charge()
    {

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
