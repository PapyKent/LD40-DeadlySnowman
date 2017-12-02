using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


	public GameManager gm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		print ("collide with " + other.name);
		if (other.GetComponent<Item> ()) {
			Item item = other.GetComponent<Item> ();
			gm.updateSize (item.scoreUpdate);
			gm.updateContent(item.contentValue);

			if (item.gameOver) {
				gm.endTheGame ();
			}
		}

	
	}
	 
}
