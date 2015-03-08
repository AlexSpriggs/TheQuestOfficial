using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialog : MonoBehaviour {
	public List<string> dialog;
	private int index = 0;

	// Displays the next string of dialog and return true. If there aren't any new lines of dialog, set index to 0 and return false
	public bool TriggerDialog(GameObject dialogBox, UILabel dialogUILabel, TypewriterEffect _TypewriterEffect) 
	{
		if (index >= dialog.Count) 
		{
			index = 0;
			dialogUILabel.text = dialog[index];
			dialogBox.SetActive(false);
			return false;
		}

		dialogUILabel.text = dialog[index];
		dialogBox.SetActive(true);
		_TypewriterEffect.ResetToBeginning();

		++index;

		return true;
	}
}
