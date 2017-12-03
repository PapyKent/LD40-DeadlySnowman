using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skier : MonoBehaviour
{

    public float speed;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float velocity = rb.velocity.magnitude;
        Debug.Log(velocity);

        if(velocity > speed)
        {
            animator.SetTrigger("Blown");
        }
    }
}

