using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 6.0f;
	public float rotateSpeed = 5.0f;

	public float DistanceToGround; //Random Testing Variable for isGrounded determination

	float JumpDistance;
	//float JumpTimer;


	Vector3 movement;
	//Vector3 RotationTarget;
	Animator anim;
	Rigidbody playerRigidbody;
	//int floorMask;
	//float camRayLength = 100;


	void Awake()
	{
		//floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent<Rigidbody>();

		//JumpTimer = 0f;
		DistanceToGround = 20f;
	}

	void Update()
	{
		RaycastHit hit;
		Ray ray = new Ray (transform.position, -Vector3.up);
		Vector3 GroundRay = transform.TransformDirection(Vector3.down);

		//RayCast Ground Detection Not Working Properly
		if (Physics.Raycast (ray, out hit, DistanceToGround)) 
		{
			Debug.Log ("The Ground is detectable;");
			Debug.DrawLine(ray.origin, hit.point);
		} 
		else
			Debug.Log ("No Ground Below");
			Debug.DrawLine(ray.origin, hit.point);

	}

	void FixedUpdate() //Called and fires every physics update
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");



		Move (h, v);
		Animating (h, v);

		if (h != 0.0f || v != 0.0f) 
		{
			Turn (h, v);
		}

	}

	void Move(float h, float v)
	{
		//Jumping
		if (Input.GetKey (KeyCode.Space)) 
		{
			JumpDistance = -2.5f;
			//JumpTimer =  Time.time;
			//Debug.Log("JumpTimer:" + JumpTimer);
		} 
		else
		{
			JumpDistance = 0;
		}

		/*if(Time.time - JumpTimer >= 3.of)
			movement.Set (h + Jump)  */

		movement.Set (h, JumpDistance, v);
		
		movement = movement.normalized * speed * Time.deltaTime;
		
		playerRigidbody.MovePosition (transform.position - movement);

		//JumpDistance = 0f; // Walking

	}

	void Turn(float horizontal, float vertical)
	{
		//transform.Rotate (0, Input.GetAxis("Horizontal") * rotateSpeed, 0);


		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, rotateSpeed * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);




		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			//Might need to rotate by 180 due to rotation issue
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}*/
	}

	void Animating(float h, float v)
	{
		//Walking Animation
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("isWalking", walking);

		//bool crouching;

		/*
		//Crouching Animation
		if (Input.GetKey (KeyCode.Z)) 
		{
			crouching = true;
		} 
		else 
		{
			crouching = false;
			//jumping = false;
		}
		*/

		//anim.SetBool ("isCrouching", crouching);
		anim.SetFloat ("isJumping", JumpDistance);


	}

	//Checks whether Player is on the ground
	/*bool isGrounded()
	{
		if(Physics.Raycast (this.transform.position, -Vector3.up, this.groundHit)) 
		{
			
			if (this.groundHit.distance < 0.05) {
				this.isGrounded = true;
				JumpTimer = 0;
			}
			else 
			{
				this.isGrounded = false;
			}
		}


	}*/



	//Write Function to check whether grounded
	//Write Function to do midair jump and reset JumpTimer at end of call

	/*void Jump(float h, float v)
	{
		movement.Set(h, 2.5f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		rigidbody.MovePosition (transform.position + movement);
	}*/
}
