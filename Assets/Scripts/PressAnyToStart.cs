using UnityEngine;
using System.Collections;

public class PressAnyToStart : MonoBehaviour {

	public string nextSceneName = "HighSchool";

	void Update () 
	{
		if (Input.anyKey)
			Application.LoadLevel(nextSceneName);
	}
}
