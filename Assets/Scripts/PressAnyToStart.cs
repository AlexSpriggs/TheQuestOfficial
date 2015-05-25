using UnityEngine;
using System.Collections;

public class PressAnyToStart : MonoBehaviour {

	public string nextSceneName = "HighSchool";
	private Fader myFader;
	private bool endScene = false;
	public UIPanel UIRoot;

	void Start()
	{
		myFader = gameObject.GetComponent<Fader>();
	}

	void Update () 
	{
		if (Input.anyKey)
			endScene = true;

		if(endScene)
		{
			myFader.FadeToBlack();
			UIRoot.alpha -= 0.1f;

			// If the screen is almost black...
			if(myFader.fader.GetComponent<GUITexture>().color.a >= .80f)
			{
				Application.LoadLevel(nextSceneName);
			}
		}
	}
}
