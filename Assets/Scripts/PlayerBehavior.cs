using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * NOTE: For updating player's colors in the outside scene
 * - Make sure that the animators "player_bw+0, player_bw+1,..., player_bw+4" is placed
 *   in the Player Behavior inspector under Anim_controllers
 */

public class PlayerBehavior : MonoBehaviour {

	private soundManager SoundManager;

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
	public bool sceneEnding = false;
	private int nextSceneNumber;
	private const int outsideSceneNumber = 4;
	private GameManager _GameManager;
    private bool doorOpenSoundPlayed = false;
#endregion

#region OutsideVariables
	[Header("Outside Variables")]
	public bool isOutside = false;
    public GameObject dialogBox;
    public GameObject computerUIRoot;
    public GameObject inputField;
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
		}
		else
		{
	        _Movement = gameObject.GetComponent<Movement>();
	        _TypewriterEffect = dialogBox.GetComponent<TypewriterEffect>();
	        dialogUILabel = dialogBox.GetComponent<UILabel>();
            animator = gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = anim_controllers[colorLevel];
        }

        SoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<soundManager>();
	}

	void Awake ()
	{
		if (!isOutside)
		{
			fader.GetComponent<GUITexture>().color = Color.black;
			// Set the texture so that it is the the size of the screen and covers it.
			fader.GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		}
	}

	void Update () {
		if (!isOutside)
		{
			// If the scene is starting...
			if(sceneStarting)
				StartScene();
			// ... call the StartScene function.
		}
		else if (isTalking && Input.GetMouseButtonDown(0))
        {
			DialogTriggered();
        }

        if (sceneEnding)
            EndScene();

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
        if (SoundManager != null)
        {
            SoundManager.isComputerOn = true;
            SoundManager.GUISounds();
            SoundManager.isComputerOn = false;
        }
        else
        {
            Debug.LogWarning("SoundManager is null");
        }

		this.GetComponent<Movement>().speed = 0;	
		onComp = true;

        if (questUIFirstHalf != null)
        { 
		    questUIFirstHalf.SetActive(false);
        } 

        if (computerUIRoot != null)
        {
            computerUIRoot.SetActive(true);
        }
        else if (uiRoot != null)
        {
            uiRoot.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Both uiRoot and computerUIRoot are null. Unable to activate computer interface");
        }
	}
	
	void BedTriggered() 
	{
		nextSceneNumber = Application.loadedLevel + 1;
		sceneEnding = true;
	}
	
	void DoorTriggered() 
	{
		//Audio for Door
        if (!doorOpenSoundPlayed)
        { 
		    //SoundManager.doorOpen = true;
		    SoundManager.BedroomSounds(true);
		    //SoundManager.doorOpen = false;
            doorOpenSoundPlayed = true;
        }

		OutsideScene = true;
		sceneEnding = true;
	}
		
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		fader.GetComponent<GUITexture>().color = Color.Lerp(fader.GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		fader.GetComponent<GUITexture>().color = Color.Lerp(fader.GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(fader.GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			fader.GetComponent<GUITexture>().color = Color.clear;
			fader.GetComponent<GUITexture>().enabled = false;

			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		fader.GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack();

		// If the screen is almost black...
		if(fader.GetComponent<GUITexture>().color.a >= 0.49f)
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
                _Movement.SetIsMouseOnInteractable(false);
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

    public void triggerEnding()
    {
        sceneEnding = true;
        nextSceneNumber = Application.loadedLevel + 1;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
#endregion
}
