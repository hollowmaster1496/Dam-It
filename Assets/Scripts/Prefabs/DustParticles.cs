using UnityEngine;
using System.Collections;

public class DustParticles : MonoBehaviour {

	public GameObject RunningDust; 

	// Use this for initialization
	void Start () {
		Instantiate(RunningDust);
	}

}
