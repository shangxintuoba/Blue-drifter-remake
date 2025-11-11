using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public AudioClip Gunshot;
    public AudioClip Reload;
    public AudioClip Walkingstep;
    public AudioClip Dash;


    public AudioClip Rain;
    public AudioClip BGM;

    public AudioSource BGMPlayer;
    public AudioSource RainPlayer;

    public static Audiomanager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }




}
