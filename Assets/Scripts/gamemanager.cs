using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public float maxTime = 240f;
    private float currentTime;    
    public Text timeText;

    public GameObject deathScreenUI; 
    public GameObject timesUpUI; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        deathScreenUI.SetActive(false);
        timesUpUI.SetActive(false);
        currentTime = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime-= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 0f)
        {
            timeEnded();
        }

    }

    public void PlayerDied()
    {
        Time.timeScale = 0f; // Optional: freeze time
        deathScreenUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void timeEnded()
    {
        Time.timeScale = 0f; // Optional: freeze time
        timesUpUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
