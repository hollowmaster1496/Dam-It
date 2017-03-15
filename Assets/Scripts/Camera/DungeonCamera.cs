using UnityEngine;
using System.Collections;

public class DungeonCamera : MonoBehaviour {
	public GameObject target;
	public float damping = 50;
	Vector3 offset;
	
	void Start() 
	{
		offset = transform.position - target.transform.position;
	}

	void LateUpdate() 
	{
		Vector3 desiredPosition = target.transform.position + offset;
		Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

		Quaternion desiredRotation = new Quaternion (0, target.transform.rotation.y, 0, 0);

		this.transform.position = position;

		// target.transform.rotation.y;
		//transform.RotateAround (target.transform.position, Vector3.up, target.transform.rotation.y);
		//transform.Rotate (target.transform.rotation.eulerAngles);
		//this.transform.LookAt(target.transform);
		this.transform.rotation = desiredRotation; //Quaternion.Slerp (this.transform.rotation, desiredRotation, 1f);
	}
}
