using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public bool onComp = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "CompTrig" && Input.GetKey(KeyCode.E) && onComp == false) {
			this.GetComponent<Movement>().speed = 0;	
			onComp = true;
		}
		if (col.gameObject.tag == "CompTrig" && Input.GetKey(KeyCode.Escape) && onComp == true) {
			this.GetComponent<Movement>().speed = 1;
			onComp = false;
		}
	}

}
