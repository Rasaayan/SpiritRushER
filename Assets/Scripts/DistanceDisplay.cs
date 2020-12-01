using UnityEngine;
using UnityEngine.UI;

public class DistanceDisplay : MonoBehaviour
{
    public Transform player;
    public Text distance;
    public float distanceTravelled = 0;

    public float bossSpawnPoint;
    public float bossDefeatPoint;

    void Update()
    {
        distanceTravelled = player.position.z;
        distance.text = "Distance : " + distanceTravelled.ToString("0");
    }
}
