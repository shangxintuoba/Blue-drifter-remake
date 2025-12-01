using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Spark;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Instantiate(Spark, gameObject.transform.position, gameObject.transform.rotation);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            Instantiate(Spark, gameObject.transform.position, gameObject.transform.rotation);
        }

        Destroy(gameObject);
    }

}
