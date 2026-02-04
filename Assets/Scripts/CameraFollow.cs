using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(player.position.x, player.position.y - 1.4f, -10f);
    }
}