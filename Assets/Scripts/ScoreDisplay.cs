using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public ObstacleSpawning scoreTrack;
    public Text score;

    void Update()
    {
        score.text = "Score : " + scoreTrack.score;
    }
}
