using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	/* Public variables */
	public float HorizontalSensitivity; // how fast the snowball moves left to right
	public float GrowthRate; // how fast the snowball grows to its target size
	public float MassFactor; // How much does the mass increase per unit of scale (size)
	public GameObject BodyPart;

	/* Private variables */
	private Rigidbody rb;

	/* Variables used for changing the size of the snowball. */
	private float targetScale = -1f;

	void Start ()
	{
		rb = GetComponent <Rigidbody> ();
		targetScale = transform.localScale.y;
	}

	void Update()
	{
		// For testing purposes only, allows you to change the size of the snowball using the X and Z keys.
		if (Input.GetKeyDown(KeyCode.X))
			ChangeSize (transform.localScale.y + 2f);
		if (Input.GetKeyDown(KeyCode.Z))
			ChangeSize (transform.localScale.y - 2f);
		// For testing purposes only, allows you to stick body parts using the C key.
		if (Input.GetKeyDown (KeyCode.C))
			StickRandomBodyPart ();
	}

	/* Changes the size of the snowball.
	 * parameters: scale - the new scale of the snowball. */
	public void ChangeSize(float scale) {
		targetScale = Mathf.Clamp(scale, 1, 200);
	}

	public void StickRandomBodyPart()
	{
		GameObject temp = Instantiate (BodyPart, gameObject.transform);
		//temp.transform.localPosition = new Vector3 (0f, 2f, 0f);
		//temp.transform.localRotation = Random.rotation;
		temp.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - (gameObject.transform.localScale.y / 2f), gameObject.transform.position.z);
		temp.transform.Rotate (Vector3.zero - gameObject.transform.eulerAngles, Space.Self);

	}
	
	void FixedUpdate () {
		// Updates the size of the snowball
		if (transform.localScale.y != targetScale)
		{
			float newValue = 0f;
			if (transform.localScale.y < targetScale)
			{
				newValue = Mathf.Clamp (transform.localScale.y + GrowthRate, transform.localScale.y, targetScale);
			} else
			{
				newValue = Mathf.Clamp (transform.localScale.y - GrowthRate, targetScale, transform.localScale.y);
			}
			transform.localScale = new Vector3 (newValue, newValue, newValue);
		}

		// Updates the movement of the snowball from left to right
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 force = new Vector3 (moveHorizontal * HorizontalSensitivity, 0f, 0f);
		rb.AddForce (force);

		// Updates the mass of the snowball
		rb.mass = 1 + transform.localScale.y * MassFactor;
	}
}