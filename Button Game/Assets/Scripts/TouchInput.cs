using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    public LayerMask touchMask;
    private Vector2 touchPos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // execute for each touch input
        foreach (Touch touch in Input.touches)
        {
            touchPos = touch.position;
            // raycast
            Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, touchMask))
            {
                // if hits, get the gameObject and run TouchDown() or TouchUp()
                GameObject recipient = hit.transform.gameObject;
                if (touch.phase == TouchPhase.Began)
                {
                    recipient.GetComponent<ButtonController>().TouchDown();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    recipient.GetComponent<ButtonController>().TouchUp();
                    // reset touchPos
                    touchPos = Vector2.zero;
                }
            }
        }
    }

    public Vector2 GetPos()
    {
        return touchPos;
    }
}
