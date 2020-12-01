using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PickUpSpawning : MonoBehaviour
{
    public Vector3 pickUpOffset;
    public GameObject[] pickUpTemplates;
    GameObject[] pickups;    

    public Transform player;

    public GameManager gameManObj;

    public Fade fadeObj;
    
    // Setting the time parameters in which obstacles can spawn
    [Range(3f, 5f)]
    public float pickupmaxTime = 5f;
    [Range(2f, 3f)]
    public float pickupMinTime = 2f;

    public Scenery sceneryObj;

    public Boss bossObj;

    public bool hourglassMissed;

    int pickUpID;

    void Start()
    {
        StartCoroutine(SpawnPickupsCoroutine());
    }

    void Update()
    {
        pickups = GameObject.FindGameObjectsWithTag("Pickup");        

        foreach (GameObject pickup in pickups)
        {
            if (pickup.transform.position.z < player.transform.position.z - 10)
            {
                Destroy(pickup);
            }            
        }
    }

    void SpawnPickup()
    {
        if (gameManObj.currentGameLevel == 1)
        {
            pickUpID = 0;
        }
        if (gameManObj.currentGameLevel == 2)
        {
            pickUpID = 1;
        }
        if (gameManObj.currentGameLevel == 3)
        {
            pickUpID = 2;
        }

        if (gameManObj.bossActive == true)
        {
            pickUpOffset.x = Random.Range(-3, 3);
            pickUpOffset.y = 1.5f;
            pickUpOffset.z = Random.Range(60, 80);

            GameObject spawnedObject = Instantiate(pickUpTemplates[pickUpID]);

            pickUpTemplates[pickUpID].transform.position = new Vector3(pickUpOffset.x, pickUpOffset.y, player.position.z + pickUpOffset.z);
            spawnedObject.transform.position = pickUpTemplates[pickUpID].transform.position;
            spawnedObject.SetActive(true);

            
        }

        StartCoroutine(SpawnPickupsCoroutine());

    }

    IEnumerator SpawnPickupsCoroutine()
    {
        float pickupSpawnDelay = Random.Range(pickupMinTime, pickupmaxTime);
        yield return new WaitForSeconds(pickupSpawnDelay);
        SpawnPickup();
    }
}
