using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSnowBall : MonoBehaviour {

    [SerializeField]
    Transform player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = player.localScale;
        transform.position = new Vector3(player.position.x, player.position.y - player.localScale.y/3f, player.position.z);
		
	}
}
