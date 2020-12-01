using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public GameObject howToPlayScreen;
    public GameObject startScreen;

    public void ShowHowToPlayScreen()
    {
        howToPlayScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    public void ShowStartScreen()
    {
        howToPlayScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
