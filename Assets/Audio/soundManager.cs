using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public bool isClickSound = false;
	public bool isComputerOn = false;
	//public bool isDialog = false;

	public bool doorOpen = false;

	public bool isTextScrolling = false;

	public AudioSource soundSource1;
	public AudioSource soundSource2;
	public AudioSource soundSource3;
	public AudioClip clickSound;
	public AudioClip computerOn;
	//public AudioClip dialogSound;
	public AudioClip doorSound;
	public AudioClip textScrollingSound;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start(){
		soundSource1 = (AudioSource)gameObject.AddComponent <AudioSource>();
		soundSource2 = (AudioSource)gameObject.AddComponent <AudioSource>();
		soundSource3 = (AudioSource)gameObject.AddComponent <AudioSource>();
	}

	public void GUISounds(){
		if (isClickSound == true) {
			soundSource1.clip = clickSound;
			soundSource1.PlayOneShot(clickSound);
			isClickSound = false;
			print("balls");
		}

		if (isComputerOn == true) {
			soundSource2.clip = computerOn;
			soundSource2.PlayOneShot(computerOn);
			isComputerOn = false;
			print("balls");
		}

		/*if (isDialog == true) {
			soundSource.clip = dialogSound;
			soundSource.PlayOneShot(dialogSound);
			isDialog = false;
			print("balls");
		}*/
	}

	public void BedroomSounds(){
		if (doorOpen == true) {
			soundSource3.clip = doorSound;
			soundSource3.PlayOneShot(doorSound);
			isClickSound = false;
			print("balls");
		}
	}

	public void NPCSounds(){
		if(isTextScrolling == true){
			soundSource1.clip = textScrollingSound;
			soundSource1.PlayOneShot(textScrollingSound);
			isTextScrolling = false;
			print ("doing the thing");
		}
	}
}
