using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bulletcase Bulletcaseprefab;
    public Transform Firepoint;
    public Transform Casepoint;

    public float MaxDistance = 20f;
    public LayerMask Mask;


    public void Fire()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(Firepoint.position, Firepoint.forward, out hitInfo, MaxDistance, Mask);
        if (hit)
        {

        }
        else
        {


        }


        Instantiate(Bulletcaseprefab, Casepoint.position, Quaternion.identity);
    }

}
