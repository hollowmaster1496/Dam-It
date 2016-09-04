using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public CoinScore CoinScore;

	// Use this for initialization
	void Start () {
		CoinScore = FindObjectOfType<CoinScore> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			CoinScore.IncrementScore();
			CoinScore.UpdateScoreText();
			Destroy(gameObject);
		}
	}
}
