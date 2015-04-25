using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public bool isClickSound = false;
	public bool isComputerOn = false;
	//public bool isDialog = false;

	public bool doorOpen = false;

	public bool isTextScrolling = false;

	public AudioSource soundSource;
	public AudioClip clickSound;
	public AudioClip computerOn;
	//public AudioClip dialogSound;
	public AudioClip doorSound;
	public AudioClip textScrollingSound;

	void Start(){
		soundSource = (AudioSource)gameObject.AddComponent ("AudioSource");
	}

	public void GUISounds(){
		if (isClickSound == true) {
			soundSource.clip = clickSound;
			soundSource.PlayOneShot(clickSound);
			isClickSound = false;
			print("balls");
		}

		if (isComputerOn == true) {
			soundSource.clip = computerOn;
			soundSource.PlayOneShot(computerOn);
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
			soundSource.clip = doorSound;
			soundSource.PlayOneShot(doorSound);
			isClickSound = false;
			print("balls");
		}
	}

	public void NPCSounds(){
		if(isTextScrolling == true){
			soundSource.clip = textScrollingSound;
			soundSource.PlayOneShot(textScrollingSound);
			isTextScrolling = false;
			print ("doing the thing");
		}
	}
}
