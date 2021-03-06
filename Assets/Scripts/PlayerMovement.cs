﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float walkingSpeed = 2.5f;
	public float sidewaysSpeed = 2.5f;

	private Animator animator;

	private bool walking = false;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update()
	{
        if (!GameManager.isGamePaused)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Movement(h, v);
        }
	}

	protected void Movement(float h, float v)
	{
		// Transitions between states
		if (!Mathf.Approximately(v, 0.0f) || !Mathf.Approximately(h, 0.0f)) 
		{
			if (!walking) 
			{
				animator.SetTrigger ("startWalking");
				walking = true;
			}

			if (Mathf.Approximately(v, 0.0f)) 
			{
				animator.applyRootMotion = false;


				// Walking forwards or backwards
				animator.SetFloat ("speed", h * sidewaysSpeed);
			} 
			else 
			{
                //animator.applyRootMotion = true;
                animator.applyRootMotion = false;

                // Walking forwards or backwards
                animator.SetFloat ("speed", v * walkingSpeed);
			}
		} 
		else 
		{
			if (walking) 
			{
				animator.SetTrigger ("backToIdle");
				walking = false;
			}
		}

		// Walking sideways
		transform.Translate(h * Vector3.right * sidewaysSpeed * Time.deltaTime);
        transform.Translate(v * Vector3.forward * walkingSpeed * Time.deltaTime);
    }
}
