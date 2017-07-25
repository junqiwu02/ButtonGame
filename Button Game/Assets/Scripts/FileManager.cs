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
    private int counter = 0;

	void Start () {
        // clear the text file
        File.WriteAllText(path, "");
        writer = new StreamWriter(path, true);
	}
	
	void Update () {
        // write values only every x frames
        counter++;
        if(counter % 4 == 0)
        {
            counter = 0;

            writer.WriteLine("time:" + Time.time + ",");
            writer.Write("gaze:" + gazePlotter.GetPos() + ",");
            writer.WriteLine("touch:" + touch.GetPos() + ",");
            
            // write hand values if a hand exists
            if(lHand != null)
            {
                writer.WriteLine("lHand:" + lHand.GetPalmPosition().ToString("F6") + ",");

                foreach (FingerModel lFinger in lFingers)
                {
                    string type = lFinger.fingerType.ToString();
                    type = "l" + type.Substring(4).ToLower();
                    writer.Write(type + ":");
                    writer.Write(lFinger.GetTipPosition().ToString("F6") + ",");
                }
                writer.WriteLine();
            }

            if(rHand != null)
            {
                writer.WriteLine("rHand:" + rHand.GetPalmPosition().ToString("F6") + ",");

                foreach (FingerModel rFinger in rFingers)
                {
                    string type = rFinger.fingerType.ToString();
                    type = "r" + type.Substring(4).ToLower();
                    writer.Write(type + ",");
                    writer.Write(rFinger.GetTipPosition().ToString("F6") + ",");
                }
                writer.WriteLine();
            }
            writer.WriteLine();
        }
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
}
