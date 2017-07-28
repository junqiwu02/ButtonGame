using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // done is an image that says "Done!"
    public GameObject done;
    public List<GameObject> buttons;

    private int markedButton;

    private string sequence;
    private int repetitions;

    private int step;
    private int currentRep;

    void Start()
    {
        // hide done
        done.SetActive(false);

        step = 0;
        currentRep = 0;

        sequence = MenuController.sequence;
        repetitions = MenuController.repetitions;

        // start the sequence
        MarkSeq();
    }

    void Update()
    {
        
    }

    public void MarkSeq()
    {
        // show done and switch scenes if all reps are done
        if (currentRep >= repetitions)
        {
            done.SetActive(true);
            SceneManager.LoadScene(0);
        }
        else
        {
            // convert string at index step of sequence to int and mark that button
            Int32.TryParse(sequence.Substring(step, 1), out markedButton);
            buttons[markedButton].GetComponent<ButtonController>().Mark();
        }

        // increment step if not at last step else increment rep
        if(step < sequence.Length - 1)
        {
            step++;
        }
        else
        {
            step = 0;
            currentRep++;
        }
    }

    /*public void MarkRand()
    {
        // alternates between changing the marked button to 0 and a random button
        if(markedButton == 0)
        {
            int rand = UnityEngine.Random.Range(1, buttons.Count);
            markedButton = rand;
            buttons[markedButton].GetComponent<ButtonController>().Mark();

        }
        else
        {
            markedButton = 0;
            buttons[markedButton].GetComponent<ButtonController>().Mark();
        }
    }*/
}
