using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    public LayerMask touchMask;
    private Vector2 touchPos = new Vector2(float.NaN, float.NaN);

    void Start () {
        // if not in debug mode, change camera culling to hide all debug objects
		if(!MenuController.isDebug)
        {
            GetComponent<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer("Debug"));
        }
	}
	
	void Update () {
        // execute for each touch input
        foreach (Touch touch in Input.touches)
        {
            // touchPos = touch.position;
            // raycast
            Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, touchMask))
            {
                // if hits, get the gameObject and run TouchDown() or TouchUp()
                GameObject recipient = hit.transform.gameObject;
                // set touchPos
                touchPos = new Vector2(hit.point.x, hit.point.y);
                if(recipient.GetComponent<ButtonController>() != null)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.GetComponent<ButtonController>().TouchDown();
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.GetComponent<ButtonController>().TouchUp();
                        // reset touchPos
                        touchPos = new Vector2(float.NaN, float.NaN);
                    }
                }
            }
        }
    }

    public Vector2 GetPos()
    {
        return touchPos;
    }
}
