using UnityEngine;
using System.Collections;

public class CorrectAnswer : MonoBehaviour {

	public GameObject camera;
	public Quiz curQuiz;

	// Use this for initialization
	void Start () {

		camera = GameObject.Find ("MainCamera");
		curQuiz = camera.GetComponent<Quiz> ();
	
	}
	public EventDelegate nextQuestion(int currentAnswer)
	{
		curQuiz.curCorrect++;
		curQuiz.AfterAnswer (curQuiz.curQuestions[curQuiz.currentQuestion].happyResponses[currentAnswer]);
		return new EventDelegate();
	}
	// Update is called once per frame
	void Update () {
	
	}

}
