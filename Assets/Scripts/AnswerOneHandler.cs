using UnityEngine;
using System.Collections;

public class AnswerOneHandler : MonoBehaviour {

	private soundManager SM;

	public Quiz curQuiz;
	public GameManager myManager;

	public enum answerType
	{
		
		correct,
		incorrect,
		stress
		
	};

	public answerType myType;
	void Start()
	{
		myManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		SM = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<soundManager>();
	}
	public void OnClick()
	{
		//Referencing AudioScript
		SM.isClickSound = true;
		SM.GUISounds ();
		SM.isClickSound = false;

		switch(myType)
		{
		case answerType.correct:
			curQuiz.curCorrect++;
			myManager.StressLevel--;
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[0]);
			break;
		case answerType.incorrect:
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[0]);
			break;
		case answerType.stress:
			myManager.StressLevel++;
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[0]);
			break;
		}
	}
}
