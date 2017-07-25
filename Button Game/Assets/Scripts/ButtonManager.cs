using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public List<GameObject> buttons;

    private int markedButton;

    void Start()
    {
        // mark button 0
        markedButton = 0;
        buttons[0].GetComponent<ButtonController>().Mark();
    }

    void Update()
    {
        // InputTouch();
    }

    public void ReMark()
    {
        // alternates between changing the marked button to 0 and a random button
        if(markedButton == 0)
        {
            int rand = Random.Range(1, buttons.Count);
            markedButton = rand;
            buttons[rand].GetComponent<ButtonController>().Mark();

        }
        else
        {
            markedButton = 0;
            buttons[0].GetComponent<ButtonController>().Mark();
        }
    }
}
