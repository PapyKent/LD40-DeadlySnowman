using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float growthRate;
	private Rigidbody rb;

	private float targetScale = -1f;

	void Start () {
		rb = GetComponent <Rigidbody> ();
		targetScale = transform.localScale.y;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.X))
			changeScale (transform.localScale.y + 2f);
		if (Input.GetKeyDown(KeyCode.Z))
			changeScale (transform.localScale.y - 2f);
	}
	
	void FixedUpdate () {
		if (transform.localScale.y != targetScale) {
			float newValue = 0f;
			if (transform.localScale.y < targetScale) {
				newValue = Mathf.Clamp (transform.localScale.y + growthRate, transform.localScale.y, targetScale);
			} else {
				newValue = Mathf.Clamp (transform.localScale.y - growthRate, targetScale, transform.localScale.y);
			}
			transform.localScale = new Vector3 (newValue, newValue, newValue);
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 force = new Vector3 (moveHorizontal * speed, 0f, 0f);
		rb.AddForce (force);
	}

	/* Changes the size of the snowball. */
	private void changeScale(float scale) {
		targetScale = scale;
	}
}