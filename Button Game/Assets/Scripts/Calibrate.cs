using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using System.IO;

public class Calibrate : MonoBehaviour {

    public FingerModel r_index;
    public GameObject calPanel;
    public GameObject mainPanel;
    public GameObject handController;

    public GameObject[] buttons;

    [HideInInspector]
    public static Vector3 centerPos;
    [HideInInspector]
    public static Vector3 planePos = new Vector3(0.0f, 0.0f, 0.7f);
    [HideInInspector]
    public static float scale;

    private Vector3[] bPos;

    private StreamWriter writer;

    private string path;
    private string editorPath = "/Resources/";
    private string buildPath = "/";

    private void Start()
    {
        // instantiate bPos array with the same number of elements as the buttons array
        bPos = new Vector3[buttons.Length];

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
    }

    public void SetScale()
    {
        bool allSet = false;
        // set allSet to true if button 0 and 1 have been pressed
        if(bPos[0] != Vector3.zero && bPos[1] != Vector3.zero)
        {
            allSet = true;
        }

        if(allSet)
        {
            // set the scale by dividing the distance between the buttons within the game by the real world distance
            scale = (buttons[0].transform.position.x - buttons[1].transform.position.x) / (bPos[0].x - bPos[1].x);

            // activate the next buttons to set the center
            buttons[2].SetActive(true);
            buttons[3].SetActive(true);

            // scale the menu hands so that the center is accurate
            handController.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void SetCenter()
    {
        bool allSet = false;
        // set allSet is button 2 and 3 have been pressed
        if (bPos[2] != Vector3.zero && bPos[3] != Vector3.zero)
        {
            allSet = true;
        }

        if (allSet)
        {
            // the center of the plane is the midpoint of the two buttons
            centerPos = (bPos[2] + bPos[3]) / 2;

            // write calibration profile
            WriteFile();

            // go back to the main panel
            calPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }

    public bool SetButton(int num)
    {
        // if a hand exists set the real world position of the button to the tip of the finger
        if (r_index != null)
        {
            bPos[num] = r_index.GetTipPosition();

            return true;
        }

        return false;
    }

    private void WriteFile()
    {
        // clear file
        File.WriteAllText(path, "");
        writer = new StreamWriter(path, true);

        // format and write the positions
        string centerStr = centerPos.ToString("F5").Replace("(", "").Replace(")", "").Replace(" ", "");
        writer.WriteLine(centerStr);
        string planeStr = planePos.ToString("F5").Replace("(", "").Replace(")", "").Replace(" ", "");
        writer.WriteLine(planeStr);
        writer.WriteLine(scale);

        writer.Close();
    }
}
