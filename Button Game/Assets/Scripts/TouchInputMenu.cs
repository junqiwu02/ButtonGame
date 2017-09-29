using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputMenu : MonoBehaviour {
    public LayerMask touchMask;

	void Update () {
        // execute for each touch input
        foreach (Touch touch in Input.touches)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, touchMask))
            {
                // if hits, get the gameObject and run TouchDown() or TouchUp()
                GameObject recipient = hit.transform.gameObject;
                recipient.GetComponent<CalButton>().Touch();
            }
        }
    }
}
