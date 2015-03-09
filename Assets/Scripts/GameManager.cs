using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int StressLevel;
	public int MadResponseSwitch;
	public int HappyResponseSwitch;
	public GameObject doorTrigger;
	private bool isFirstPlaythrough = false;

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

	void OnLevelWasLoaded(int level) {
		if (level == 1 || level == 2) {
			doorTrigger = GameObject.FindGameObjectWithTag("DoorTrig");
			if (!isFirstPlaythrough && doorTrigger != null)
				doorTrigger.SetActive(true);
			else
				doorTrigger.SetActive(false);
		}
	}

	public bool checkIsFirstPlaythrough()	{ return isFirstPlaythrough; }
	public void firstPlaythroughDone()		{ isFirstPlaythrough = false; }
}
