using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{


		public List<Question> curQuestions;
		public int stressLevel;
		public GUISkin compSkin;
		private Rect WindowRect = new Rect (0, 0, Screen.width, Screen.height);
		public GameObject player;

		public enum ComputerStates
		{
				Happy,
				Mad}
		;
		public ComputerStates myStates;

		void Start ()
		{
				myStates = ComputerStates.Happy;
		}
		void OnGUI ()
		{
				GUI.skin = compSkin;
		
				GUILayout.BeginVertical ();
				if (player.GetComponent<PlayerBehavior> ().onComp == true) {
						WindowRect = GUI.Window (0, WindowRect, menuFunc, "Computer");
			
				}
				GUILayout.EndVertical ();
		}
	
		private void menuFunc (int id)
		{
	/*			//buttons 
				GUILayout.Label (curQuestions [id].question);
				for (int ii = 0; ii < curQuestions[id].answers.Count; ii++) {
						if (GUILayout.Button (ii + 1 + " " + curQuestions [id].answers [ii])) {
								switch (myStates) {

								case ComputerStates.Happy:
										GUILayout.Label (curQuestions [id].happyResponses [ii]);
										break;
								case ComputerStates.Mad:
										GUILayout.Label (curQuestions [id].madResponses [ii]);
										break;
								}
								//Stress level needs to be recorded*
								if (GUILayout.Button ("Continue!")) {
										enabled = false;
										menuFunc (id++);
								}

						}
				}
				*/
		}



}

