using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIScript1 : MonoBehaviour {
	
	public int FramesBeforeTurning;

	private bool collidesWithTerrain;
	private bool forward = true;
	private int currentFrames = 0;

	// Use this for initialization
	void Start () {
		collidesWithTerrain = false;
		currentFrames = Random.Range (0, FramesBeforeTurning);
		gameObject.transform.Rotate(new Vector3(0f, 90f, 0f));
		if (Random.value >= 0.5) {
			forward = false;
			gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (forward) {
			gameObject.transform.position += new Vector3 (0.025f, 0f, 0f);
		} else {
			gameObject.transform.position += new Vector3 (-0.025f, 0f, 0f);
		}
		currentFrames++;
		if (currentFrames >= FramesBeforeTurning) {
			turnAround ();

			currentFrames = 0;
		}

	}

	void turnAround() {
		gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
		forward = !forward;
	}
}
