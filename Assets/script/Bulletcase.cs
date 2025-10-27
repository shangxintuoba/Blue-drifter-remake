using UnityEngine;

public class Bulletcase : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody rb;

    void Awake()
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1, Random.Range(0.3f, 1f), 0)* speed* Random.Range(0.5f, 1f), ForceMode.Impulse);
    }


}
