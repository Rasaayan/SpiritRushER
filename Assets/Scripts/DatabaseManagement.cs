using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class DatabaseManagement : MonoBehaviour
{
    public InputField nameField;
    public Text highscoresText;
    public HudManager hudManagerObj;
    public ObstacleSpawning obstacleSpawningObj;

    public PlayerInfo playerInfo = new PlayerInfo();

    public static string name;
    public static int score;

    int checker = 0;
    
    public void Submit(string nameFromGameMan )
    {
        name = nameFromGameMan;
        score = obstacleSpawningObj.score;

        RestClient.Get<PlayerInfo>("https://spirit-rush.firebaseio.com/" + name + ".json").Then(onResolved: response =>
        {
            playerInfo = response;
        });

        UploadToDatabase();
    }

    public void Retrieve()
    {
        RetreiveFromdatabase(nameField.text);
    }
    
    public void UpdateScore()
    {
        hudManagerObj.SetHighScore(playerInfo.playerScore);
        highscoresText.text = "Your Friends Highscore  : " + playerInfo.playerScore;
    }

    public void UploadToDatabase()
    {
        PlayerInfo player = new PlayerInfo();
        RestClient.Put("https://spirit-rush.firebaseio.com/" + name + ".json", player);
    }

    public void RetreiveFromdatabase(string name)
    {
        RestClient.Get<PlayerInfo>("https://spirit-rush.firebaseio.com/" + name + ".json").Then( onResolved: response =>
        {
            playerInfo = response;
            UpdateScore();
        });        
    }


}
