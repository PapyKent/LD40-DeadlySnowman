using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	
	public float speedMultiplier = 1;
	public float sizeAdder = 1;
	public float contentValue = 1;
	public GameObject bodyPart;

	public bool gameOver = false;
	public bool triggerVS = false;

	private bool collided;

	// Use this for initialization
	void Start () {
		collided = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerCollide()
	{
		collided = true;
		Destroy (gameObject, 0f);
	}
		
	public bool isCollided() {
		return collided;
	}
}
