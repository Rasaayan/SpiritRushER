using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
   

    public HudManager hudManagerObj;
    public GameManager gameManagerObj;
    public Boss bossObj;

    public float health = 1;
    public void Update()
    {
        health = gameManagerObj.round;
    }


    public void TakeDamage(float dmgAmount)
    {
        int ran = Random.Range(0, 2);

        if (ran == 1)
        {
            AudioManager.Instance.PlayClip("MobHurt_1");
        }
        else
        {
            AudioManager.Instance.PlayClip("MobHurt_2");
        }


        health -= dmgAmount;

        if (health <= 0)
        {
            hudManagerObj.increaseLives();
            Destroy(gameObject);
        }        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bossObj.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
