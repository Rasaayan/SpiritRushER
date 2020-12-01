using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MobSpawning : MonoBehaviour
{
    BoxCollider collider;

    Light light;

    GameObject[] mobs;
    
    public GameObject[] mobTemplates;    

    public Vector3 mobOffset;

    public GameManager gameManObj;

    public Transform player;

    public int mobID = 0;

    public Scenery sceneryObj;

    [Range(1f, 1.75f)]
    public float mobMaxTime = 1.75f;

    [Range(0.5f, 0.75f)]
    public float mobMinTime = 0.5f;

    public BossSpawning bossSpawningObj;

    float mobSpawnDelay = 0;
    
    public Pickups pickupsObj;

    public HudManager hudManagerObj;

    void Start()
    {
        StartCoroutine(SpawnMobsCoroutine());
    }

    void Update()
    {
        mobs = GameObject.FindGameObjectsWithTag("Mob");

        foreach (GameObject mob in mobs)
        {
            if (mob.transform.position.z < player.transform.position.z - 10)
            {
                if (mob.name.Contains("Time"))
                {
                    bossSpawningObj.bossHp += 25;
                    if (bossSpawningObj.bossHp > 100)
                    {
                        bossSpawningObj.bossHp = 100;
                    }
                }
                Destroy(mob);
            }
        }        
    }

    void SpawnMob()
    {
        if (gameManObj.currentGameLevel == 1)
        {
            mobID = Random.Range(0,2);
            mobSpawnDelay = Random.Range(mobMinTime, mobMaxTime);
            mobOffset.y = 1.5f;
        }
        if (gameManObj.currentGameLevel == 2)
        {
            mobID = Random.Range(3,4);
            mobSpawnDelay = 1.5f;
            mobOffset.y = 1f;
        }
        if (gameManObj.currentGameLevel == 3)
        {
            mobSpawnDelay = Random.Range(mobMinTime, mobMaxTime);
            mobID = 5;
            mobOffset.y = 1f;
        }

        mobOffset.x = Random.Range(-3, 3);
        
        mobOffset.z = Random.Range(60, 80);

        GameObject spawnedObject = Instantiate(mobTemplates[mobID]);

        mobTemplates[mobID].transform.position = new Vector3(mobOffset.x, mobOffset.y, player.position.z + mobOffset.z);
        spawnedObject.transform.position = mobTemplates[mobID].transform.position;
        spawnedObject.SetActive(true);

        if (hudManagerObj.etherModeActive == true)
        {
            collider = spawnedObject.GetComponent<BoxCollider>();            
            this.collider.isTrigger = true;

            light = spawnedObject.GetComponent<Light>();
            this.light.enabled = true;

            foreach (GameObject mob in mobs)
            {
                collider = mob.GetComponent<BoxCollider>();
                this.collider.isTrigger = true;
            }
        }     

        StartCoroutine(SpawnMobsCoroutine());
    }

    IEnumerator SpawnMobsCoroutine()
    {        
        yield return new WaitForSeconds(mobSpawnDelay);
        SpawnMob();
    }
}
