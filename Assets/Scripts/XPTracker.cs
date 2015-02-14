using UnityEngine;
using System.Collections;

public class XPTracker : MonoBehaviour {

	public UISlider xpBar;

	private float currXP = 0.0f;
	private float maxXP = 1000.0f;
	private float lerpSpeed = 2.5f;

	void Start() 
	{
		xpBar.value = 0.0f;
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) addXP(50);		// LMB - adds 5%
		if (Input.GetMouseButtonDown(1)) addXP(100);	// RMB - adds 10%
		float currPercentage = currXP / maxXP;

		xpBar.value = Mathf.Lerp(xpBar.value, currPercentage, Time.deltaTime * lerpSpeed);
	}

	public void addXP (float amt) {currXP += amt;}
}
