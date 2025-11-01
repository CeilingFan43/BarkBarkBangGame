using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject creditsScreen;

     private bool lastCreditsState;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.MenuMusic();
        lastCreditsState = creditsScreen.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsScreen.activeSelf != lastCreditsState)
        {
            if (creditsScreen.activeSelf)
            {
                audioManager.CreditsMusic();
            }
            else
            {
                audioManager.MenuMusic();
            }

            lastCreditsState = creditsScreen.activeSelf;
        }
    }
}
