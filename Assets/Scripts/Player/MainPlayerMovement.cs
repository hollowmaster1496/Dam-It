﻿using UnityEngine;
using System.Collections;

public class MainPlayerMovement : MonoBehaviour {

	public float TranslateSpeed = 6.0f;
	public float RotateSpeed = 5.0f;
	public float JumpHeight = 5.0f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	public ParticleSystem dust;

	public bool canMove;
	public bool inWater;


	void Awake(){
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent<Rigidbody>();
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() //Called and fires every physics update
	{
		// Prevent player movement if canMove is false
		if(!canMove)
		{
			// idle animation
			anim.SetBool("isIdle", true);
			anim.SetBool("isOnLand", true);
			anim.SetInteger("Type_Idle", 0);
			anim.SetInteger("Type_LandMotion", 0);

			// diable dust particles
			dust.enableEmission = false;

			return;
		}

		// Access Controller Input
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Set speed and enable particles depending on current player animation
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") ||
		    anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
		{
			TranslateSpeed = 20.0f;
			JumpHeight = 15.0f;
			dust.enableEmission = true;
		} 
		else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Swim"))
		{
			TranslateSpeed = 25.0f;
			JumpHeight = 10.0f;
			dust.enableEmission = false;
		}
		else 
		{
			TranslateSpeed = 10.0f;
			JumpHeight = 5.0f;
			dust.enableEmission = false;
		}

		// Rotate player if Joystick is off-center
		if (h != 0.0f || v != 0.0f)
		{
			Turn (h, v);
		}

		// Update Position and animation
		Move (h, v, JumpHeight);
		Animating (h, v);
		
	}

	// Updates Position of player
	void Move(float h, float v, float jump)
	{

		float JumpDistance;
		// Jumping
		if (Input.GetKey (KeyCode.Space))
		{
			JumpDistance = jump;
		} 
		else
		{
			JumpDistance = 0;
		}
		
		movement.Set (h, JumpDistance, v);

		movement = Camera.main.transform.TransformDirection (movement);

		movement = movement.normalized * TranslateSpeed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position + movement);
		
	}

	// Rotates player
	void Turn(float horizontal, float vertical)
	{

		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		targetDirection = Camera.main.transform.TransformDirection(targetDirection);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, RotateSpeed * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);

	}

	// Animates player using Mecanim state transitions
	void Animating(float h, float v)
	{

		if (inWater == true)
		{
			/* Swim animation */
			anim.SetBool("isInWater", true);
			anim.SetBool("isOnLand", false);
			anim.SetInteger("Type_LandMotion", 0);
		} 
		else
		{

			anim.SetBool("isInWater", false);
			anim.SetBool("isOnLand", true);

			/* Land Animations */
			if (Input.GetKey (KeyCode.Space)) 
			{
				// jump animation
				anim.SetInteger ("Type_Idle", 1);
				anim.SetBool ("isIdle", true);
				anim.SetBool("isOnLand", false);
			} 
			else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) ||
			        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
			{
				// walk and run animation
				anim.SetBool("isIdle", false);
				anim.SetBool("isOnLand", true);
				anim.SetInteger("Type_LandMotion", 1);
				
			}
			else if(Input.anyKeyDown == false)
			{
				// idle animation
				anim.SetBool("isIdle", true);
				anim.SetBool("isOnLand", true);
				anim.SetInteger("Type_Idle", 0);
				anim.SetInteger("Type_LandMotion", 0);
			}
		}
	}
}
