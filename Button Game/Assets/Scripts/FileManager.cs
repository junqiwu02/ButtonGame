using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Leap;
using Leap.Unity;
using Tobii.Gaming;


public class FileManager : MonoBehaviour {

    public DrawGazePoint gazePlotter;
    public TouchInput touch;
    public ButtonManager bManager;

    private HandModel lHand;
    private HandModel rHand;
    private FingerModel[] lFingers = new FingerModel[5];
    private FingerModel[] rFingers = new FingerModel[5];

    private StreamWriter writer;

    private string path;
    private string editorPath = "/Resources/";
    private string buildPath = "/User Data/";

	private double startTime;


	public RectTransform canvasTransform;
	private float canvasWidth;
	private float canvasHeight;


	void Start () {
		canvasWidth = canvasTransform.rect.width;
		canvasHeight = canvasTransform.rect.height;

        if(!MenuController.username.Equals("nosave"))
        {
            startTime = Time.time;

            // set path
            if (Application.isEditor)
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
            writer.WriteLine("time,touch,,button_pos,,l_palm,,,l_thumb,,,l_index,,,l_middle,,,l_ring,,,l_pinky,,,r_palm,,,r_thumb,,,r_index,,,r_middle,,,r_ring,,,r_pinky,,,GazePoint.x,GazePoint.y");
            // writer.WriteLine("time,gaze,,touch,,button_pos,,l_palm,,,l_thumb,,,l_index,,,l_middle,,,l_ring,,,l_pinky,,,r_palm,,,r_thumb,,,r_index,,,r_middle,,,r_ring,,,r_pinky,,");
        }
    }

    void Update () {
        if(!MenuController.username.Equals("nosave"))
        {
            double elapsedTime = Time.time - startTime;

            // only write the time if the game is paused
            if (ButtonManager.isPaused)
            {
                writer.WriteLine(elapsedTime + ",paused");
            }
            else
            {
                writer.Write(elapsedTime + ",");
                // string gazePos = gazePlotter.GetPos() + ",";
                // remove parentheses
                // writer.Write(gazePos.Replace("(","").Replace(")",""));
                string touchPos = touch.GetPos().ToString("F5") + ",";
                writer.Write(touchPos.Replace("(", "").Replace(")", ""));

                // get marked button position
                Vector3 bPos3 = bManager.GetButton().transform.position;
                Vector2 bPos = new Vector2(bPos3.x, bPos3.y);
                writer.Write(bPos.ToString("F5").Replace("(", "").Replace(")", "") + ",");

                // write hand values if a hand exists
                if (lHand != null)
                {
                    string palmPos = lHand.GetPalmPosition().ToString("F5") + ",";
                    writer.Write(palmPos.Replace("(", "").Replace(")", ""));

                    foreach (FingerModel lFinger in lFingers)
                    {
                        //string type = lFinger.fingerType.ToString();
                        //type = "l" + type.Substring(4).ToLower();
                        string fingerPos = lFinger.GetTipPosition().ToString("F5") + ",";
                        writer.Write(fingerPos.Replace("(", "").Replace(")", ""));
                    }
                }
                else
                {
                    //writer.Write(",,,,,,,,,,,,,,,,,,");
                    for (int i = 0; i < 18; i++)
                    {
                        writer.Write(float.NaN + ",");
                    }
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
                        if (i == 4)
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
                    //writer.Write(",,,,,,,,,,,,,,,,,");
                    for (int i = 0; i < 17; i++)
                    {
                        writer.Write(float.NaN + ",");
                    }
                    writer.Write(float.NaN);
                }

				///////////////////////////////////////////////////////////////////////
				///Write the gaze point tracking data

                Vector2 gazeVector = TobiiAPI.GetGazePoint().Viewport;
				if (!float.IsNaN (gazeVector.x) && !float.IsNaN (gazeVector.y)) {
					gazeVector.x = Map (gazeVector.x, 0, 1, -0.32f, 0.32f);
					gazeVector.y = Map (gazeVector.y, 0, 1, -0.18f, 0.18f);
					writer.Write (","+gazeVector.x+","+gazeVector.y);
				} 
				else 
				{
					writer.Write (",NaN,NaN");
				}
                writer.WriteLine();
            }
        }
    }
	private float Map(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
    private void OnApplicationQuit()
    {
        if (!MenuController.username.Equals("nosave"))
            writer.Close();
    }

    private void OnDisable()
    {
        if(!MenuController.username.Equals("nosave"))
            writer.Close();
    }

    private void OnDestroy()
    {
        if (!MenuController.username.Equals("nosave"))
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
