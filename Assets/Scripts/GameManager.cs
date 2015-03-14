using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int StressLevel;
	public int MadResponseSwitch;
	public int HappyResponseSwitch;
	private GameObject doorTrigger;
    private GameObject room_openwindow;
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

	void OnLevelWasLoaded(int level) {
        if (level == 1 || level == 2)
        {
            doorTrigger = GameObject.FindGameObjectWithTag("DoorTrig");
            room_openwindow = GameObject.FindGameObjectWithTag("OpenWindow");
            if (doorTrigger == null || room_openwindow == null)
                Debug.LogWarning("Warning: Failed to load door trigger and open window prefabs!");
            else
            {
                if (!isFirstPlaythrough)
                {
                    doorTrigger.SetActive(true);
                    room_openwindow.SetActive(true);
                }
                else
                {
                    doorTrigger.SetActive(false);
                    room_openwindow.SetActive(false);
                }
            }
        }
	}

	public bool checkIsFirstPlaythrough()	{ return isFirstPlaythrough; }
	public void firstPlaythroughDone()		{ isFirstPlaythrough = false; }
}
