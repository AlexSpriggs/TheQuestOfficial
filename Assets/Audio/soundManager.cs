using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public bool isClickSound = false;
	public bool isComputerOn = false;
	public bool isDialog = false;

	public AudioSource soundSource;
	public AudioClip clickSound;
	public AudioClip computerOn;
	public AudioClip dialogSound;

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

		if (isDialog == true) {
			soundSource.clip = dialogSound;
			soundSource.PlayOneShot(dialogSound);
			isDialog = false;
			print("balls");
		}
	}
}
