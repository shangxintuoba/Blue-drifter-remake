using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GunAndArm GunAndArm;
    public GameObject Player;
    public TextMeshProUGUI Name;

    public Animator EnemyAnimator;


    //Wandering
    private float WanderRange;


    //Chase and shot
    private float SearchRange;
    private float ShotRange;


    public enum State
    {
        Wandering,
        Chasing,
        Shooting
    }

    public State EnemyState = State.Wandering;

    public void Update()
    {
        switch (EnemyState)
        {
            case State.Wandering:
                Wander();
                break;

            case State.Chasing:
                Chase();
                break;

            case State.Shooting:
                Shoot();
                break;
        }

    }

    private void Wander()
    {
       //wandering around in a given range

    }

    private void Shoot()
    {

    }


    private void Chase()
    {


    }


    void EnemyStateCOntrol()
    {


    }


    void Showscannednames()
    {
        if (GunAndArm.isScannerModeOn)
        {
            //add a function here to detect if the players camera is pointing at the enemy's body 
            Name.gameObject.SetActive(true);
        }

    }

}
