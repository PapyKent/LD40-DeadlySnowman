using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MashingManager : MonoBehaviour {

	public bool gIsDown;
	public float rollingFaceLimit;
	public int gain;
	public GameObject mashingUI;
	private RawImage key1;
	private RawImage key2;
	public Color lowAlpha = new Color(0.0f, 1.0f, 0.0f, 0.25f);
	public Color highAlpha = new Color(0.0f, 1.0f, 0.0f, 1.0f);

	int currentValue = 0;
	public int valueToReach = 500;
	public float rewardValue = 10;

	public GameManager gm;
	// Use this for initialization
	void Start ()
	{
		
	}
	void OnEnable(){
		gIsDown = false;
		InvokeRepeating("CheckRollingFaceRate", 0.0f, rollingFaceLimit);
		key1 = mashingUI.transform.Find("Key1").GetComponent<RawImage>();
		key2 = mashingUI.transform.Find("Key2").GetComponent<RawImage>();
		key1.color = highAlpha;
		key2.color = lowAlpha;
		currentValue = 0;
	}
		


	// Update is called once per frame
	void Update ()
	{
		if (currentValue >= valueToReach && gm.eventOccuring == true) {
			print ("this is over!");
			currentValue = 0;
			gm.eventOccuring = false;
			mashingUI.SetActive (false);
			SoundManager.instance.stopShonen ();
			gm.changeCamera ();
			gm.updateContent (rewardValue);
			gm.ResumeGame ();
		}


		if (!gIsDown && Input.GetKeyDown(KeyCode.Alpha1))
		{
			//Terminator.GetTerminator().isRecharging = true;
			gIsDown = true;
			key1.color = lowAlpha;
			key2.color = highAlpha;
			currentValue += gain;
		}
		else if (gIsDown && Input.GetKeyDown(KeyCode.Alpha3))
		{
			gIsDown = false;
			//this.gameObject.GetComponent<Terminator>().energy.CurrentValue += energyGain;
			key1.color = highAlpha;
			key2.color = lowAlpha;
			currentValue += gain;
		}
	}

	private void CheckRollingFaceRate()
	{
		if (gIsDown)
		{
			gIsDown = false;
			key1.color = highAlpha;
			key2.color = lowAlpha;
		}
		//Terminator.GetTerminator().isRecharging = false;
	}
}
