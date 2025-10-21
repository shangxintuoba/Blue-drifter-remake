using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bulletcase Bulletcaseprefab;
    public Transform Firepoint;
    public Transform Casepoint;

    public float MaxDistance = 20f;
    public LayerMask ShootableObjects;
    public LineRenderer BulletTrail;


    private void Start()
    {
        //BulletTrail.enabled = false;
    }

    public void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Firepoint.position, Firepoint.forward, out hitInfo, MaxDistance, ShootableObjects);
            if (hit)
            {
                DrawBulletTrail(hitInfo.point);
            }
            else
            {
                DrawBulletTrail(Firepoint.position + Firepoint.forward * MaxDistance);

            }
            Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);
            //BulletTrail.enabled = true;

        }
        else
        {
            //BulletTrail.enabled = false;
        }
    }

    private void DrawBulletTrail(Vector3 hitpoint)
    {
        BulletTrail.SetPosition(0, hitpoint - Firepoint.forward * 20f);
        BulletTrail.SetPosition(1, hitpoint);
    }

}
