using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public Enemy[] enemies;
    public enum State
    {
        Wandering,
        Fighting,
    }

    public State EnemyState = State.Wandering;

    void Start()
    {
        Audiomanager.Instance.PlayBGMLoop(Audiomanager.Instance.BGM);
    }

    private void Update()
    {
        



    }

}
