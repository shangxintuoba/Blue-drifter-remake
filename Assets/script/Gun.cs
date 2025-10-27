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
    public float ShotrateTimer;
    public int bulletnumber = 6;

    public float bulletspeed = 300f;



    //bulletcase count 
    public int BulletcaseNumber =0;
    

    private void Start()
    {
        ShotrateTimer = Shotrate;
    }

    public void Update()
    {
        if(ShotrateTimer>0) ShotrateTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && ShotrateTimer<=0)
        {
            ShotrateTimer = Shotrate;
            Fire();
        }
    }

    public void Fire()
    {
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletspeed;
        BulletcaseNumber++;

    }

    public void Reload()
    {
        

    }
}
