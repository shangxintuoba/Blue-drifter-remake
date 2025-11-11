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

    public float Shotrate = 1f;
    private float ShotrateTimer;
    public int bulletnumber = 6;
    private int bulletmagnumber;

    public float bulletspeed = 300f;
    public float reloadtime = 1.5f;



    //bulletcase count 
    public int BulletcaseNumber =0;


    //aim & Reload
    private Vector3 initialPosition;
    public Animator Animator;
    public float AimoffsetY;
    public float AimoffsetZ;
    private bool isReloading;

    //cameara shake
    public CinemachineImpulseSource Impulse;


    private void Start()
    {
        ShotrateTimer = Shotrate;
        bulletmagnumber = bulletnumber;
        initialPosition = transform.localPosition;

    }

    public void Update()
    {
        //check shotrate
        if(ShotrateTimer>0) ShotrateTimer -= Time.deltaTime;
        //fire
        if (Input.GetMouseButtonDown(0) && ShotrateTimer <= 0)
        {
            if (bulletmagnumber > 0) //bullet mag
            {
                ShotrateTimer = Shotrate;
                Fire();
                bulletmagnumber--;

            }
        }
        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }


        Aim();
       
    }

    public void Fire()
    {
        //bulletcase
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);


        //bullet
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletspeed;
        BulletcaseNumber++;
        Impulse.GenerateImpulse();

    }

    IEnumerator Reload()
    {
        isReloading = true;
        //reload animation
        yield return new WaitForSeconds(reloadtime);
        bulletmagnumber = bulletnumber;
        isReloading = false;
        Animator.SetBool("isReloading", false);
    }

    public void Aim()
    {
       Animator.SetBool("isAiming", (Input.GetMouseButton(1) && !isReloading));
       
    }
}
