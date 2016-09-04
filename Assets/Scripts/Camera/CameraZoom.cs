using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
		
	private float zoomSpeed = 2.0f;
	
	void Update () {
		
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		transform.Translate(scroll * zoomSpeed, 0, scroll * zoomSpeed, Space.World);
	}

	public void SetZoomSpeed(float zs)
	{
		zoomSpeed = zs;
	}

	public float GetZoomSpeed()
	{
		return zoomSpeed;
	}

}
