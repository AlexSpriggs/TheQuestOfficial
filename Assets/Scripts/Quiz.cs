﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class Quiz : MonoBehaviour
{

	private soundManager SM;

	public List<Question> curQuestions;
	public GameManager myManager;
	public int currentQuestion;
	public float curCorrect;
	private float gpaTotal;
	public GameObject player;
	public bool QuizComplete;
	public string BeforeQuizStartsScript;
	[Header("GUI")]
	public GUISkin compSkin;
	public GameObject uiRoot;
	public GameObject questUIFirstHalf;
	public GameObject questUISecondHalf;
	//private Rect WindowRect = new Rect (0, 0, Screen.width, Screen.height);
	[Header("Answers")]
	public UILabel compLabel;
	public GameObject compLabelObject;
	public GameObject firstAns;
		public UILabel firstButAns;
	public GameObject secondAns;
		public UILabel secondButAns;
	public GameObject thirdAns;
		public UILabel thirdButAns;
	public string finalScreenText;
	[Header("Answer Handlers")]
	public AnswerOneHandler ansOneHandle;
	public AnswerTwoHandler ansTwoHandle;
	public AnswerThreeHandler ansThreeHandle;

	public GameObject continueButton;
	public GameObject quitButton;

	private string questionText;

	void Start ()
	{
		myManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		currentQuestion = -1;
		BeforeQuizStarts();
		QuizComplete = false;
		questionText = compLabel.text;

		SM = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<soundManager>();
	}
	public void BeforeQuizStarts()
	{
		compLabel.text = BeforeQuizStartsScript;
		firstAns.SetActive(false);
		secondAns.SetActive(false);
		thirdAns.SetActive(false);
		continueButton.SetActive(true);
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
		ansOneHandle.SetCorrectAmount();
		ansTwoHandle.SetCorrectAmount();
		ansThreeHandle.SetCorrectAmount();
		GoToNextQuestion (currentQuestion + 1);

		//Referencing AudioScript
		SM.isClickSound = true;
		SM.GUISounds ();
		SM.isClickSound = false;
	}
	public void EndQuiz()
	{
		//Jack how do?
		//uiRoot.SetActive(false);
		firstAns.SetActive(false);
		secondAns.SetActive(false);
		thirdAns.SetActive(false);
		quitButton.SetActive(true);

		gpaTotal = ((curCorrect/5) * 100)/25;

		if(gpaTotal!=4)
			compLabel.text = "Your GPA is: " + gpaTotal + ". " + finalScreenText;
		else
			compLabel.text = "Your GPA is: 4.0. " + finalScreenText;

		QuizComplete = true;

		//Referencing AudioScript
		SM.isClickSound = true;
		SM.GUISounds ();
		SM.isClickSound = false;

	}
	public void CloseScreen()
	{
		uiRoot.SetActive(false);
		questUISecondHalf.SetActive(true);
		player.GetComponent<Movement>().speed = 1;	
		
	}

	public void GoToNextQuestion(int curQuestion)
	{
		int nextQuestion = curQuestion;
		currentQuestion = nextQuestion;
		try
		{
			compLabel.text = curQuestions[nextQuestion].question;
			//compLabel.ProcessText();

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

			//compLabelObject.GetComponent<TypewriterEffect>().ResetToBeginning();
		}
		catch
		{
			if(curCorrect >= 3)
				EndQuiz();
			else
			{
				gpaTotal = ((curCorrect/5) * 100)/25;
				compLabel.text = "Your GPA is: " + gpaTotal + ". " +  "You have failed. Try again.";
				curCorrect = 0;
				ansOneHandle.SetCorrectAmount();
				ansTwoHandle.SetCorrectAmount();
				ansThreeHandle.SetCorrectAmount();
				currentQuestion = -1;
				firstAns.SetActive(false);
				secondAns.SetActive(false);
				thirdAns.SetActive(false);
				continueButton.SetActive(true);
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

