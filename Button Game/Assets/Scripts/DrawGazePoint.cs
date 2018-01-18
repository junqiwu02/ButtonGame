using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class DrawGazePoint : MonoBehaviour {
    public GameObject gazePointPrefab;

    private Vector2 gazeVector;
    private float canvasWidth;
    private float canvasHeight;


	void Start () {
        RectTransform canvasTransform = GetComponent<RectTransform>();
        canvasWidth = canvasTransform.rect.width;
        canvasHeight = canvasTransform.rect.height;

	}
	
	void Update () {
        // instantiate clone of _gazePointPrefab as child of Canvas
        GameObject gazePoint = Instantiate(gazePointPrefab) as GameObject;
        gazePoint.transform.SetParent(transform);
        // get the point where the user is looking, map it to the Canvas coordinates and set the position of the GameObject
        gazeVector = TobiiAPI.GetGazePoint().Screen;
        if (!float.IsNaN(gazeVector.x) && !float.IsNaN(gazeVector.y))
        {
            gazeVector.x = Map(gazeVector.x, 0, Screen.width, -canvasWidth / 2, canvasWidth / 2);
            gazeVector.y = Map(gazeVector.y, 0, Screen.height, -canvasHeight / 2, canvasHeight / 2);
            gazePoint.transform.localPosition = gazeVector;
        }
        // destroy the object after 0.2 seconds
        Destroy(gazePoint, 0.2f);
    }

    private float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public Vector2 GetPos()
    {
        return gazeVector;
    }
}
