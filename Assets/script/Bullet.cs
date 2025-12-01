using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject testcube;

    private void OnCollisionEnter(Collision collision)
    {

        //Instantiate(testcube, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);


    }

}
