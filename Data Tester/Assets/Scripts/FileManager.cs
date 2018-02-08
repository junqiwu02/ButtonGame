using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour {

    public HandController lPalmCon;
    public HandController lThumbCon;
    public HandController lIndexCon;
    public HandController lMiddleCon;
    public HandController lRingCon;
    public HandController lPinkyCon;

    public HandController rPalmCon;
    public HandController rThumbCon;
    public HandController rIndexCon;
    public HandController rMiddleCon;
    public HandController rRingCon;
    public HandController rPinkyCon;

	public Transform gazePoint;

    private string path = "Assets/Resources/Data4.csv";
    private StreamReader reader;

    private float[] timeStamps;
    private int currTimeStamp = 0;

    private Vector3[] lPalm;
    private Vector3[] lThumb;
    private Vector3[] lIndex;
    private Vector3[] lMiddle;
    private Vector3[] lRing;
    private Vector3[] lPinky;

    private Vector3[] rPalm;
    private Vector3[] rThumb;
    private Vector3[] rIndex;
    private Vector3[] rMiddle;
    private Vector3[] rRing;
    private Vector3[] rPinky;

    private Vector2[] touchPos;
    private Vector2[] buttonPos;
	private Vector2[] gazePos;


	void Start () {

        int numOfLines = File.ReadAllLines(path).Length;

		reader = new StreamReader(path);
		reader.ReadLine();

		timeStamps = new float[numOfLines - 1];

		lPalm = new Vector3[numOfLines - 1];
		lThumb = new Vector3[numOfLines - 1];
		lIndex = new Vector3[numOfLines - 1];
		lMiddle = new Vector3[numOfLines - 1];
		lRing = new Vector3[numOfLines - 1];
		lPinky = new Vector3[numOfLines - 1];

		rPalm = new Vector3[numOfLines - 1];
		rThumb = new Vector3[numOfLines - 1];
		rIndex = new Vector3[numOfLines - 1];
		rMiddle = new Vector3[numOfLines - 1];
		rRing = new Vector3[numOfLines - 1];
		rPinky = new Vector3[numOfLines - 1];

        touchPos = new Vector2[numOfLines - 1];
        buttonPos = new Vector2[numOfLines - 1];
        gazePos = new Vector2[numOfLines - 1];

		for(int i = 0; i < numOfLines - 1; i++)
		{
			string[] data = reader.ReadLine().Split(',');

			timeStamps[i] = float.Parse(data[0]);

            touchPos[i] = new Vector2(float.Parse(data[1]), float.Parse(data[2]));
            buttonPos[i] = new Vector2(float.Parse(data[3]), float.Parse(data[4]));

            lPalm[i] = new Vector3(float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]));
			lThumb[i] = new Vector3(float.Parse(data[8]), float.Parse(data[9]), float.Parse(data[10]));
			lIndex[i] = new Vector3(float.Parse(data[11]), float.Parse(data[12]), float.Parse(data[13]));
			lMiddle[i] = new Vector3(float.Parse(data[14]), float.Parse(data[15]), float.Parse(data[16]));
			lRing[i] = new Vector3(float.Parse(data[17]), float.Parse(data[18]), float.Parse(data[19]));
			lPinky[i] = new Vector3(float.Parse(data[20]), float.Parse(data[21]), float.Parse(data[22]));

			rPalm[i] = new Vector3(float.Parse(data[23]), float.Parse(data[24]), float.Parse(data[25]));
			rThumb[i] = new Vector3(float.Parse(data[26]), float.Parse(data[27]), float.Parse(data[28]));
			rIndex[i] = new Vector3(float.Parse(data[29]), float.Parse(data[30]), float.Parse(data[31]));
			rMiddle[i] = new Vector3(float.Parse(data[32]), float.Parse(data[33]), float.Parse(data[34]));
			rRing[i] = new Vector3(float.Parse(data[35]), float.Parse(data[36]), float.Parse(data[37]));
			rPinky[i] = new Vector3(float.Parse(data[38]), float.Parse(data[39]), float.Parse(data[40]));

            gazePos[i] = new Vector2(float.Parse(data[41]), float.Parse(data[42]));
        }
	}
	
	void Update () {
		if(currTimeStamp < timeStamps.Length - 1)
        {
            if (Time.time >= timeStamps[currTimeStamp])
            {
                currTimeStamp++;
            }
        }

        lPalmCon.SetLerpPos(lPalm[currTimeStamp]);
        lThumbCon.SetLerpPos(lThumb[currTimeStamp]);
        lIndexCon.SetLerpPos(lIndex[currTimeStamp]);
        lMiddleCon.SetLerpPos(lMiddle[currTimeStamp]);
        lRingCon.SetLerpPos(lRing[currTimeStamp]);
        lPinkyCon.SetLerpPos(lPinky[currTimeStamp]);

        rPalmCon.SetLerpPos(rPalm[currTimeStamp]);
        rThumbCon.SetLerpPos(rThumb[currTimeStamp]);
        rIndexCon.SetLerpPos(rIndex[currTimeStamp]);
        rMiddleCon.SetLerpPos(rMiddle[currTimeStamp]);
        rRingCon.SetLerpPos(rRing[currTimeStamp]);
        rPinkyCon.SetLerpPos(rPinky[currTimeStamp]);

        if (!float.IsNaN(gazePos[currTimeStamp].x))
        {
            gazePoint.position = gazePos[currTimeStamp];
            Debug.Log(currTimeStamp+"...........");
            Debug.Log(gazePos[currTimeStamp]);
        }
    }
}
