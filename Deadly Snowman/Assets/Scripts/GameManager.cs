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
	public bool gameOver = false;

	public GameObject start;
	public GameObject end;

	public GameObject player;

	public Text scoreUI;
	public Text sizeUI;
	public Text itemsUI;

	bool isCoroutineActive = false;

	public float refreshDelayUI = 1.0f;

	// Use this for initialization
	void Start () {
		startTimer = Time.time;
		StartCoroutine(RefreshUI());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("space")){
			restartTheGame();
		}


	
	}


	void updateUIValues(){
		int scoreTMP = (int)getScore (); 
		scoreUI.text = scoreTMP.ToString();
		sizeUI.text = ballSize.ToString();
		itemsUI.text = ballContent.ToString();
	}


	IEnumerator RefreshUI() {
		isCoroutineActive = true;
		while (!gameOver) {
			updateUIValues ();
			yield return new WaitForSeconds(refreshDelayUI);
		}
		isCoroutineActive = false;
	}


	float getScore(){
		endTimer = Time.time;
		timer = (endTimer - startTimer);
		float score = timer*10 + ballSize*5 + ballContent*300;
		return score;
	}

	public void updateContent(float contentValue){
		ballContent += contentValue;
	}

	public void updateSize(float sizeMultiplier){
		ballSize *= sizeMultiplier;
	}

	public void endTheGame(){		
		gameOver = true;
		print ("Game over! Time is :" + timer.ToString() + "sec");
		print ("Score is :" + getScore());

	}

	public void restartTheGame(){
		//reset param + positions

		player.transform.position = start.transform.position;
		gameOver = false;
	}

}
