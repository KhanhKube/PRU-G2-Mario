using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player; // Kéo Player vào đây
    public float yOffset = -10f; // Giữ Camera MiniMap phía trên Player

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, yOffset);
        }
    }
}
