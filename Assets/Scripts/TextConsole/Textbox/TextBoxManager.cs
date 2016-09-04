using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public Text text; 

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public MainPlayerMovement player;

	public bool isActive;
	public bool stopPlayerMotion;

	// Use this for initialization
	void Start () {
	
		player = FindObjectOfType<MainPlayerMovement> ();
		//stopPlayerMotion = false;

		if (textFile != null) 
		{

			textLines = (textFile.text.Split('\n'));
		}

		if (endAtLine == 0) 
		{
			endAtLine = textLines.Length - 1;
		}


		if (isActive) 
		{
			EnableTextBox ();
		} 
		else 
		{
			DisableTextBox();
		}
	}
	
	// Update is called once per frame
	void Update () {

		//Check if textbox should be active
		if (!isActive) 
		{
			return;
		}

		text.text = textLines [currentLine];

		if (Input.GetKeyDown (KeyCode.M))
		{
			currentLine += 1;
		}

		if (currentLine > endAtLine) 
		{
			//textBox.SetActive(false);
			DisableTextBox();
		}
	}


	public void EnableTextBox()
	{
		textBox.SetActive (true);
		isActive = true;

		if(stopPlayerMotion == true)
		{
			player.canMove = false;

		}

	}

	public void DisableTextBox()
	{
		textBox.SetActive (false);
		isActive = false;
		player.canMove = true;
	}

	public void ReloadScript(TextAsset textInput)
	{
		if (textInput != null) 
		{
			textLines = new string[1];
			textLines = (textInput.text.Split('\n'));
		}
	}
}
