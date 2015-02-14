using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDialog : MonoBehaviour {
	public List<string> dialog;
	private int index = 0;

	// Displays the next string of dialog and return true. If there aren't any new lines of dialog, set index to 0 and return false
	public bool TriggerDialog(GameObject dialogBox, UILabel dialogUILabel) 
	{
		if (index >= dialog.Count) 
		{
			index = 0;
			dialogUILabel.text = "";
			dialogBox.SetActive(false);
			return false;
		}

		dialogBox.SetActive(false);
		dialogUILabel.text = dialog[index];
		dialogBox.SetActive(true);

		++index;

		return true;
	}
}
