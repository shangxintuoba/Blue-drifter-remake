using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

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
    

    private void Start()
    {
        ShotrateTimer = Shotrate;
        bulletmagnumber =bulletnumber;
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
    }

    public void Fire()
    {
        //bulletcase
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);

        //bullet
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletspeed;
        BulletcaseNumber++;

    }

    IEnumerator Reload()
    {
        //reload animation
        yield return new WaitForSeconds(reloadtime);
        bulletmagnumber = bulletnumber;
    }
}
