using TMPro;
using UnityEngine;

public class EnemyCharacterUI : MonoBehaviour
{

    public Camera playerCamera;


    private void Update()
    {
       

    }

    void LateUpdate()
    {
        //facing player
        transform.LookAt(
           transform.position + playerCamera.transform.rotation * Vector3.forward,
           playerCamera.transform.rotation * Vector3.up);
    }


}
