using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	private soundManager SM;
	
	public bool onComp = false;
	public GameObject mainCam;
	public GameObject uiRoot;
	public GameObject questUIFirstHalf;
	public GameObject questUISecondHalf;
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public GameObject fader;
	public bool OutsideScene = false;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private bool sceneEnding = false;
	private int nextSceneNumber;
	private const int outsideSceneNumber = 4;
	private GameManager _GameManager;
	
	void Start()
	{
		fader=GameObject.FindGameObjectWithTag("Fader");
		_GameManager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<soundManager>();
	}

	void Awake ()
	{
		fader.guiTexture.color = Color.black;
		// Set the texture so that it is the the size of the screen and covers it.
		fader.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void Update () {
		// If the scene is starting...
		if(sceneStarting)
			StartScene ();
		// ... call the StartScene function.
		if(sceneEnding)
			EndScene();
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKey(KeyCode.E)) 
		{
			Trigger (col.gameObject.tag);
		}
	}

	void ComputerTriggered() 
	{
		SM.isComputerOn = true;
		SM.GUISounds();
		SM.isComputerOn = false;
		this.GetComponent<Movement>().speed = 0;	
		onComp = true;
		questUIFirstHalf.SetActive(false);
		uiRoot.SetActive(true);
	}
	
	void BedTriggered() 
	{
		Debug.Log ("Bed Triggered");
		nextSceneNumber = Application.loadedLevel + 1;
		sceneEnding = true;
	}
	
	void DoorTriggered() 
	{
		OutsideScene = true;
		sceneEnding = true;
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
		// Make sure the texture is enabled.
		fader.guiTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
		
		// If the screen is almost black...
		if(fader.guiTexture.color.a >= .80f)
		{
			if (OutsideScene)
			{
				Application.LoadLevel(outsideSceneNumber);
			}
			else
			{
				// ... reload the level.
				Application.LoadLevel(nextSceneNumber);
			}
		}
	}

	public void Trigger(string tag) 
	{
		switch(tag)
		{
		case "CompTrig":
			if(!onComp) ComputerTriggered();
			break;
		case "BedTrig":
			if(mainCam.GetComponent<Quiz>().QuizComplete) BedTriggered();
			break;
		case "DoorTrig":
			if(!_GameManager.checkIsFirstPlaythrough()) DoorTriggered();
			break;
		}
	}
}
