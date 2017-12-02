﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float growthRate;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent <Rigidbody> ();
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 force = new Vector3 (moveHorizontal * speed, 0f, 0f);

		rb.AddForce (force);
	}
}