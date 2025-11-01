using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    // Start is called before the first frame update
    public AudioClip ChaseMusic;
    public AudioClip GameBackgroundMusic;
    public AudioClip GameStartSound;
    public AudioClip MenuMusicSound;
    public AudioClip CreditsMusicSound;
    public AudioClip EndMusicSound;
    public AudioClip AmbientMusic;
    public AudioClip endChaseSound;

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

public IEnumerator EndChase()
{

    enemiesChasing = Mathf.Max(0, enemiesChasing - 1);

    if (enemiesChasing == 0)
    {
        Debug.Log("Chase music stopped.");
        musicSource.Stop();
        musicSource.clip = endChaseSound;
        musicSource.loop = true;
        musicSource.Play();

        yield return new WaitForSeconds(4f);
        isChaseMusicPlaying = false;

        musicSource.Stop();
        musicSource.clip = AmbientMusic;
        musicSource.loop = true;
        musicSource.Play();
        Debug.Log("Ambient attempted");


    }
    else
    {
        Debug.Log("ENEMIES CHASING: " + enemiesChasing + " enemies still chasing.");
    }
}

public void StartSound()
{
    soundSource.clip = GameStartSound;
    soundSource.Play();
}

public void MenuMusic()
{
    musicSource.clip = MenuMusicSound;
    musicSource.loop = true;
    musicSource.Play();
}

public void CreditsMusic()
{
    musicSource.clip = CreditsMusicSound;
    musicSource.loop = true;
    musicSource.Play();
}

public void EndMusic()
{
    musicSource.clip = EndMusicSound;
    musicSource.loop = true;
    musicSource.Play();
}

public void StartMusic()
{
    musicSource.clip = AmbientMusic;
    musicSource.loop = true;
    musicSource.Play();
}


}