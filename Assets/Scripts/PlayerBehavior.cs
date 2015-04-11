using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehavior : MonoBehaviour {

	private soundManager SM;

#region IndoorVariables
	[Header("Indoor Variables")]
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
#endregion

#region OutsideVariables
	[Header("Outside Variables")]
	public bool isOutside = false;
    public GameObject dialogBox;
    private Collider2D npcColl;

    // Script Objects
    private Movement _Movement;
    private TypewriterEffect _TypewriterEffect;
    private UILabel dialogUILabel;

    private bool isTalking = false;
    private bool isDialogTyping = false;		// is the npc's dialog currently being "typed out" on the screen

    // Player Color Variables
    public List<RuntimeAnimatorController> anim_controllers;
    private Animator animator;
    private int colorLevel = 0;
    private bool isDebug = false;
#endregion

	void Start()
	{
		if (!isOutside)
		{
			fader=GameObject.FindGameObjectWithTag("Fader");
			_GameManager=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

	        SM = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<soundManager>();
		}
		else
		{
	        _Movement = gameObject.GetComponent<Movement>();
	        _TypewriterEffect = dialogBox.GetComponent<TypewriterEffect>();
	        dialogUILabel = dialogBox.GetComponent<UILabel>();
            animator = gameObject.GetComponent<Animator>();
		}
	}

	void Awake ()
	{
		if (!isOutside)
		{
			fader.guiTexture.color = Color.black;
			// Set the texture so that it is the the size of the screen and covers it.
			fader.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		}
	}

	void Update () {
		if (!isOutside)
		{
			// If the scene is starting...
			if(sceneStarting)
				StartScene();
			// ... call the StartScene function.
			if(sceneEnding)
				EndScene();
		}
		else if (isTalking && Input.GetMouseButtonDown(0))
        {
			DialogTriggered();
        }

        // Debug only
        if (isDebug && Input.GetMouseButtonDown(1) && animator != null)
            UpdateAnimator();
	}

	public void Trigger(string tag, Collider2D col) 
	{
		switch(tag)
		{
		case "CompTrig":
			if(!onComp) 
				ComputerTriggered();
			break;
		case "BedTrig":
			if(mainCam.GetComponent<Quiz>().QuizComplete) 
				BedTriggered();
			break;
		case "DoorTrig":
			if(!_GameManager.checkIsFirstPlaythrough()) 
				DoorTriggered();
			break;
		case "DialogTrig":
			if (!isTalking)
			{
				isTalking = true;
				npcColl = col;
				DialogTriggered();

                // Prevent player from talking to NPC a second time by destroying the interaction script
                ClickInteract _ClickInteract = col.gameObject.GetComponent<ClickInteract>();
                if (_ClickInteract != null)
                    GameObject.Destroy(_ClickInteract);

                if (animator != null)
                    UpdateAnimator();
			}
			break;
		}
	}

/*
	void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKey(KeyCode.E)) 
		{
			Trigger (col.gameObject.tag, col);
		}
	}
*/

#region IndoorFunctions
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
		if(fader.guiTexture.color.a >= 0.49f)
		{
			if (OutsideScene)
			{
				Application.LoadLevel(outsideSceneNumber);
			}
			else
			{
				// ... load the level.
				Application.LoadLevel(nextSceneNumber);
			}
		}
	}
#endregion
	
#region OutsideFunctions
	void DialogTriggered()
	{
		if (!isDialogTyping)        // if a new string of dialog isn't being displayed, check if there's anymore dialog to show
		{				
			if (npcColl.GetComponent<NPCDialog>().TriggerDialog(dialogBox, dialogUILabel, _TypewriterEffect))
			{
				_Movement.enabled = false;
				isDialogTyping = true;
			}
			else                    // if there isn't, enable player movement
			{							
				_Movement.enabled = true;
				isDialogTyping = false;
				isTalking = false;
			}
			// _TypewriterEffect.ResetToBeginning();
		}
		else if (isDialogTyping)    // otherwise, if a new string of dialog is being "typed out", finish the typing effect
		{			
			_TypewriterEffect.Finish();
			isDialogTyping = false;
		}
	}

	public void dialogStatusUpdate() {
		isDialogTyping = false;
	}

    // Update the animator's runtime animation controller, i.e. the player color
    void UpdateAnimator()
    {
        if (colorLevel + 1 < anim_controllers.Count)
        {
            ++colorLevel;
            animator.runtimeAnimatorController = anim_controllers[colorLevel];
            
            // Forcing update on the animator variables do not seem to do anything
            // e.g. if player is facing right when the animator changes its controller, the player animation state reverts back to its default (facing front) 
            // _Movement.UpdateAnimatorMovementVars();
            // _Movement.UpdateAnimatorIsMoving();
        }
    }
#endregion
}
