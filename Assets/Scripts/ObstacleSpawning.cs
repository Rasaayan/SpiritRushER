using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawning : MonoBehaviour
{
    BoxCollider collider;
    public Vector3 obstacleOffset;   
    public GameObject[] obstacleTemplates;    
    GameObject[] obstacles;
    

    public Transform player;

    public Scenery sceneryObj;

    public GameManager gameManObj;

    float obstacleSpawnDelay = 0;

    // Setting the time parameters in which obstacles can spawn
    [Range(0.50f, 0.1f)] 
    public float obstacleMaxTime = 1f;

    [Range(0.20f, 0.50f)] 
    public float obstacleMinTime = 0.20f;    

    public int score;
   
    int obstacleID = 0;

    public Pickups pickupsObj;

    void Start()
    {
        StartCoroutine(ObstacleSpawnCoroutine());
    }

    void Update()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if(obstacle.transform.position.z < player.transform.position.z - 10)
            {
                score++;
                Destroy(obstacle);
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            etherCount++;            
        } 
        */
    }

    void SpawnObstacle()
    {
        if (gameManObj.currentGameLevel == 1 )
        {
            obstacleID = Random.Range(0, 2);
                        
            obstacleSpawnDelay = Random.Range(obstacleMinTime, obstacleMaxTime);

            obstacleOffset.y = 0.2f;
            obstacleOffset.x = Random.Range(-4.5f, 1.7f);
        }
        else if (gameManObj.currentGameLevel == 2)
        {
            obstacleID = Random.Range(3, 5); 

            obstacleSpawnDelay = 1.5f;            

            if (obstacleID == 3)
            {
                obstacleOffset.y = 1f;
                obstacleOffset.x = -0.5f;
            }
            else if(obstacleID == 4)
            {
                obstacleOffset.y = 1.5f;
                obstacleOffset.x = Random.Range(-2.4f, 2.4f);
            }
            else
            {
                obstacleOffset.y = 2.5f;
                obstacleOffset.x = Random.Range(-2.4f, 2.4f);
            }
        }
        else if (gameManObj.currentGameLevel == 3)
        {
            obstacleID = Random.Range(6, 8);

            obstacleSpawnDelay = 1.25f;

            obstacleOffset.y = 0.5f;
            obstacleOffset.x = Random.Range(-2.7f, 2.7f);   
        }
            
        obstacleOffset.z = Random.Range(65, 70);
            
        GameObject spawnedObject = Instantiate(obstacleTemplates[obstacleID]);        

        obstacleTemplates[obstacleID].transform.position = new Vector3(obstacleOffset.x, obstacleOffset.y, player.position.z + obstacleOffset.z);
        spawnedObject.transform.position = obstacleTemplates[obstacleID].transform.position;
        spawnedObject.SetActive(true);


        StartCoroutine(ObstacleSpawnCoroutine());
    }    

    IEnumerator ObstacleSpawnCoroutine()
    {        
        yield return new WaitForSeconds(obstacleSpawnDelay);
        SpawnObstacle();
    }

}
