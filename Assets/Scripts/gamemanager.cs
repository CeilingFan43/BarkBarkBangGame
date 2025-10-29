using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public float maxTime = 240f;
    public float currentTime;    
    public Text timeText;

    public GameObject deathScreenUI; 
    public GameObject timesUpUI; 

    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    public GameObject buttonsScreen;





    // Start is called before the first frame update
    void Start()
    {
        StartGame();
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
        //deathScreenUI.SetActive(true);
        StartCoroutine(GameEnd());

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

    public IEnumerator GameEnd()
    {
        deathScreenUI.SetActive(true);

        //Screen 1
        screen1.SetActive(true);
        screen2.SetActive(false);
        screen3.SetActive(false);
        buttonsScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(4f);

        //screen 2
        screen1.SetActive(false);
        screen2.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);

        //screenn 3
        screen2.SetActive(false);
        screen3.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);

        //bring up buttons
        buttonsScreen.SetActive(true);


        yield break;

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        //
        //timesUpUI.SetActive(false);
        currentTime = maxTime;
    }
}
