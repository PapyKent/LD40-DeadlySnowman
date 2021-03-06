﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject gameCamera;
	public GameObject versusCamera;
	public GameObject goCamera;

	public Animator vsAnimator;
	public Animator vsAnimator2;

	public Animator bodybuilderAnimator;
	public Animator snowballAnimator;


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

	public GameObject UI;
	public Text finalTimeUI;
	public Text finalScoreUI;
	public GameObject GOScreen;

	bool isCoroutineActive = false;

	public float refreshDelayUI = 1.0f;

	public bool eventOccuring = false;

	public bool isVSAnimPlaying = false;

	public GameObject mashingEvent;

	/* used for pausing the game */
	private Vector3 tempVelocity;
	private Vector3 tempAngularVelocity;

	// Use this for initialization
	void Start () {
		startTimer = Time.time;
		StartCoroutine(RefreshUI());
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("space")){
			//restartTheGame();
		}			

		ballSize = (int)player.GetComponent<PlayerController>().getBallSize ();
	}


	public void startCoVS(){
		PauseGame ();
		StartCoroutine(launchVS());
	}
		
	public void activateVSAnim(int i){
		if(i == 1)
			vsAnimator.SetTrigger("enterVS");
		if(i == 2)
			vsAnimator2.SetTrigger("bbiscoming");
	}


	public void activateVSEndFightAnim(){				
		bodybuilderAnimator.SetTrigger ("FinishBattle");
		snowballAnimator.SetTrigger ("FinishBattle");		
	}

	public void desactivateVSEndFightAnim(){				
		bodybuilderAnimator.ResetTrigger ("FinishBattle");
		snowballAnimator.ResetTrigger ("FinishBattle");		
	}

	public void desactivateVSAnim(int i){
		if(i == 1)
			vsAnimator.ResetTrigger("enterVS");
		if(i == 2)
			vsAnimator2.ResetTrigger("bbiscoming");
	}

	void changeStateMashing(){
		mashingEvent.SetActive (!mashingEvent.activeSelf);
	}


	void desactivateMashing(){
		mashingEvent.SetActive (false);
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

	IEnumerator launchVS() {
		changeUIstate ();
		isVSAnimPlaying = true;
		SoundManager.instance.pauseBGMusic ();
		SoundManager.instance.playOpeningShonen ();
		activateVSAnim (1);
		activateVSAnim (2);
		yield return new WaitForSeconds(1.5f);
		changeCamera ();
		desactivateVSAnim (1);
		changeStateMashing ();
		SoundManager.instance.playShonen ();
		desactivateVSAnim (2);
		isVSAnimPlaying = false;
		eventOccuring = true;

	}


	float getScore(){
		endTimer = Time.time;
		timer = (endTimer - startTimer);
		float score =  (ballSize*5);
		return score;
	}

	public void updateContent(float contentValue){
		ballContent += contentValue;
	}

	public void updateSize(float sizeMultiplier){
		ballSize *= sizeMultiplier;
	}
		


	public void endTheGame(){
		changeUIstate ();	
		changeCameraGO ();	
		gameOver = true;
		GOScreen.SetActive (true);
		float score = getScore();
		endTimer = Time.time;
		timer = (endTimer - startTimer);
		finalTimeUI.text = timer.ToString();
		finalScoreUI.text = score.ToString();

	}

	public void restartTheGame(){
		player.transform.position = start.transform.position;
		gameOver = false;
		startTimer = Time.time;
		ballSize = 1;
		ballContent = 0;
		changeCameraGO ();
		//repop everything
	}

	public void changeUIstate(){
		UI.SetActive (!UI.activeSelf);
	}


	public void changeCamera(){
		gameCamera.SetActive (!gameCamera.activeSelf);
		versusCamera.SetActive (!versusCamera.activeSelf);
	}

	public void changeCameraGO(){
		gameCamera.SetActive (!gameCamera.activeSelf);
		goCamera.SetActive (!goCamera.activeSelf);
	}

	/* Pauses the main game. */
	public void PauseGame() {
		PlayerController playerScript = player.GetComponent <PlayerController> ();
		playerScript.ChangeState (PlayerController.STATE_PAUSED);
		Rigidbody rb = player.GetComponent <Rigidbody> ();
		tempVelocity = rb.velocity;
		tempAngularVelocity = rb.angularVelocity;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = true;
	}

	public void ResumeGame()
	{
		Debug.Log ("HEY");
		PlayerController playerScript = player.GetComponent <PlayerController> ();
		playerScript.ChangeState (PlayerController.STATE_GAMEPLAY);
		Rigidbody rb = player.GetComponent <Rigidbody> ();
		rb.velocity = tempVelocity;
		rb.angularVelocity = tempAngularVelocity;
		rb.isKinematic = false;
		desactivateVSEndFightAnim ();
	}

}
