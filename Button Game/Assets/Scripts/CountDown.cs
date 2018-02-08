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

    public static bool toCount = false;
    public static bool next = false;

    void Start ()
    {
        startTime = Time.time;
        musicSource.clip = musicClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (toCount && Time.time - startTime > 1)
        {
            startTime += 1;
            timeToCount -= 1;
            numText.text = timeToCount+"";
        }
        if (toCount && timeToCount == 1 && play && Time.time - startTime >= 0.5)
        {
            playSound();
            setPlay(false);
        }
        if(toCount && timeToCount == 0)
        {
            startTime = double.MaxValue;
            if (!next)
                loadTestScene();
            else
            {
                loadTestSceneForNext();
                next = false;
            }
            timeToCount = 3;
            toCount = false;
        }
	}

    public void loadTestSceneForNext()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    public void loadTestScene()
    { 
        SceneManager.LoadScene(1);
    }
    public void playSound()
    {
        musicSource.Play();
    }
    public void setPlay(bool p)
    {
        play = p;
    }
}
