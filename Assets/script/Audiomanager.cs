using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public AudioClip Gunshot;
    public AudioClip Reload;
    public AudioClip Walkingstep;
    public AudioClip Scaner;


    public AudioClip Rain;
    public AudioClip BGM;
    public AudioClip FightBGM;

    public AudioSource BGMPlayer;
    public AudioSource RainPlayer;
    public AudioSource SoundFXPlayer;
    public AudioSource WalkPlayer;

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


    public void PlayGunshot()
    {

        SoundFXPlayer.PlayOneShot(Gunshot);

    }
    public void PlayReload()
    {
        SoundFXPlayer.PlayOneShot(Reload);

    }
    public void PlayScaner()
    {
        if (!SoundFXPlayer.isPlaying)
        { 
            SoundFXPlayer.PlayOneShot(Scaner); 
        }
           
    }

    public void PlayBGMLoop(AudioClip bgmClip)
    {
        if (BGMPlayer.clip != bgmClip || !BGMPlayer.isPlaying)
        {
            BGMPlayer.clip = bgmClip;
            BGMPlayer.loop = true; 
            BGMPlayer.Play();
        }
    }

}
