using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    public AudioSource musicSource;
    public AudioClip musicClip;
    public Text numText;


    public ButtonManager bManager;

    public int timeToCount = 3;
    
    private double startTime;

    private bool play = true;

    void Start ()
    {
        startTime = Time.time;
        musicSource.clip = musicClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > 1)
        {
            startTime += 1;
            timeToCount -= 1;
            numText.text = timeToCount+"";
        }
        if (timeToCount == 1 && play && Time.time - startTime >= 0.5)
        {
            playSound();
            play = false;
        }
        if(timeToCount == 0)
        {
            unloadSelf();
            timeToCount = 3;
        }
	}
    public void unloadSelf()
    {
        if (SceneManager.GetSceneByName("CountDown").isLoaded)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("CountDown"));
        }
        else
        {
            Debug.Log("Scene 2(CountDown) is never loaded, cannot unload");
        }
    }
    public void playSound()
    {
        musicSource.Play();
    }
}
