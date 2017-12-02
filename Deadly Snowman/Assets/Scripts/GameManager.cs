using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timer = 0;
	float ballSize = 1;
	public int ballContent = 0;
	float startTimer=0;
	float endTimer=0;
	bool gameOver = false;


	// Use this for initialization
	void Start () {
		startTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {


		//end of the game
		if (Input.GetKeyUp ("space") && !gameOver) {
			endTimer = Time.time;
			timer = (endTimer - startTimer);
			print ("Game over! Time is :" + timer.ToString() + "sec");
			float score = timer + ballSize + ballContent;
			print ("Score is :" + score + "sec");
			gameOver = true;
		}
	}
}
