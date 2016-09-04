using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset text; 

	public int startLine;
	public int endLine;

	public TextBoxManager textbox;


	public bool requirePress;
	private bool waitForPress;



	// Use this for initialization
	void Start () {
		textbox = FindObjectOfType<TextBoxManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (waitForPress && Input.GetKeyDown (KeyCode.Return)) 
		{
			textbox.ReloadScript(text);
			textbox.currentLine = startLine;
			textbox.endAtLine = endLine;
			
			textbox.EnableTextBox();
		}

	}

	void OnTriggerEnter(Collider other)
	{
		Animator anim = GetComponent<Animator> ();
		anim.SetBool("getAttention", true);

		if (other.tag == "Player") 
		{

			if(requirePress == true)
			{
				waitForPress = true;
				return;
			}

		}
	}

	void OnTriggerExit(Collider other)
	{

		Animator anim = GetComponent<Animator> ();
		anim.SetBool("getAttention", false);

		if(other.tag == "Player")
		{
			waitForPress = false;
		}
		return;

	}
}
