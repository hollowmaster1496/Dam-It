using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	private float distanceAway = 30;
	private float distanceUp = 1;
	private float smooth = 10;
	private Transform follow;
	private Transform targetPosition;
	private float scrollFactor = 0.0f;

	public float zoomSpeed = 5.0f;

	private Transform cameraPivot;


	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag ("Player").transform;
		targetPosition = this.transform;
		cameraPivot = GameObject.Find ("CameraPivot").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate()
	{
		cameraPivot.position = follow.position;

		scrollFactor = ZoomCamera ();

		targetPosition.position = cameraPivot.position + (Vector3.up * distanceUp) -
			(Camera.main.transform.TransformDirection(Vector3.forward) * distanceAway);

		if (Input.GetKey (KeyCode.B)) 
		{
			cameraPivot.RotateAround(cameraPivot.position, Vector3.up, 3.0f);
		}
		else if (Input.GetKey (KeyCode.N)) 
		{
			cameraPivot.RotateAround(cameraPivot.position, Vector3.up, -3.0f);
		}
		else
		{
			this.transform.position = Vector3.Lerp (transform.position, targetPosition.position, smooth);//targetPosition;
		}
	}

	void Rotate(float horizontal, float vertical)
	{
		
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		targetDirection = Camera.main.transform.TransformDirection(targetDirection);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(cameraPivot.rigidbody.rotation, targetRotation, 35f * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		cameraPivot.rigidbody.MoveRotation(newRotation);
		
	}

	float ZoomCamera()
	{
		float scroll = Input.GetAxis ("Fire1");
		if (0.6f < scroll && scroll < 1.0f) 
		{
			Debug.Log("Scroll:" + scroll);
			distanceAway = 35f * Mathf.Abs (scroll);
		}

		return scroll + 1;
	}
}
