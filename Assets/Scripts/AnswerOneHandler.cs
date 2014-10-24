﻿using UnityEngine;
using System.Collections;

public class AnswerOneHandler : MonoBehaviour {

	public Quiz curQuiz;

	public enum answerType
	{
		
		correct,
		incorrect,
		stress
		
	};

	public answerType myType;

	public void OnClick()
	{
		switch(myType)
		{
		case answerType.correct:
			curQuiz.curCorrect++;
			curQuiz.AfterAnswer (curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			break;
		case answerType.incorrect:
			curQuiz.AfterAnswer (curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			break;
		case answerType.stress:
			curQuiz.stressLevel++;
			curQuiz.AfterAnswer(curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[0]);
			break;
		}
	}
}
