using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leap;
using Leap.Unity;

public class FileManager : MonoBehaviour {

    public string path = "Assets/Resources/Test.txt";

    public DrawGazePoint gazePlotter;
    public TouchInput touch;

    private HandModel lHand;
    private HandModel rHand;
    private FingerModel[] lFingers = new FingerModel[5];
    private FingerModel[] rFingers = new FingerModel[5];

    private StreamWriter writer;

	void Start () {
        // clear the text file
        File.WriteAllText(path, "");
        writer = new StreamWriter(path, true);
        // title
        writer.WriteLine("time,gaze,,touch,,l_palm,,,l_thumb,,,l_index,,,l_middle,,,l_ring,,,l_pinky,,,r_palm,,,r_thumb,,,r_index,,,r_middle,,,r_ring,,,r_pinky,,,");
	}
	
	void Update () {
        writer.Write(Time.time + ",");
        writer.Write(gazePlotter.GetPos() + ",");
        writer.Write(touch.GetPos() + ",");
            
        // write hand values if a hand exists
        if(lHand != null)
        {
            writer.Write(lHand.GetPalmPosition().ToString("F6") + ",");

            foreach (FingerModel lFinger in lFingers)
            {
                //string type = lFinger.fingerType.ToString();
                //type = "l" + type.Substring(4).ToLower();
                writer.Write(lFinger.GetTipPosition().ToString("F6") + ",");
            }
        }
        else
        {
            writer.Write(",,,,,,,,,,,,,,,,,,");
        }

        if(rHand != null)
        {
            writer.Write(rHand.GetPalmPosition().ToString("F6") + ",");

            foreach (FingerModel rFinger in rFingers)
            {
                //string type = rFinger.fingerType.ToString();
                //type = "r" + type.Substring(4).ToLower();
                writer.Write(rFinger.GetTipPosition().ToString("F6") + ",");
            }
        }
        else
        {
            writer.Write(",,,,,,,,,,,,,,,,,,");
        }
        writer.WriteLine();
    }

    private void OnApplicationQuit()
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

    public void clearHand(Chirality handedness)
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
