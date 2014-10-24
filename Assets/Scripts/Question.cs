using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question : MonoBehaviour  {


	public List<string> answers;
	public List<string> happyResponses;
	public List<string> madResponses;

	public string question;
	public enum answerType
	{

		correct,
		incorrect,
		stress

	};
	public answerType AnswerOneType;
	public answerType AnswerTwoType;
	public answerType AnswerThreeType;



}
