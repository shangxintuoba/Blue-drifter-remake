using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        Debug.DrawLine(new Vector3(0,0,0),transform.position,Color.red);
    }

}
