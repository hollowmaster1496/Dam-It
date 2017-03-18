using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	private float distanceAway = 50;
	private float distanceUp = 1;
	private float smooth = 10;
	private Transform follow;
	private Transform targetPosition;

	public Camera skyCam;

	public float zoomSpeed = 5.0f;
	public float zoomFactor = 1.0f;

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

		ZoomCamera ();

		targetPosition.position = cameraPivot.position + ((Vector3.up * distanceUp) -
			(Camera.main.transform.TransformDirection(Vector3.forward) * distanceAway*zoomFactor));

		if(skyCam.enabled)
		{
			cameraPivot.localEulerAngles = Vector3.zero;
		}
		else if (Input.GetKey (KeyCode.B))
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

	void ZoomCamera()
	{
		if (Input.GetKey(KeyCode.M))
		{
			zoomFactor = zoomFactor /= 1.05f;
			if(zoomFactor <= 0.25f)
				zoomFactor = 0.25f;
		}
		else if (Input.GetKey(KeyCode.Comma))
		{
			zoomFactor = zoomFactor *= 1.05f;
			if(zoomFactor >= 1)
				zoomFactor = 1.0f;
		}
	}
}
