using UnityEngine;

public class rainfollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    public float heightOffset = 30f;   // height above player
    public float followSpeed = 5f;     // smoothness

    void LateUpdate()
    {
        if (!player) return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y + heightOffset,
            player.position.z
        );

        // Smooth follow
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}
