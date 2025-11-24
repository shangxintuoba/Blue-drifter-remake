using UnityEngine;

public class CameraTile : MonoBehaviour
{
    private float CameraRotationAdjust;
    public float Normaltile = 5f;
    public float Fasttile = 7f;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift)) { CameraRotationAdjust = Mathf.Lerp(Normaltile, Fasttile,Time.deltaTime); } else { CameraRotationAdjust = Normaltile; }

        float tiltX = Input.GetAxis("Vertical") * CameraRotationAdjust;
        float tiltZ = -Input.GetAxis("Horizontal") * CameraRotationAdjust;


        Quaternion targetTilt = initialRotation * Quaternion.Euler(tiltX, 0f, tiltZ);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetTilt, 1f);

    }


}
