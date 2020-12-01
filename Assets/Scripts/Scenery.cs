using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenery : MonoBehaviour
{
    public Vector3 sceneryOffset;
    public GameObject[] sceneryAssetTemplates;
    GameObject[] sceneryAssets;

    public GameManager gameManObj;

    public Transform player;

    int housePicker = 1;
    int houseID = 0;

    int streetLightPicker = 1;
    int streetLightID = 0;

    int forrestTreePicker = 1;
    int forrestTreeID = 0;

    void Start()
    {
        StartCoroutine(SpawnStreetLightsCoroutine());
        StartCoroutine(SpawnHousesCoroutine());
        StartCoroutine(SpawnTreesCoroutine(0.5f));
    }

    void Update()
    {
        sceneryAssets = GameObject.FindGameObjectsWithTag("Scenery");
        foreach (GameObject asset in sceneryAssets)
        {
            if (asset.transform.position.z < player.transform.position.z - 10)
            {
                Destroy(asset);
            }
        }        
    }   

    void SpawnStreetLights()
    {
        if (gameManObj.currentGameLevel == 1 )
        {
            if (streetLightPicker % 2 == 0)
            {
                sceneryOffset.x = -3.1f;
                streetLightID = 0;
            }
            else
            {
                sceneryOffset.x = 3.15f;
                streetLightID = 1;
            }

            sceneryOffset.y = 4.9f;
            sceneryOffset.z = 80;

            GameObject spawnedObject = Instantiate(sceneryAssetTemplates[streetLightID]);

            sceneryAssetTemplates[streetLightID].transform.position = new Vector3(sceneryOffset.x, sceneryOffset.y, player.position.z + sceneryOffset.z);
            spawnedObject.transform.position = sceneryAssetTemplates[streetLightID].transform.position;
            spawnedObject.SetActive(true);

            streetLightPicker++;

            StartCoroutine(SpawnStreetLightsCoroutine());
        }
    }

    public IEnumerator SpawnStreetLightsCoroutine()
    {
        float streetLightSpawnDelay = 2.75f;
        yield return new WaitForSeconds(streetLightSpawnDelay);
        SpawnStreetLights();
    }

    void SpawnHouses()
    {
        if (gameManObj.currentGameLevel ==1) 
        {
            if (housePicker % 2 == 0)
            {
                sceneryOffset.x = -11;
                sceneryOffset.y = 3.8f;
                houseID = 2;
            }
            else
            {
                sceneryOffset.x = 11f;
                sceneryOffset.y = 3.15f;
                houseID = 3;
            }
            
            sceneryOffset.z = 90;

            GameObject spawnedObject = Instantiate(sceneryAssetTemplates[houseID]);

            sceneryAssetTemplates[houseID].transform.position = new Vector3(sceneryOffset.x, sceneryOffset.y, player.position.z + sceneryOffset.z);
            spawnedObject.transform.position = sceneryAssetTemplates[houseID].transform.position;
            spawnedObject.SetActive(true);

            housePicker++;

            StartCoroutine(SpawnHousesCoroutine());
        }
        
    }

    public IEnumerator SpawnHousesCoroutine()
    {
        float houseSpawnDelay = 3.25f;
        yield return new WaitForSeconds(houseSpawnDelay);
        SpawnHouses();
    }    

    void SpawnTrees()
    {
        if (gameManObj.currentGameLevel == 1)
        {
            int ranSide = Random.Range(0, 2);

            if (ranSide == 0)
            {
                sceneryOffset.x = Random.Range(-11.5f, -7.5f);
            }
            else
            {
                sceneryOffset.x = Random.Range(7.5f, 11.5f);
            }

            int treeID = Random.Range(4, 7);

            sceneryOffset.y = 3f;
            sceneryOffset.z = 70;

            GameObject spawnedObject = Instantiate(sceneryAssetTemplates[treeID]);

            sceneryAssetTemplates[treeID].transform.position = new Vector3(sceneryOffset.x, sceneryOffset.y, player.position.z + sceneryOffset.z);
            spawnedObject.transform.position = sceneryAssetTemplates[treeID].transform.position;
            spawnedObject.SetActive(true);

            StartCoroutine(SpawnTreesCoroutine(0.5f));
        }  
    }

    public IEnumerator SpawnTreesCoroutine(float treesToSpawn)
    {
        float treeSpawnDelay = treesToSpawn;
        yield return new WaitForSeconds(treeSpawnDelay);
        SpawnTrees();
    }

    void SpawnForrestTrees()
    {
        if (gameManObj.currentGameLevel == 2)
        {
            if (forrestTreePicker % 2 == 0)
            {
                forrestTreeID = 7;
                sceneryOffset.x = -6f;
            }
            else
            {
                forrestTreeID = 8;
                sceneryOffset.x = 6f;
            }

            sceneryOffset.y = 3.15f;
            sceneryOffset.z = 70;

            GameObject spawnedObject = Instantiate(sceneryAssetTemplates[forrestTreeID]);

            sceneryAssetTemplates[forrestTreeID].transform.position = new Vector3(sceneryOffset.x, sceneryOffset.y, player.position.z + sceneryOffset.z);
            spawnedObject.transform.position = sceneryAssetTemplates[forrestTreeID].transform.position;
            spawnedObject.SetActive(true);

            StartCoroutine(SpawnForrestTreesCoroutine(0.20f));

            forrestTreePicker++;
        }
    }

    public IEnumerator SpawnForrestTreesCoroutine(float treesToSpawn)
    {
        float treeSpawnDelay = treesToSpawn;
        yield return new WaitForSeconds(treeSpawnDelay);
        SpawnForrestTrees();
    }
}
