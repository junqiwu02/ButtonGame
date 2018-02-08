using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCountDownScene : MonoBehaviour {


    public ButtonManager bm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
    public void toCountDown()
    {
        SceneManager.LoadScene(2);
    }
    public void toCountDownForNext()
    {
        CountDown.next = true;
        SceneManager.LoadScene(2);
    }
}
