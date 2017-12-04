using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIScript2 : MonoBehaviour {

	public int NumberOfFramesInCycle;
	public float CircleRadius;
	private float increment;

	private float currentAngle;
	private float currentLocationX;
	private float currentLocationY;

	// Use this for initialization
	void Start () {
		increment = 360f / NumberOfFramesInCycle;
		currentAngle = 0f;
		currentLocationX = transform.position.x;
		currentLocationY = transform.position.z;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		currentAngle += increment;
		while (currentAngle >= 360)
			currentAngle -= 360;
		gameObject.transform.Rotate(new Vector3(0f, -increment, 0f));
		float xDisp = Mathf.Cos (currentAngle * Mathf.Deg2Rad) * CircleRadius;
		float yDisp = Mathf.Sin (currentAngle * Mathf.Deg2Rad) * CircleRadius;

		transform.position = new Vector3 (currentLocationX + xDisp, transform.position.y, currentLocationY + yDisp);
	}

	void turnAround() {
		
	}
}
