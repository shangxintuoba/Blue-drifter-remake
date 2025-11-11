using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject Player;
    public Camera MainCamera;
    public Camera CarCamera;
    private bool isPlayerDriving;
    public float maxspeed;
    public float acceleration;


    private void Start()
    {
        
    }


    public void EnterCar()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //MainCamera.enabled = false;
            //CarCamera.enabled = true;

        }
    }

    public void DrivingCOntrol()
    {

    }

    

}
