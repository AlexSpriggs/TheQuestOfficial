using UnityEngine;
using System.Collections;

public class audioAmbiance : MonoBehaviour {

	public AudioSource ambianceSource;
	public AudioClip beachClip;
	public GameObject player;

	public int ambianceSwitch;
	private float absValueOfSource;
	private float absValueOfPlayer;

	bool soundGate = false;

	// Use this for initialization
	void Start () {
		ambianceSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		ambianceSource.playOnAwake = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		selectAmbiance ();

		absValueOfSource = Mathf.Abs((gameObject.transform.position.x * gameObject.transform.position.y) + 12 /*+ ambianceSource.maxDistance*/);
		absValueOfPlayer = Mathf.Abs(player.transform.position.x * player.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		//ambiancePlay ();
		//print (absValueOfSource - Mathf.Abs(player.transform.position.x * player.transform.position.y));
		//print ("Source: " + absValueOfSource);
		//print ("Player: " + Mathf.Abs(player.transform.position.x * player.transform.position.y));
		//print (soundGate);
	}

	public void selectAmbiance(){
		if (gameObject.name == "audioTriggerBeach") {
			ambianceSwitch = 1;
		}

		switch (ambianceSwitch) {
		case 1:
			ambianceSource.clip = beachClip;
			ambianceSource.rolloffMode = AudioRolloffMode.Linear;
			ambianceSource.minDistance = 0.04F;
			ambianceSource.maxDistance = 2F;
			//ambianceSource.Play();
			ambianceSource.loop = true;
			break;
		case 2:
			break;
		case 3:
			break;
		}
	}

	/*public void ambiancePlay(){
		if (soundGate == true) { 
			if (Mathf.Abs ((player.transform.position.x * player.transform.position.y)) <= absValueOfSource/*Mathf.Abs((gameObject.transform.position.x * gameObject.transform.position.y) + ambianceSource.maxDistance)) {
				ambianceSource.Play ();
			}
		}else {
			ambianceSource.Pause();
			soundGate = true;
		}
	}*/

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == gameObject) {
			ambianceSource.Play ();
			print("ok");
		}
	}
	public void OnTriggerExit2D(Collider2D col){
		if (col.gameObject == gameObject) {
			ambianceSource.Pause ();
			print("no");
		}
	}
}
