﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAway : MonoBehaviour {

    [SerializeField]
    Transform snowBall;

    public float explosionForce = 10;
    public float radious = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        float blowRadious = radious * snowBall.localScale.x;
        if (other.gameObject.CompareTag("Target")) //If detected an object with "Target" tag
        {
            Collider[] cols = Physics.OverlapSphere(this.transform.position, blowRadious); //Objects in given radious

            foreach (var col in cols)
            {
                //Blow away every object which has "Target" tag
                if (col.gameObject.CompareTag("Target"))
                {
                    if (col.attachedRigidbody.velocity.magnitude < 10) //Prevent objects getting blown away more than once
                    {
                        col.attachedRigidbody.AddExplosionForce(explosionForce, this.transform.position, blowRadious, 1, ForceMode.Impulse);
                        col.attachedRigidbody.AddForce(0, 500, 0);
                    }
                }
            }
        }
    }
}
