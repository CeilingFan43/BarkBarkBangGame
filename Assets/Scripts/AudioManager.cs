using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    // Start is called before the first frame update
    public AudioClip ChaseMusic;
    private int enemiesChasing = 0;
    private bool isChaseMusicPlaying = false;
    void Start()
    {
    Debug.Log("AudioManager started. Initial enemiesChasing: " + enemiesChasing);
    }


public void StartChase()
{
    enemiesChasing++;
    Debug.Log("StartChase called, current enemies chasing " + enemiesChasing);

    if (!isChaseMusicPlaying && enemiesChasing >= 1)
    {
        Debug.Log("Chase music started.");
        musicSource.clip = ChaseMusic;
        musicSource.loop = true;
        musicSource.Play();
        isChaseMusicPlaying = true;
    }
}

public void EndChase()
{

    enemiesChasing = Mathf.Max(0, enemiesChasing - 1);

    if (enemiesChasing == 0)
    {
        Debug.Log("Chase music stopped.");
        musicSource.Stop();
        isChaseMusicPlaying = false;
    }
    else
    {
        Debug.Log("ENEMIES CHASING: " + enemiesChasing + " enemies still chasing.");
    }
}
}