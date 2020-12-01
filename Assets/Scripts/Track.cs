using System.Collections;
using UnityEngine;

public class Track : MonoBehaviour
{
    public Vector3 trackTileOffset;
    public GameObject[] trackTileTemplates;
    GameObject[] trackTiles;

    public Transform player;

    public GameManager gameManagerObj;

    public string level;

    int tilePicker = 0;

    void Start()
    {
        StartCoroutine(SpawnTrackTilesCoroutine());
    }

    void Update()
    {
        trackTiles = GameObject.FindGameObjectsWithTag("TrackTile");
        foreach (GameObject tile in trackTiles)
        {
            if (tile.transform.position.z < player.transform.position.z - 60)
            {
                Destroy(tile);
            }
        }
    }

    void SpawnTrackTiles()
    {
        trackTileOffset.z = 100;

        tilePicker = gameManagerObj.currentGameLevel - 1;

        GameObject tile = Instantiate(trackTileTemplates[tilePicker]);

        trackTileTemplates[tilePicker].transform.position = new Vector3(trackTileOffset.x, trackTileOffset.y, player.position.z + trackTileOffset.z);
        tile.transform.position = trackTileTemplates[tilePicker].transform.position;
        tile.SetActive(true);

        StartCoroutine(SpawnTrackTilesCoroutine());
    }

    IEnumerator SpawnTrackTilesCoroutine()
    {
        float trackTileSpawnDelay= 1.75f;
        yield return new WaitForSeconds(trackTileSpawnDelay);
        SpawnTrackTiles();
    }    
}
