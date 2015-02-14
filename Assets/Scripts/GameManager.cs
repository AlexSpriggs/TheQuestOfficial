using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int StressLevel;
	public int MadResponseSwitch;
	public int HappyResponseSwitch;
	private bool isFirstPlaythrough = true;

	public enum ComputerStates
	{
		Happy,
		Mad
	};
	
	public ComputerStates myState;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}


	void Start () {

		StressLevel = 0;
		myState = ComputerStates.Happy;

	}
	

	void FixedUpdate () {

		if(StressLevel >= MadResponseSwitch && myState != ComputerStates.Mad)
			myState = ComputerStates.Mad;
		if(StressLevel <= HappyResponseSwitch && myState != ComputerStates.Happy)
			myState = ComputerStates.Happy;
	
	}

	public bool checkIsFirstPlaythrough()	{ return isFirstPlaythrough; }
	public void firstPlaythroughDone()		{ isFirstPlaythrough = false; }
}
