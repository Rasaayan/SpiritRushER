using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Vector3 trackTileOffset;
    public GameObject[] trackTileTemplates;
    GameObject[] trackTiles;

    public Transform player;

    public GameManager gameManagerObj;

    public string level;


    void Start()
    {
        StartCoroutine(SpawnGrassTilesCoroutine());
    }

    void Update()
    {
        trackTiles = GameObject.FindGameObjectsWithTag("GrassTile");
        foreach (GameObject tile in trackTiles)
        {
            if (tile.transform.position.z < player.transform.position.z - 80)
            {
                Destroy(tile);
            }
        }
    }

    void SpawnGrassTiles()
    {
        trackTileOffset.z = 70;       

        GameObject tile = Instantiate(trackTileTemplates[0]);

        trackTileTemplates[0].transform.position = new Vector3(trackTileOffset.x, trackTileOffset.y, player.position.z + trackTileOffset.z);
        tile.transform.position = trackTileTemplates[0].transform.position;
        tile.SetActive(true);

        StartCoroutine(SpawnGrassTilesCoroutine());
    }

    IEnumerator SpawnGrassTilesCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        SpawnGrassTiles();
    }
}
