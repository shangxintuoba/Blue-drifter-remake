using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 90f; 
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
