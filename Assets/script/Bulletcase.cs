using System.Collections;
using UnityEngine;

public class Bulletcase : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1, Random.Range(0.3f, 1f), 0) * speed * Random.Range(0.5f, 1f), ForceMode.Impulse);

        rb.AddTorque(new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)) * Random.Range(1f, 2f), ForceMode.Impulse);

        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}
