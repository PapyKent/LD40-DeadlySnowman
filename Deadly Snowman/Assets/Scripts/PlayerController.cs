using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	/* Public variables */
	public float HorizontalSensitivity; // how fast the snowball moves left to right
	public float GrowthRate; // how fast the snowball grows to its target size (animation)
	public float MassFactor; // How much does the mass increase per unit of scale (size)
	public float[] SizeBoundaries; // the scale values that cause the camera to go to second angle ([0]) and third angle ([1])
	public float RollGrowRate; // how much of its current size does the ball grow per frame?
	public GameObject BodyPart;
	public GameObject Camera;

	/* Private variables */
	private Rigidbody rb;
	private GameObject ballShadow;
	private ArrayList bodyParts;
	private ArrayList rotationAdded;

	/* Variables used for changing the size of the snowball. */
	private float targetScale = -1f;

	private int state;
	public const int STATE_GAMEPLAY = 1;
	public const int STATE_PAUSED = 2;

	void Start ()
	{
		rb = GetComponent <Rigidbody> ();
		targetScale = transform.localScale.y;
		bodyParts = new ArrayList ();
		rotationAdded = new ArrayList ();
		ballShadow = new GameObject ("PlayerShadow");
		ballShadow.transform.position = transform.position;
		state = STATE_GAMEPLAY;
	}

	void Update()
	{
		if (state == STATE_GAMEPLAY) {
			// For testing purposes only, allows you to change the size of the snowball using the X and Z keys.
			if (Input.GetKeyDown (KeyCode.X))
				ChangeSize (transform.localScale.y + 2f);
			if (Input.GetKeyDown (KeyCode.Z))
				ChangeSize (transform.localScale.y - 2f);
			// For testing purposes only, allows you to stick body parts using the C key.
			if (Input.GetKeyDown (KeyCode.C))
				StickRandomBodyPart ();
		}
	}

	/* Changes the size of the snowball.
	 * parameters: scale - the new scale of the snowball. */
	public void ChangeSize(float scale) {
		targetScale = Mathf.Clamp(scale, 1, 200);
	}

	public void StickRandomBodyPart()
	{
		//GameObject temp = Instantiate (BodyPart, gameObject.transform);
		//temp.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - (gameObject.transform.localScale.y / 2f), gameObject.transform.position.z);
		//temp.transform.Rotate (Vector3.zero - gameObject.transform.eulerAngles, Space.Self);
		GameObject temp = Instantiate(BodyPart);
		temp.transform.position = ballShadow.transform.position + new Vector3(0f, (gameObject.transform.localScale.y / 2), 0f);
		temp.transform.SetParent (ballShadow.transform);
		//temp.transform.localPosition = new Vector3(0f, (gameObject.transform.localScale.y / 2), 0f);
		bodyParts.Add (temp);
		rotationAdded.Add (ballShadow.transform.rotation);
	}
	
	void FixedUpdate () {
		if (state == STATE_GAMEPLAY) {
			// Updates the size of the snowball
			if (transform.localScale.y != targetScale) {
				float newValue = 0f;
				if (transform.localScale.y < targetScale) {
					newValue = Mathf.Clamp (transform.localScale.y + GrowthRate, transform.localScale.y, targetScale);
				} else {
					newValue = Mathf.Clamp (transform.localScale.y - GrowthRate, targetScale, transform.localScale.y);
				}
				transform.localScale = new Vector3 (newValue, newValue, newValue);
			}

			// Updates the movement of the snowball from left to right
			float moveHorizontal = Input.GetAxis ("Horizontal");
			Vector3 force = new Vector3 (moveHorizontal * HorizontalSensitivity, 0f, 0f);
			rb.AddForce (force);

			// Grows the snowball (from rolling)
			ChangeSize (transform.localScale.y + RollGrowRate);

			// Updates the mass of the snowball
			rb.mass = 1 + transform.localScale.y * MassFactor;

			// Updates the camera based on the size of the snowball
			for (int i = SizeBoundaries.Length - 1; i >= 0; i--) {
				if (transform.localScale.y >= SizeBoundaries [i]) {
					Camera.GetComponent <CameraController> ().ChangeAngle (i);
					break;
				}
			}

			// Places the ball shadow in the same location as the ball
			ballShadow.transform.position = transform.position;
			ballShadow.transform.rotation = transform.rotation;
			for (int i = 0; i < bodyParts.Count; i++) {
				GameObject curr = (GameObject)bodyParts [i];
				Quaternion temp = ballShadow.transform.rotation;
				ballShadow.transform.rotation = (Quaternion)rotationAdded [i];
				curr.transform.position = ballShadow.transform.position + new Vector3 (0f, (gameObject.transform.localScale.y / 2), 0f);
				ballShadow.transform.rotation = temp;
			}
		}
	}

	public void ChangeState(int state)
	{
		this.state = state;
	}
}