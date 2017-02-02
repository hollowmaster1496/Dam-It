using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {

	[SerializeField] private GameObject[] cameras = new GameObject[2];
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown("1") && cameras[0] != null) 
		{
			switchCameras(0);
		} 
		else if (Input.GetKeyDown("2") && cameras[1] != null) 
		{
			switchCameras(1);
		}
	}
	
	private void switchCameras(int keyNum) {
		for (int i = 0; i < cameras.Length-1; i++) {
			if (cameras[i] != null && keyNum != i) {
				// turn camera off
				cameras[i].GetComponent<Camera>().enabled = false;
			} else {
				// turn camera on
				cameras[i].GetComponent<Camera>().enabled = true;
			}
		}
	}
}
