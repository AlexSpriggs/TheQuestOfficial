using UnityEngine;
using System.Collections;

public class FirstPlaythroughDone : MonoBehaviour {

	void Start () 
	{
		GameObject gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		if (gameManager != null)
			gameManager.GetComponent<GameManager>().firstPlaythroughDone();
	}
}
