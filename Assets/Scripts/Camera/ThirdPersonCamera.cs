using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	private float distanceAway = 30;
	private float distanceUp = 1;
	private float smooth = 10;
	private Transform follow;
	//private Vector3 targetPosition;
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
		//distanceAway *= cameraPivot.rigidbody.

	}


	//Debugging info 
	void onDrawGizmos()
	{

	}

	void LateUpdate()
	{
		cameraPivot.position = follow.position;

		scrollFactor = ZoomCamera ();

		targetPosition.position = cameraPivot.position + (Vector3.up * distanceUp) - (Camera.main.transform.TransformDirection(Vector3.forward) * distanceAway);


		/*Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay(follow.position, -1f* Vector3.forward * distanceAway, Color.green);
		Debug.DrawLine (follow.position, targetPosition.position, Color.magenta); */

		if (Input.GetKey (KeyCode.B)) {
			cameraPivot.RotateAround(cameraPivot.position, Vector3.up, 3.0f);
			//targetPosition.position = cam
			//targetPosition.position.Set(this.transform.position.x, this.transform.position.y, this.transform.position.z);

			//targetPosition.position = targetPosition.position;//transform.Rotate(Vector3.up);
			//Rotate (25f, 25f);
		}
		else if (Input.GetKey (KeyCode.N)) {
			//targetPosition.RotateAround(follow.position, Vector3.up, -100.0f);
			cameraPivot.RotateAround(cameraPivot.position, Vector3.up, -3.0f);
		}
		else
		{
			this.transform.position = Vector3.Lerp (transform.position, targetPosition.position, smooth);//targetPosition;

		}

		//transform.LookAt (follow);
		//transform.rotation = transform.localRotation;


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
		//float scroll = Input.GetAxis("Mouse ScrollWheel");
		//float scroll = Input.GetAxis ("Vertical");
		float scroll = Input.GetAxis ("Fire1");
		if (0.6f < scroll && scroll < 1.0f) 
		{
			Debug.Log("Scroll:" + scroll);
			distanceAway = 35f * Mathf.Abs (scroll);
		}



		//transform.Translate(scroll * zoomSpeed, 0, scroll * zoomSpeed, Space.World);
		return scroll + 1;
	}
}
