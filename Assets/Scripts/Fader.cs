using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public GameObject fader;

	void Start()
	{
		fader=GameObject.FindGameObjectWithTag("Fader");
		//_GameManager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		//FadeToClear();
	}

	void Awake ()
	{
		//fader.guiTexture.color = Color.black;
		// Set the texture so that it is the the size of the screen and covers it.
		fader.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		fader.guiTexture.color = Color.Lerp(fader.guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	public void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		fader.guiTexture.color = Color.Lerp(fader.guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
