using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleCollisions : MonoBehaviour
{
    public Movement playerMovement; 
    public Rigidbody playerRb;
    public GameManager gameManagerObj;
    public Movement movementObj;
    public Pickups pickupsObj;
    public HudManager hudManagerObj;

    int timeWarpvalue = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle" && gameManagerObj.currentGameLevel == 2)
        {
            Debug.Log("hit time gate");
            AudioManager.Instance.PlayClip("Hurt_1");
            timeWarpvalue = 0;
            movementObj.TimeWarp(timeWarpvalue);
            Destroy(collision.collider.gameObject);
        }
        else if ((collision.collider.tag == "Obstacle"  && gameManagerObj.currentGameLevel != 2) || (collision.collider.tag == "Mob" && hudManagerObj.etherModeActive == false))
        {
            hudManagerObj.lives--;
            AudioManager.Instance.PlayClip("HeartBeat");
            if (hudManagerObj.lives <= 0)
            {
                playerMovement.enabled = false;
                FindObjectOfType<GameManager>().GameOver();
            }
            else
            {
                Destroy(collision.collider.gameObject);
            }
        }
    }    
}
