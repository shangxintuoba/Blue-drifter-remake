using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bulletcase Bulletcaseprefab;
    public Bullet Bulletprefab;
    
    public Transform Firepoint;
    public Transform Casepoint;

    public float Shotrate = 0.3f;
    public int bulletnumber = 6;

    public float bulletspeed = 300f;

    private void Start()
    {
        //BulletTrail.enabled = false;
    }

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("fire");
            Fire();
        }
    }

    public void Fire()
    {
        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);
        Rigidbody rb = Instantiate(Bulletprefab, Firepoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletspeed;
    }

    public void Reload()
    {


    }
}
