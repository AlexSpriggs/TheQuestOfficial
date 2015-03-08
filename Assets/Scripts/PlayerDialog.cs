using UnityEngine;
using System.Collections;

public class PlayerDialog : MonoBehaviour {
	public GameObject dialogBox;

	// Script Objects
	private Movement _Movement;
	private TypewriterEffect _TypewriterEffect;
	private UILabel dialogUILabel;

	private bool isDialogTyping = false;		// is the npc's dialog currently being "typed out" on the screen

	void Start() {
		_Movement = gameObject.GetComponent<Movement>();
		_TypewriterEffect = dialogBox.GetComponent<TypewriterEffect>();
		dialogUILabel = dialogBox.GetComponent<UILabel>();
	}

	void OnTriggerStay2D(Collider2D coll) {
		// If player is within range and interacting with a NPC / object that has dialog
		if (coll.CompareTag("DialogTrig") && Input.GetKeyDown(KeyCode.E)) {
			if(!isDialogTyping) {				// if a new string of dialog isn't being displayed, check if there's anymore dialog to show
				if (coll.GetComponent<NPCDialog>().TriggerDialog(dialogBox, dialogUILabel, _TypewriterEffect)) {
					_Movement.enabled = false;
					isDialogTyping = true;
				}
				else {							// if there isn't, enable player movement
					_Movement.enabled = true;
					isDialogTyping = false;
				}
			}
			else if (isDialogTyping) {			// otherwise, if a new string of dialog is being "typed out", finish the typing effect
				_TypewriterEffect.Finish();
				isDialogTyping = false;
			}
		}
	}

	// The function called by TypewriterEffect once a full line of dialog has been displayed
	public void dialogStatusUpdate() {
		isDialogTyping = false;
	}
}
