using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    /*// input field reference
    public InputField sequence_input;
    public InputField repetitions_input;

    [HideInInspector]
    public static string sequence = "0";
    [HideInInspector]
    public static int repetitions = 1;

    public void SetSequence()
    {
        // get the sequence and repetitions from the input field and set the static vars
        sequence = sequence_input.text;
        Int32.TryParse(repetitions_input.text, out repetitions);
        Debug.Log("Saved");
    }*/

    public InputField sizeField;
    public InputField distanceField;
    
    [HideInInspector]
    public static float buttonSize = 1.0f;
    [HideInInspector]
    public static float buttonDistance = 1.0f;
    [HideInInspector]
    public static bool testLoaded = false;

    public void LoadMain()
    {
        if(testLoaded)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogError("Must load a test first");
        }
    }

    public void SetScale()
    {
        if(sizeField.text != "")
        {
            buttonSize = float.Parse(sizeField.text);
        }

        if(distanceField.text != "")
        {
            buttonDistance = float.Parse(distanceField.text);
        }
    }
}
