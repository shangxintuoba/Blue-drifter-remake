using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;

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
       

    }

    private void Shoot()
    {

    }


    private void Chase()
    {


    }
}
