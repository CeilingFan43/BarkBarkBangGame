using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour

{
    public AudioManager Audio;
    public GameObject startUI;
    public CanvasGroup startUIOpacity;
    public float fadeScale = 1;

    private bool fadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        startUIOpacity.alpha = 1;
        StartGame();
        Debug.Log("game started");
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            startUIOpacity.alpha -= Time.deltaTime * fadeScale;
            if(startUIOpacity.alpha == 0)
            {
                fadeOut = false;
                startUI.SetActive(false);
            }

        
        }
    }
    

    public void StartGame()
    {
        startUI.SetActive(true);
        fadeOut = true;
        Audio.StartSound();
    }

}


