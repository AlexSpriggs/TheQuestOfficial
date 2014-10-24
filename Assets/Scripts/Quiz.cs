using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{

	public CorrectAnswer correctAnswer;
	public IncorrectAnswer incorrectAnswer;
	public StressAnswer stressAnswer;

	public List<Question> curQuestions;
	public int stressLevel;
	public int currentQuestion;
	public int curCorrect;
	public GUISkin compSkin;
	private Rect WindowRect = new Rect (0, 0, Screen.width, Screen.height);
	public GameObject player;
	public UILabel compLabel;
	public UIButton firstAns;
		public UILabel firstButAns;
	public UIButton secondAns;
		public UILabel secondButAns;
	public UIButton thirdAns;
		public UILabel thirdButAns;


	public enum ComputerStates
	{
		Happy,
		Mad
	};
	public ComputerStates myStates;

	void Start ()
	{

		myStates = ComputerStates.Happy;
	}
	public void AfterAnswer(string response)
	{
		compLabel.text = response;
		secondAns.enabled = false;
		thirdAns.enabled = false;

		firstAns.onClick.Clear ();
		firstAns.onClick.Add (ContinueToQuestion ());
	}
	public EventDelegate ContinueToQuestion()
	{
		GoToNextQuestion (currentQuestion);

		return new EventDelegate();
	}
	public void GoToNextQuestion(int curQuestion)
	{
		int nextQuestion = curQuestion++;
		currentQuestion = nextQuestion;
		compLabel.text = curQuestions[nextQuestion].question;
		for(int i=0; i < curQuestions[nextQuestion].answers.Count; i++)
		{
			if(i == 0)
			{
				firstButAns.text = curQuestions[nextQuestion].answers[i];

				firstAns.onClick.Clear();
				switch(curQuestions[nextQuestion].myType)
				{
				case Question.answerType.correct:
					firstAns.onClick.Add(correctAnswer.nextQuestion(i));
					break;
				case Question.answerType.incorrect:
					firstAns.onClick.Add(incorrectAnswer.nextQuestion(i));
					break;
				case Question.answerType.stress:
					firstAns.onClick.Add(stressAnswer.nextQuestion(i));
					break;
				}
			}
			if(i == 1)
			{
				secondButAns.text = curQuestions[nextQuestion].answers[i];
				
				secondAns.onClick.Clear();
				switch(curQuestions[nextQuestion].myType)
				{
				case Question.answerType.correct:
					secondAns.onClick.Add(correctAnswer.nextQuestion(i));
					
					break;
				case Question.answerType.incorrect:
					secondAns.onClick.Add(incorrectAnswer.nextQuestion(i));
					break;
				case Question.answerType.stress:
					secondAns.onClick.Add(stressAnswer.nextQuestion(i));
					break;
				}
			}
			if(i == 2)
			{
				thirdButAns.text = curQuestions[nextQuestion].answers[i];

				thirdAns.onClick.Clear();
				switch(curQuestions[nextQuestion].myType)
				{
				case Question.answerType.correct:
					thirdAns.onClick.Add(correctAnswer.nextQuestion(i));
					
					break;
				case Question.answerType.incorrect:
					thirdAns.onClick.Add(incorrectAnswer.nextQuestion(i));
					break;
				case Question.answerType.stress:
					thirdAns.onClick.Add(stressAnswer.nextQuestion(i));
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

