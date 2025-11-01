using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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

    public GameObject PauseScreen;

    public GameObject player;

    private bool gameEnded = false;

    





    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime-= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 0f && !gameEnded)
        {
            gameEnded = true;
            timeEnded();
        }

    }

    public void PlayerDied()
    {
        Time.timeScale = 0f; // Optional: freeze time
        GameLose();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void timeEnded()
    {
        Time.timeScale = 0f; // Optional: freeze time
        StartCoroutine(GameWin());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public IEnumerator GameWin()
    {
        timesUpUI.SetActive(true);

        //Screen 1
        screen1.SetActive(true);
        screen2.SetActive(true);
        screen3.SetActive(false);
        buttonsScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(4f);

        //screen 2
        screen1.SetActive(false);
        yield return new WaitForSecondsRealtime(5f);

        //screenn 3
        screen3.SetActive(true);
        screen2.SetActive(false);

        yield return new WaitForSecondsRealtime(4f);

        //bring up buttons
        buttonsScreen.SetActive(true);


        yield break;

    }

    public void GameLose()
    {
        deathScreenUI.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        //
        //timesUpUI.SetActive(false);
        currentTime = maxTime;
    }

    public void Pause()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<playerAttack>().enabled = false;
        player.GetComponent<newPlayerMovement>().enabled = false;

    }

    public void UnPause()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<playerAttack>().enabled = true;
        player.GetComponent<newPlayerMovement>().enabled = true;
    }
}
