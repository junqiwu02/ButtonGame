using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leap;
using Leap.Unity;

public class FileManager : MonoBehaviour {

    public DrawGazePoint gazePlotter;
    public TouchInput touch;

    private HandModel lHand;
    private HandModel rHand;
    private FingerModel[] lFingers = new FingerModel[5];
    private FingerModel[] rFingers = new FingerModel[5];

    private StreamWriter writer;

    private string path;
    private string editorPath = "/Resources/";
    private string buildPath = "/User Data/";

    void Start () {
        // set path
        if(Application.isEditor)
        {
            path = Application.dataPath + editorPath;
        }
        else
        {
            path = Application.dataPath + buildPath;
        }

        // set path username and time
        path += MenuController.username + "_data_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
        // clear the text file
        File.WriteAllText(path, "");
        writer = new StreamWriter(path, true);
        // title
        writer.WriteLine("time,gaze,,touch,,l_palm,,,l_thumb,,,l_index,,,l_middle,,,l_ring,,,l_pinky,,,r_palm,,,r_thumb,,,r_index,,,r_middle,,,r_ring,,,r_pinky,,");
	}
	
	void Update () {
        // only write the time if the game is paused
        if(ButtonManager.isPaused)
        {
            writer.WriteLine(Time.time + ",paused");
        }
        else
        {
            writer.Write(Time.time + ",");
            string gazePos = gazePlotter.GetPos() + ",";
            // remove parentheses
            writer.Write(gazePos.Replace("(","").Replace(")",""));
            string touchPos = touch.GetPos() + ",";
            writer.Write(touchPos.Replace("(","").Replace(")",""));

            // write hand values if a hand exists
            if (lHand != null)
            {
                string palmPos = lHand.GetPalmPosition().ToString("F5") + ",";
                writer.Write(palmPos.Replace("(","").Replace(")",""));

                foreach (FingerModel lFinger in lFingers)
                {
                    //string type = lFinger.fingerType.ToString();
                    //type = "l" + type.Substring(4).ToLower();
                    string fingerPos = lFinger.GetTipPosition().ToString("F5") + ",";
                    writer.Write(fingerPos.Replace("(","").Replace(")",""));
                }
            }
            else
            {
                writer.Write(",,,,,,,,,,,,,,,,,,");
            }

            if (rHand != null)
            {
                string palmPos = rHand.GetPalmPosition().ToString("F5") + ",";
                writer.Write(palmPos.Replace("(", "").Replace(")", ""));

                int i = 0;
                foreach (FingerModel rFinger in rFingers)
                {
                    //string type = rFinger.fingerType.ToString();
                    //type = "r" + type.Substring(4).ToLower();
                    // counter so that last entry doesn't have an extra comma
                    string fingerPos;
                    if(i == 4)
                    {
                        fingerPos = rFinger.GetTipPosition().ToString("F5");
                    }
                    else
                    {
                        fingerPos = rFinger.GetTipPosition().ToString("F5") + ",";
                    }
                    writer.Write(fingerPos.Replace("(", "").Replace(")", ""));
                    i++;
                }
            }
            else
            {
                writer.Write(",,,,,,,,,,,,,,,,,");
            }
            writer.WriteLine();
        }
    }

    private void OnApplicationQuit()
    {
        writer.Close();
    }

    private void OnDisable()
    {
        writer.Close();
    }

    private void OnDestroy()
    {
        writer.Close();
    }

    // set the hand references
    public void SetHand(HandModel hand, Chirality handedness)
    {
        if(handedness == Chirality.Left)
        {
            lHand = hand;
            lFingers = hand.fingers;
        }
        else if(handedness == Chirality.Right)
        {
            rHand = hand;
            rFingers = hand.fingers;
        }
    }

    public void ClearHand(Chirality handedness)
    {
        if (handedness == Chirality.Left)
        {
            lHand = null;
            lFingers = null;
        }
        else if (handedness == Chirality.Right)
        {
            rHand = null;
            rFingers = null;
        }
    }
}
