using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
    public delegate void Collected();
    public static event Collected OnCollected;    

    public PickUpSpawning pickupSpawningObj;
    public ObstacleSpawning obstacleSpawningObj;
    public HudManager hudManagerObj;
    public Attack attackObj;    
    public Movement movementObj;
    public Fade fadeObj;
    public Boss bossObj;

    void Start()
    {
    }

    void Update()
    {
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains("Light") )
        {
            AudioManager.Instance.PlayClip("PickUp");
            
            fadeObj.fadeDelay = 2f;
            fadeObj.fadePassDuration = 0.85f;

            attackObj.totalLight ++;
            hudManagerObj.IncreaseLight();
        }
        else if (gameObject.name.Contains("HourGlass") == true )
        {
            AudioManager.Instance.PlayClip("PickUp");
            bossObj.TakeDamage(20);
            movementObj.TimeWarp(1);
        }
        else if (gameObject.name.Contains("Ether") )
        {
            AudioManager.Instance.PlayClip("PickUp");
            hudManagerObj.IncreaseEther();
        }        

        if (other.tag == "Player")
        {
            if (OnCollected != null)
            {
                OnCollected();
            }
            Destroy(gameObject);
        }        
    }        

    public IEnumerator EtherMode()
    {
        yield return new WaitForSeconds(5);
        hudManagerObj.etherModeActive = false;
    }
}
