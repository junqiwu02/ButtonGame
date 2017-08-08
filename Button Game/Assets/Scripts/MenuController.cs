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
    public GameObject blankFieldError;

    // options panel
    public InputField sizeField;
    public InputField distanceField;

    // test loader panel
    public Dropdown testSelector;
    public InputField pathField;
    private bool pathFieldCleared;
    
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

    public GameObject loginPanel;
    public GameObject mainPanel;

    private string path;
    private string editorPath = "/Resources/Tests";
    private string buildPath = "/Tests";

    private void Start()
    {
        // set path
        if (Application.isEditor)
        {
            path = Application.dataPath + editorPath;
        }
        else
        {
            path = Application.dataPath + buildPath;
        }

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
            blankFieldError.SetActive(false);

            username = nameField.text;

            // format
            username = username.Replace(" ", "_");
            username = username.ToLower();

            loginPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else
        {
            blankFieldError.SetActive(true);
        }
    }

    private void Update()
    {
        // if Custom test is not selected, set the path
        if(testSelector.value != 0)
        {
            pathField.transform.localPosition = new Vector3(10000f, 10000f, 0f);
            pathField.text = path + "/" + testNames[testSelector.value];
            pathFieldCleared = false;
        }
        else
        {
            if (!pathFieldCleared)
            {
                pathField.text = "";
                pathFieldCleared = true;
            }
            pathField.transform.localPosition = new Vector3(0f, -50f, 0f);
        }
    }

    public void Quit()
    {
        if(Application.isEditor)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
