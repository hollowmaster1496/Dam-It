using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public MainPlayerMovement player;



	// Use this for initialization
	void Start () {
		player = FindObjectOfType<MainPlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			player.inWater = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			player.inWater = false;
		}
	}
}
