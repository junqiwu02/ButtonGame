using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLoader : MonoBehaviour {
    public InputField pathField;

    [HideInInspector]
    public static int numOfBlocks;
    [HideInInspector]
    public static bool hasDelay;
    [HideInInspector]
    public static string[] sequences;
    [HideInInspector]
    public static int[] repetitions;

    private StreamReader reader;

    public GameObject blankFieldError;

    public void LoadFile()
    {
        if(pathField.text != "")
        {
            blankFieldError.SetActive(false);

            // instantiate reader
            reader = new StreamReader(pathField.text);

            // get numOfBlocks and hasDelay from first line
            string[] data = reader.ReadLine().Split(',');
            Int32.TryParse(data[0], out numOfBlocks);
            Boolean.TryParse(data[1], out hasDelay);

            // create arrays based on numOfBlocks
            sequences = new string[numOfBlocks];
            repetitions = new int[numOfBlocks];

            // fill the arrays
            for (int i = 0; i < numOfBlocks; i++)
            {
                data = reader.ReadLine().Split(',');
                sequences[i] = data[0];
                Int32.TryParse(data[1], out repetitions[i]);
            }

            reader.Close();

            MenuController.testLoaded = true;
        }
        else
        {
            blankFieldError.SetActive(true);
        }
    }
}
