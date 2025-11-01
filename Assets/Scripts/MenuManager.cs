using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject instructionsScreen;
    public GameObject creditsScreen;
    public AudioManager audioManager;
    



public void InstructionsScreen()
{
    instructionsScreen.SetActive(!instructionsScreen.activeSelf);
}

public void creditsScreenTurnOn()
{
    creditsScreen.SetActive(!creditsScreen.activeSelf);
}
}