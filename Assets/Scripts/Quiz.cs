using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{

	public List<Question> curQuestions;
	public int stressLevel;
	public int currentQuestion;
	public int curCorrect;
	public GUISkin compSkin;
	private Rect WindowRect = new Rect (0, 0, Screen.width, Screen.height);
	public GameObject player;
	public UILabel compLabel;
	public GameObject firstAns;
		public UILabel firstButAns;
	public GameObject secondAns;
		public UILabel secondButAns;
	public GameObject thirdAns;
		public UILabel thirdButAns;

	public AnswerOneHandler ansOneHandle;
	public AnswerTwoHandler ansTwoHandle;
	public AnswerThreeHandler ansThreeHandle;

	public GameObject continueButton;


	public enum ComputerStates
	{
		Happy,
		Mad
	};
	public ComputerStates myStates;

	void Start ()
	{
		myStates = ComputerStates.Happy;
		GoToNextQuestion(0);
	}
	public void AfterAnswer(string response)
	{
		compLabel.text = response;
		firstAns.SetActive(false);
		secondAns.SetActive(false);
		thirdAns.SetActive(false);
		continueButton.SetActive(true);
	}
	public void ContinueToQuestion()
	{
		firstAns.SetActive(true);
		secondAns.SetActive(true);
		thirdAns.SetActive(true);
		continueButton.SetActive(false);
		GoToNextQuestion (currentQuestion + 1);
	}
	public void GoToNextQuestion(int curQuestion)
	{
		int nextQuestion = curQuestion;
		currentQuestion = nextQuestion;
		compLabel.text = curQuestions[nextQuestion].question;
		for(int i=0; i < curQuestions[nextQuestion].answers.Count; i++)
		{
			if(i == 0)
			{
				firstButAns.text = curQuestions[nextQuestion].answers[i];

				switch(curQuestions[nextQuestion].AnswerOneType)
				{
				case Question.answerType.correct:
					ansOneHandle.myType = AnswerOneHandler.answerType.correct;
					break;
				case Question.answerType.incorrect:
					ansOneHandle.myType = AnswerOneHandler.answerType.incorrect;
					break;
				case Question.answerType.stress:
					ansOneHandle.myType = AnswerOneHandler.answerType.stress;
					break;
				}
			}
			if(i == 1)
			{
				secondButAns.text = curQuestions[nextQuestion].answers[i];

				switch(curQuestions[nextQuestion].AnswerTwoType)
				{
				case Question.answerType.correct:
					ansTwoHandle.myType = AnswerTwoHandler.answerType.correct;
					break;
				case Question.answerType.incorrect:
					ansTwoHandle.myType = AnswerTwoHandler.answerType.incorrect;
					break;
				case Question.answerType.stress:
					ansTwoHandle.myType = AnswerTwoHandler.answerType.stress;
					break;
				}
			}
			if(i == 2)
			{
				thirdButAns.text = curQuestions[nextQuestion].answers[i];

				switch(curQuestions[nextQuestion].AnswerThreeType)
				{
				case Question.answerType.correct:
					ansThreeHandle.myType = AnswerThreeHandler.answerType.correct;
					break;
				case Question.answerType.incorrect:
					ansThreeHandle.myType = AnswerThreeHandler.answerType.incorrect;
					break;
				case Question.answerType.stress:
					ansThreeHandle.myType = AnswerThreeHandler.answerType.stress;
					break;
				}
			}
		}


	}
		

/*		void OnGUI ()
		{
				GUI.skin = compSkin;
		
				GUILayout.BeginVertical ();
				if (player.GetComponent<PlayerBehavior> ().onComp == true) {
						WindowRect = GUI.Window (0, WindowRect, menuFunc, "Computer");
			
				}
				GUILayout.EndVertical ();
		}
*/
/*	
		private void menuFunc (int id)
		{
			//buttons 
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

		}


*/
}

