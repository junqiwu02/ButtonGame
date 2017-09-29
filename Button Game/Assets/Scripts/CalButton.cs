using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalButton : MonoBehaviour {
    
    public int buttonNum;
    public Calibrate cal;

    public void Touch()
    {
        // SetButton() returns true if it's successful
        bool successful = cal.SetButton(buttonNum);

        if (successful)
        {
            // buttons 0 and 1 set the scale
            if(buttonNum < 2)
            {
                cal.SetScale();
            }
            // buttons 2 and 3 set the center
            else
            {
                cal.SetCenter();
            }
            // disable the gameobject
            gameObject.SetActive(false);
        }
    }
}
