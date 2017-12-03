using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	public float ChangeViewSpeed;
	public float[] DistanceModes;
	public float[] HeightModes;
	public float[] RotationModes;

	private float targetDistance;
	private float targetHeight;
	private float targetRotation;

	// list of camera angles
	public float[,] cameraAngles;
	private int currentAngleIndex;

	void Start () {
		cameraAngles = new float[DistanceModes.Length, DistanceModes.Length];
		for (int i = 0; i < DistanceModes.Length; i++) {
			cameraAngles [i, 0] = DistanceModes [i];
			cameraAngles [i, 1] = HeightModes [i];
			cameraAngles [i, 2] = RotationModes [i];
		}

		offset = transform.position - player.transform.position;
		currentAngleIndex = 0;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.A)) {
			ChangeAngle (Mathf.Clamp (currentAngleIndex - 1, 0, cameraAngles.GetLength(0) - 1));
		} else if (Input.GetKeyDown (KeyCode.S)) {
			Debug.Log (cameraAngles.Length);
			ChangeAngle (Mathf.Clamp (currentAngleIndex + 1, 0, cameraAngles.GetLength(0) - 1));
		}
	}

	void FixedUpdate()
	{
		if (offset.z != targetDistance)
		{
			float newZ = -1;
			if (offset.z < targetDistance) {
				newZ = Mathf.Clamp (offset.z + ChangeViewSpeed, offset.z, targetDistance);
			}
			else
			{
				newZ = Mathf.Clamp (offset.z - ChangeViewSpeed, targetDistance, offset.z);
			}
			offset = new Vector3 (offset.x, offset.y, newZ);
		}
		if (offset.y != targetHeight)
		{
			float newY = -1;
			if (offset.y < targetHeight) {
				newY = Mathf.Clamp (offset.y + ChangeViewSpeed, offset.y, targetHeight);
			}
			else
			{
				newY = Mathf.Clamp (offset.y - ChangeViewSpeed, targetHeight, offset.y);
			}
			offset = new Vector3 (offset.x, newY, offset.z);
		}
		if (transform.eulerAngles.x != targetRotation) {
			float newR = -1;
			if (transform.eulerAngles.x < targetRotation) {
				newR = Mathf.Clamp (transform.eulerAngles.x + ChangeViewSpeed, transform.eulerAngles.x, targetRotation);
			}
			else
			{
				newR = Mathf.Clamp (transform.eulerAngles.x - ChangeViewSpeed, targetRotation, transform.eulerAngles.x);
			}
			transform.eulerAngles = new Vector3 (newR, transform.eulerAngles.y, transform.eulerAngles.z);
		}
	}

	public void ChangeAngle(int index)
	{
		currentAngleIndex = index;
		targetDistance = cameraAngles [index, 0];
		targetHeight = cameraAngles [index, 1];
		targetRotation = cameraAngles [index, 2];
	}
	
	void LateUpdate() {
		transform.position = player.transform.position + offset;
	}
}
