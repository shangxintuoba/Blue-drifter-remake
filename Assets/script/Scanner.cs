using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GunAndArm GunAndArm;
    public GameObject Filter;




    private void Update()
    {
        HandleFIlter();
    }

    void HandleFIlter()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Filter.SetActive(GunAndArm.isScannerModeOn);
        }
    }

}
