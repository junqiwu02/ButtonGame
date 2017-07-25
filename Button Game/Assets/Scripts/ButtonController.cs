using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public Material yellow;
    public Material red;

    private bool isMarked;

    // simulate touches with mouse
    private void OnMouseDown()
    {
        // TouchDown();
    }

    private void OnMouseUp()
    {
        // TouchUp();
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Mark()
    {
        GetComponent<Renderer>().material = red;
        isMarked = true;
    }

    public void TouchDown()
    {
        // animation
        transform.localScale = new Vector3(0.5f, 0.2f, 1f);
    }

    public void TouchUp()
    {
        // if isMarked, unmark and call ButtonManager
        if(isMarked)
        {
            isMarked = false;
            GetComponent<Renderer>().material = yellow;
            transform.localScale = new Vector3(0.5f, 0.4f, 1f);
            transform.parent.GetComponent<ButtonManager>().ReMark();
        }
        else
        {
            // animation
            transform.localScale = new Vector3(0.5f, 0.4f, 1f);
        }
    }
}
