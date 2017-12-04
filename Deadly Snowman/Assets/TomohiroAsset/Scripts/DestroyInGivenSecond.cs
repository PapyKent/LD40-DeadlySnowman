using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInGivenSecond : MonoBehaviour {

    public float time;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}

}
