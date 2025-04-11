using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource Ambiance;

    [Header("PLayer Audio")]
    public AudioClip PrismpickupAudio;
    public AudioClip dashAudio;
    public AudioClip jumpAudio;
    public AudioClip moveAudio;
    public AudioClip colorChangeAudio;
    public AudioClip deathAudio;
    public AudioClip winAudio;

    [Header("Level Audio")]
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip Level4;
    public AudioClip menuAudio;
    public AudioClip sceneChangeAudio;
    public AudioClip clickAudio;
    

    [Header("Puzzle Audio")]
    public AudioClip mirrorRotateAudio;
    public AudioClip lazerOnAudio;
    public AudioClip lazerOffAudio;

    [Header("Trap Audio")]
    public AudioClip axeSwingAudio;
    public AudioClip spikeAudio;
    public AudioClip spearAudio;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(this);

        // Ensure this script runs before any other audio script on scene load
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the scene name and play the correct music
        if (scene.name == "Level_0")
        {
            AudioManager.instance.PlayMusic(menuAudio);
        }
        else if(scene.name == "Level_TUT")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level1);
        }
        else if (scene.name == "Level_2")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level2);
        }
        else if (scene.name == "Level_3")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level3);
        }
        else if (scene.name == "Level_4")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level4);
        }
    }

    public void PlayClip(AudioClip Clip, bool random, float vol)
    {
        if (random)
        {
            RandomizeSound();
        }

        SFXSource.volume = vol;
        SFXSource.PlayOneShot(Clip);

    }

    private void RandomizeSound()
    {

        SFXSource.pitch = Random.Range(0.8f, 1.0f);
    }

    public void PlayMusic(AudioClip Clip)
    {
        musicSource.clip = Clip;
        musicSource.Play();

    }

    public void PlayUi(AudioClip Clip)
    {

        SFXSource.PlayOneShot(Clip);

    }


    public void playAmbiance()
    {
        //Ambiance.clip = Cricketambiance;

        if (SceneManager.GetActiveScene().buildIndex.CompareTo(2) == 0)
        {

            AudioManager.instance.Ambiance.Play();
        }

    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid errors if this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}