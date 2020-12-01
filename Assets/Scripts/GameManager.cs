using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ObstacleCollisions ObstacleCollisionsObj;
    public DistanceDisplay distanceObj;
    public Scenery sceneryObj;
    public BossSpawning bossSpawningObj;
    public GameObject newGameMenu;
    public Rigidbody player;
    public InputField nameText;
    public GameObject gameOverScreen;
    public Movement movementObj;
    public DatabaseManagement databaseManagementObj;
    public Text gameOverText;
    public GameObject nameErrorPanel;
    public ObstacleSpawning obstacleSpawningObj;

    string name = "";
    public float restartDelay = 4f;
    public bool gameOver = false;
    double distanceTracker = 0;
    double bossDefeatPoint;
    double nextLevelStartPoint = 100000000;
    double bossSpawnPoint = 500;
    public int currentGameLevel = 1;
    public bool bossActive = false;
    bool nextLevelActive = false;
    public int round = 1;
    public bool newGameBeingMade;
    public Text nameError;

    public void Start()
    {
        //DontDestroyOnLoad(gameObject);

        newGameMenu.SetActive(true); 
        Time.timeScale = 0f;
        newGameBeingMade = true;
        StartCoroutine(Theme1());
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            databaseManagementObj.Submit(name);
            gameOver = true;
            player.isKinematic = true;
            Time.timeScale = 0f;
            gameOverText.text = "Game Over !" +  "Your Score was: " + obstacleSpawningObj.score;            
            gameOverScreen.SetActive(true);
        }        
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.R) && newGameBeingMade == false && gameOver == false)
        {
            NewGame();
        }

        distanceTracker = Math.Round(distanceObj.distanceTravelled);

        if (distanceTracker == bossSpawnPoint && bossActive == false)
        {
            bossSpawningObj.SpawnBoss();
            bossActive = true;
        }


        if (distanceTracker == nextLevelStartPoint && nextLevelActive == false)
        {
            NextLevel();
        }        
    }

    public void NextLevel()
    {
        if (currentGameLevel == 1)
        {
            currentGameLevel = 2;
            StartCoroutine(sceneryObj.SpawnForrestTreesCoroutine(0.20f));
            Debug.Log("Level " + currentGameLevel + " now starting");
        }
        else if (currentGameLevel == 2)
        {
            currentGameLevel = 3;
            movementObj.TimeWarp(1);
            Debug.Log("Level " + currentGameLevel + " now starting");
        }
        else if (currentGameLevel == 3)
        {
            currentGameLevel = 1;
            StartCoroutine(sceneryObj.SpawnStreetLightsCoroutine());
            StartCoroutine(sceneryObj.SpawnHousesCoroutine());
            StartCoroutine(sceneryObj.SpawnTreesCoroutine(0.5f));
            Debug.Log("Level " + currentGameLevel + " now starting");
            round++;
        }
        nextLevelActive = true;
    }

    public void NewGame()
    {
        gameOverScreen.SetActive(false);
        newGameBeingMade = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             
    }

    public void BossDefeated()
    {
        nextLevelActive = false;

        bossDefeatPoint = distanceTracker;

        Debug.Log("Boss Defeat point : " + bossDefeatPoint);

        nextLevelStartPoint = 500 + bossDefeatPoint;
        Debug.Log("Next level starts at : " + nextLevelStartPoint);

        bossSpawnPoint = nextLevelStartPoint + 500;

        bossActive = false;
    }

    public void StartTheme1()
    {
        AudioManager.Instance.PlayClip("Track_1");
    }

    public void StartTheme2()
    {
        AudioManager.Instance.PlayClip("Track_2");
    }

    public void StartTheme3()
    {
        AudioManager.Instance.PlayClip("Track_3");
    }

    IEnumerator Theme1()
    {
        StartTheme1();
        yield return new WaitForSeconds(74);
        StartCoroutine(Theme2());
    }

    IEnumerator Theme2()
    {
        StartTheme2();
        yield return new WaitForSeconds(147);
        StartCoroutine(Theme3());
    }

    IEnumerator Theme3()
    {
        StartTheme3();
        yield return new WaitForSeconds(240);
        StartCoroutine(Theme1());
    }

    public void NewPlayer()
    {
        name = nameText.text;
        if (name != "")
        {
            databaseManagementObj.RetreiveFromdatabase(name);
            newGameMenu.SetActive(false);
            Time.timeScale = 1f;
            newGameBeingMade = false;
        }
        else
        {
            nameErrorPanel.SetActive(true);
        }
    }
}
