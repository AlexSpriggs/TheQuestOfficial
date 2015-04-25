using UnityEngine;
using System.Collections;

public class AnswerTwoHandler : MonoBehaviour {

	private soundManager SM;

	public Quiz curQuiz;
	public GameManager myManager;
	private float currentCorrect;
	
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

		SetCorrectAmount();
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
			if(currentCorrect == curQuiz.curCorrect){
				curQuiz.curCorrect++;}
			myManager.StressLevel--;
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[1]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[1]);
			break;
		case answerType.incorrect:
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[1]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[1]);
			break;
		case answerType.stress:
			myManager.StressLevel++;
			if(myManager.myState == GameManager.ComputerStates.Happy)
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[1]);
			else
				curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].madResponses[1]);
			break;
		}
	}

	public void SetCorrectAmount()
	{
		currentCorrect = curQuiz.curCorrect;
	}
}
