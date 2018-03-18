using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // done is an image that says "Done!"
    public GameObject done;
    public GameObject delayPanel;
    public List<GameObject> buttons;
    public GameObject loading;

    public static bool isPaused;

    private int markedButton;

    // test information
    private string sequence;
    private int repetition;
    private bool hasDelay;
    private int numOfBlocks;

    // counters
    private int step;
    private int currentRep;
    private int currentBlock;

    void Start()
    {
        // hide done
        done.SetActive(false);

        step = 0;
        currentRep = 0;
        currentBlock = 0;

        sequence = TestLoader.sequences[currentBlock];
        repetition = TestLoader.repetitions[currentBlock];
        hasDelay = TestLoader.hasDelay;
        numOfBlocks = TestLoader.numOfBlocks;

        // start the sequence
        MarkSeq();
    }

    public void NextBlock()
    {
        // reset step and currentRep
        step = 0;
        currentRep = 0;
        // if currentBlock is greater than numOfBlocks, go to main menu
        if (currentBlock >= numOfBlocks - 1)
        {
            LoadMenu();
        }
        else
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            // hide done
            done.SetActive(false);
            // if the test has delays, open delayPanel and wait for button press
            if (hasDelay)
            {
                isPaused = true;
                delayPanel.SetActive(true);
                currentBlock++;
                sequence = TestLoader.sequences[currentBlock];
                repetition = TestLoader.repetitions[currentBlock];
            }
            // else, increment currentBlock, set new sequence and repetition, and mark next button
            else
            {
                currentBlock++;
                sequence = TestLoader.sequences[currentBlock];
                repetition = TestLoader.repetitions[currentBlock];
                MarkSeq();
            }
        }

    }

    public void MarkSeq()
    {
        isPaused = false;
        // show done and switch scenes if all reps are done
        if (currentRep >= repetition)
        {
            done.SetActive(true);
            Invoke("NextBlock", 1.0f);
        }
        else
        {
            // convert string at index step of sequence to int and mark that button
            Int32.TryParse(sequence.Substring(step, 1), out markedButton);
            buttons[markedButton].GetComponent<ButtonController>().Mark();

            // increment step if not at last step else increment rep
            if (step < (sequence.Length - 1))
            {
                step++;
            }
            else
            {
                step = 0;
                currentRep++;
            }
        }
    }

    public void LoadMenu()
    {
        loading.SetActive(true);
        SceneManager.LoadScene(0);
    }

    public GameObject GetButton()
    {
        return buttons[markedButton];
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
