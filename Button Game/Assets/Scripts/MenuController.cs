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

    public InputField nameField;

    // options panel
    public InputField sizeField;
    public InputField distanceField;

    // test loader panel
    public Dropdown testSelector;
    public InputField pathField;
    
    [HideInInspector]
    public static float buttonSize = 1.0f;
    [HideInInspector]
    public static float buttonDistance = 1.0f;
    [HideInInspector]
    public static bool testLoaded = false;
    [HideInInspector]
    public static string username = "new_user";

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

    public void SetName()
    {
        // set username
        if(nameField.text != "")
        {
            username = nameField.text;

            // format
            username = username.Replace(" ", "_");
            username = username.ToLower();
        }
    }

    private void Update()
    {
        // testSelector dropdown
       switch(testSelector.value)
        {
            case 1:
                // Day 1
                pathField.text = "C:\\Users\\Junqi\\Documents\\Git\\ButtonGame\\Button Game\\Assets\\Resources\\Day1.csv";
                break;
            default:
                // Custom
                break;
        }
    }
}
