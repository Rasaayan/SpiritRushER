using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject creditsScreen;
    public GameObject startScreen;

    public void ShowCreditsScreen()
    {
        creditsScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    public void ShowStartScreen()
    {
        creditsScreen.SetActive(false);
        startScreen.SetActive(true);
    }
}
