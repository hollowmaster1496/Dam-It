using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour {

	public int score; 
	public Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//scoreText.text = "Score:" + score.ToString (); 
	}

	public void IncrementScore()
	{
		score++;
		return;
	}

	public void UpdateScoreText()
	{
		scoreText.text = "Coins: " + score.ToString ();
	}
}
