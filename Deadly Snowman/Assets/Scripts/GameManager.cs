using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timer = 0;
	public float ballSize = 1;
	public float ballContent = 0;
	float startTimer=0;
	float endTimer=0;
	bool gameOver = false;

	public GameObject start;
	public GameObject end;

	public GameObject player;


	// Use this for initialization
	void Start () {
		startTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("space")){
			restartTheGame();
		}
	}



	public void updateContent(float contentValue){
		ballContent += contentValue;
	}

	public void updateSize(float sizeMultiplier){
		ballSize *= sizeMultiplier;
	}

	public void endTheGame(){
		endTimer = Time.time;
		timer = (endTimer - startTimer);
		print ("Game over! Time is :" + timer.ToString() + "sec");
		float score = timer + ballSize + ballContent;
		print ("Score is :" + score + "sec");
		gameOver = true;
	}

	public void restartTheGame(){
		//reset param + positions

		player.transform.position = start.transform.position;
		gameOver = false;
	}

}
