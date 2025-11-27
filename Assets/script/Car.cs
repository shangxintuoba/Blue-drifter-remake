using StarterAssets;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cameratileroot;
    public Camera MainCamera;
    public Camera CarCamera;
    private MonoBehaviour firstPersonController;
    private MonoBehaviour CameraTile;

    private bool isDriving;
    private float Playerdistance;



    //driving stats
    public float maxspeed;
    public float acceleration;



    private void Start()
    {
         firstPersonController  = Player.GetComponent<FirstPersonController>();
        CameraTile = Cameratileroot.GetComponent<CameraTile>();
    }


    private void Update()
    {
        CalculatePlayerDistance();
        DrivingControl();


    }

    public void CalculatePlayerDistance()
    {






        if (Input.GetKeyDown(KeyCode.F) && Playerdistance <= 5f)
        {
           EnterCar();
        }
    }

    public void EnterCar()
    {
        firstPersonController.enabled = false;
        CameraTile.enabled = false;
    }


    public void DrivingControl()
    {




    }

    

}
