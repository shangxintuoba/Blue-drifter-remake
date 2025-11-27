using UnityEngine;

public class CameraTile : MonoBehaviour
{
    private float CameraRotationAdjust;
    public float Normaltile = 3f;
    public float Fasttile = 5f;
    private Quaternion initialRotation;
    private float T = 0;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        
        T += 10f * Time.deltaTime;
        T = Mathf.Clamp01(T);



        if (Input.GetKey(KeyCode.LeftShift)) { CameraRotationAdjust =  Fasttile; } else { CameraRotationAdjust = Normaltile; }

        float tiltX = Input.GetAxis("Vertical") * CameraRotationAdjust * 0.8f;
        float tiltZ = -Input.GetAxis("Horizontal") * CameraRotationAdjust;


        Quaternion targetTilt = initialRotation * Quaternion.Euler(tiltX, 0f, tiltZ);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetTilt, T);

    }


}
