using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused = false;

    public GameObject pauseMenu;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
                AudioManager.Instance.PlayClip("Menu_Open");
            }
            else
            {
                Pause();
                AudioManager.Instance.PlayClip("Menu_Open");
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Credits()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        Time.timeScale = 1f;
    }
}
