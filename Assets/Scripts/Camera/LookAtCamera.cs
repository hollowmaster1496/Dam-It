using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject target;
	Vector3 movement;


	void FixedUpdated()
	{

	}

	void LateUpdate() {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		movement.Set(h, 0, v);

		movement = movement.normalized * 16.0f * Time.deltaTime;

		//movement = movement.normalized * target.transform. * Time.deltaTime;

		transform.position = transform.position + movement;

		transform.LookAt(target.transform);

	}

}
