using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CalibrationManager : MonoBehaviour {

    private StreamReader reader;

    private string path;
    private string editorPath = "/Resources/";
    private string buildPath = "";

    private Vector3 centerPos;
    private Vector3 planePos;
    private float scale;

    void Start ()
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
        path += "CalibrationProfile.csv";

        // read the calibration profile, if successful set the coordinates
        if(ReadFile())
        {
            transform.position += planePos - centerPos;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private bool ReadFile()
    {
        // if there isn't a calibration file, return false
        if(!File.Exists(path))
        {
            return false;
        }

        reader = new StreamReader(path);

        // read and set the vectors
        string[] line = reader.ReadLine().Split(',');
        centerPos = new Vector3(float.Parse(line[0]), float.Parse(line[1]), float.Parse(line[2]));

        line = reader.ReadLine().Split(',');
        planePos = new Vector3(float.Parse(line[0]), float.Parse(line[1]), float.Parse(line[2]));

        scale = float.Parse(reader.ReadLine());

        reader.Close();

        return true;
    }
}