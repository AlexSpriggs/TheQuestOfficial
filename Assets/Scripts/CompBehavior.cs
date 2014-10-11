using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CompBehavior : MonoBehaviour {



	public GUISkin compSkin;
	private Rect WindowRect = new Rect(0, 0, Screen.width, Screen.height);
	public GameObject player;



	public List<string> answers;
	public List<string> PlayerResponses;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUI.skin = compSkin;
		
		GUILayout.BeginVertical();
		if (player.GetComponent<PlayerBehavior>().onComp == true) {
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu");
			
		}
		GUILayout.EndVertical();
	}
	
	private void menuFunc(int id)
	{

		//buttons 
		GUILayout.Label ("Text Display");
		
		for (int i = 0; i<answers.Count;i++)
		{
			if(GUILayout.Button(i+1 + " " + answers[i]))
			{



			}
		}


		
	}
}
