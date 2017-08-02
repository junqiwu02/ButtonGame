using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

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
    // login panel
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

    private List<string> testNames;
    private StreamReader reader;

    private string path = "Assets/Resources/Tests";

    private void Start()
    {
        SetTests();
    }

    public void SetTests()
    {
        // get file names from directory info and add them to the list
        testNames = new List<string>() { "Custom" };
        DirectoryInfo d = new DirectoryInfo(path);
        FileInfo[] files = d.GetFiles("*.csv");
        foreach(FileInfo file in files)
        {
            testNames.Add(file.Name);
        }

        // add the list to the dropdown
        testSelector.ClearOptions();
        testSelector.AddOptions(testNames);
    }

    public void LoadMain()
    {
        // load main scene if a test has been selected
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
        // set button size and distance
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
        // if Custom test is not selected, set the path
        if(testSelector.value != 0)
        {
            pathField.text = path + "/" + testNames[testSelector.value];
        }
    }
}
