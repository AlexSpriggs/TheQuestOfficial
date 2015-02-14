using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public bool onComp = false;
	public GameObject mainCam;
	public GameObject uiRoot;
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public GameObject fader;
	
	private bool sceneStarting = false;      // Whether or not the scene is still fading in.

	void Start()
	{
		fader=GameObject.FindGameObjectWithTag("Fader");
	}
	void Awake ()
	{

		// Set the texture so that it is the the size of the screen and covers it.
		fader.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	void Update () {
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			EndScene();
		
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "CompTrig" && Input.GetKey(KeyCode.E) && onComp == false) {
			this.GetComponent<Movement>().speed = 0;	
			onComp = true;
			uiRoot.SetActive(true);
			//mainCam.GetComponent<Quiz>().GoToNextQuestion(0);
			//set NGUI renderer on
		}
		if (col.gameObject.tag == "BedTrig" && Input.GetKey(KeyCode.E)) {	
			if(mainCam.GetComponent<Quiz>().QuizComplete)
				sceneStarting = true;
		
		}
		//if (col.gameObject.tag == "CompTrig" && Input.GetKey(KeyCode.Escape) && onComp == true) {
			//this.GetComponent<Movement>().speed = 1;
			//onComp = false;
			//set NGUI renderer off
		//}
	}
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		fader.guiTexture.color = Color.Lerp(fader.guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		fader.guiTexture.color = Color.Lerp(fader.guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(fader.guiTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			fader.guiTexture.color = Color.clear;
			fader.guiTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		int i = Application.loadedLevel;
		// Make sure the texture is enabled.
		fader.guiTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(fader.guiTexture.color.a >= .80f)
			// ... reload the level.
			Application.LoadLevel(i + 1);
	}
	
}
