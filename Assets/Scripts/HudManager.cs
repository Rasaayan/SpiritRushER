using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public int lives = 3;
    public int ether = 0;
    public int light = 0;

    public int enemiesKilled = 0;
    public bool etherModeReady = false;
    public bool etherModeActive = false;

    public GameObject lightPanel;
    public GameObject etherPanel;

    public Text livesText;
    public Text lightText;
    public Text etherText;
    public Text hudHighScore;

    public RawImage lightImage;
    public RawImage etherImage;    

    public GameManager gameManagerObj;

    public Pickups pickupsObj;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ActivateEther();
        }
        livesText.text = "Lives : " + lives;
        etherText.text = "Ether " + ether + "/5";
        lightText.text = "Light : " + light;
        LevelHuD();
    }

    public void increaseLives()
    {
        enemiesKilled++;
        if (enemiesKilled == 40)
        {
            lives++;
            enemiesKilled = 0;
        }
    }

    public void IncreaseEther()
    {       
        if (ether < 5 && etherModeActive == false)
        {
            ether++;
        }

        if (ether == 5)
        {
            etherModeReady = true;
        }
        else
        {
            etherModeReady = false;
        }
    }

    public void ActivateEther()
    {
        if (etherModeReady == true)
        {
            etherModeActive = true;
            StartCoroutine(pickupsObj.EtherMode());
        }
        ether = 0;
        etherModeReady = false;
    }

    public void SetHighScore(int highScore)
    {
        hudHighScore.text = "Your Highscore: " + highScore;
    }

    public void LevelHuD()
    {
        if (gameManagerObj.currentGameLevel == 1)
        {
            lightPanel.SetActive(true);
            etherPanel.SetActive(false);
        }       
        else if (gameManagerObj.currentGameLevel == 3)
        {
            etherPanel.SetActive(true);
            lightPanel.SetActive(false);
        }
    }

    public void IncreaseLight()
    {
        if (gameManagerObj.bossActive == true )
        {
            light++;
        }        
    }
}
