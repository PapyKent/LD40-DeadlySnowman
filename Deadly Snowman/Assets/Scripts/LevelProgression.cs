using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour {


	public GameManager gm;
	Vector3 startingPositon;
	Vector3 endPosition;
	public float delay = 0.1f;

	float totalDistance = 0;

	float currentDistance = 0;


	bool isCoroutineActive = false;
	public Slider slider;


	// Use this for initialization
	void Start () {
		startingPositon = gm.start.transform.position;
		endPosition = gm.end.transform.position;

		totalDistance = Vector3.Distance (startingPositon, endPosition);


		StartCoroutine(StartProgress());
	}
	
	// Update is called once per frame
	void Update () {
		currentDistance = Vector3.Distance (gm.player.transform.position, gm.end.transform.position);

	}


	IEnumerator StartProgress() {
		isCoroutineActive = true;
		while (!gm.gameOver) {
			updateProgressBar ();
			yield return new WaitForSeconds(delay);
		}
		isCoroutineActive = false;
	}


	void updateProgressBar(){
		slider.value = 1 - (currentDistance/totalDistance);
	}
}
