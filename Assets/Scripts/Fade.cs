using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Instance { set; get; }

    public Image fadeImage;
    public bool isShowing;
    public bool inTransition;
    public float transition;
    public float fadeduration;
    public float fadePassDuration = 0.85f;
    public float fadeDelay = 3f;

    public PickUpSpawning pickupspawningObj;

    public GameManager gameManObj;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeActivate(bool showing, float duration)
    {
        isShowing = showing;
        inTransition = true;
        fadeduration = duration;
        transition = (isShowing) ? 0 : 1; // Goes form hiding 0 to showing 1
    }

    void Start()
    {
        StartCoroutine(ScreenFadeCoroutine());
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.L)) // Testing
        {
            FadeControl();
        }

        if (!inTransition)
        {
            return;
        }

        transition += (isShowing) ? Time.deltaTime * (1 / fadeduration) : -Time.deltaTime * (1 / fadeduration);
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0)
        {
            inTransition = false;
        }
    }

    public void FadeControl()
    {
        if (gameManObj.currentGameLevel == 1 && gameManObj.bossActive == true)
        {
            if (fadeDelay <= 1.5)
            {
                fadePassDuration = fadePassDuration + 0.2f;
                FadeActivate(true, fadePassDuration);
                FadeActivate(false, fadePassDuration);
            }
            else
            {
                FadeActivate(true, fadePassDuration);
                FadeActivate(false, fadePassDuration);
                fadeDelay = fadeDelay - 0.5f;
            }
        }        

        StartCoroutine(ScreenFadeCoroutine());
    }

    IEnumerator ScreenFadeCoroutine()
    {
        yield return new WaitForSeconds(fadeDelay);
        
        FadeControl();
    }
}
