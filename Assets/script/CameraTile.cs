using UnityEngine;

public class CameraTile : MonoBehaviour
{
    private float CameraRotationAdjust;
    public float Normaltile = 3f;
    public float Fasttile = 5f;
    private Quaternion initialRotation;

    float tiltX;
    float tiltZ;
    private float rotatespeed;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CameraRotationAdjust = Fasttile;
                rotatespeed = 5f;
            }
            else
            {
                CameraRotationAdjust = Normaltile;
                rotatespeed = 50f;
            }

        }
            tiltX = Input.GetAxis("Vertical") * CameraRotationAdjust;
            tiltZ = -Input.GetAxis("Horizontal") * CameraRotationAdjust;

            Quaternion targetTilt = initialRotation * Quaternion.Euler(tiltX, 0f, tiltZ);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetTilt, rotatespeed * Time.deltaTime);
        
    }
}
