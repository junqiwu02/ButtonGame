using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    // input field reference
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
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }
}
