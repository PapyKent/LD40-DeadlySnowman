using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public Animator vsAnimator;
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
			restartTheGame();
		}
					
	
	}


	public void startCoVS(){
		PauseGame ();
		StartCoroutine(launchVS());
	}
		
	public void activateVSAnim(){
		vsAnimator.SetTrigger("enterVS");
	}

	public void desactivateVSAnim(){
		vsAnimator.ResetTrigger("enterVS");
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
		isVSAnimPlaying = true;
		activateVSAnim ();
		yield return new WaitForSeconds(1.5f);
		desactivateVSAnim ();
		isVSAnimPlaying = false;
		eventOccuring = true;
		changeStateMashing ();
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
		print ("Score is : " + timer*10 + " "+ ballSize*5 +" "+ ballContent*300 + " " + getScore());

	}

	public void restartTheGame(){
		//reset param + positions

		player.transform.position = start.transform.position;
		gameOver = false;
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
	}

}
