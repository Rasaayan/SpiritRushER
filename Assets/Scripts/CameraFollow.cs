using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;

    void Update()
    {
        transform.position = player.position + camOffset; // Takes the players position and increases the values by the camOffset so that the cam trails the player 
    }
} 
