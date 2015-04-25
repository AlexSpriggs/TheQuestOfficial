using UnityEngine;
using System.Collections;

public class musicManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioClip insideMusic;
	public AudioClip outsideMusic;

	public bool havePlayerBehaviorScript = false;

	public bool isLowerVolume = true;
	public bool isRaiseVolume = true;

	private GameManager GM;
	private PlayerBehavior PB;

	// Use this for initialization
	void Start () {
		musicSource = (AudioSource)gameObject.AddComponent ("AudioSource");

		GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();



		insideMusicFunction();
	}
	
	// Update is called once per frame
	void Update () {
		if(!havePlayerBehaviorScript && Application.loadedLevel == 1){
			PB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
			havePlayerBehaviorScript = true;
		}

		swapMusic();

		//scene ending
	}

	public void insideMusicFunction(){
		musicSource.clip = insideMusic;
		musicSource.Play();
		musicSource.loop = true;
	}

	public void outsideMusicFunction(){
		musicSource.clip = outsideMusic;
		musicSource.Play();
		musicSource.loop = true;
	}

	//TODO FIGURE OUT HOW TO GET MUSIC VOLUME TO LOWER WHEN TRIGGERED TO

	public void swapMusic(){
		if (GM.isFirstPlaythrough == false) {
			if(isLowerVolume == true){
			print("asdjfkljakdsdfdfddfdfdfdfdf");
			musicSource.volume -= 0.3f * Time.deltaTime;
			if (musicSource.volume < 0f) {
				musicSource.volume = 0f;
				isLowerVolume = false;
			}
			}
		}
		if (PB.sceneEnding == true) {
			if (isRaiseVolume == true) {
				print ("adsjklfjlk;asd");
				musicSource.volume += 0.3f * Time.deltaTime;
				if (musicSource.volume >= 0.5f) {
					musicSource.volume = 0.5f;
					isRaiseVolume = false;
				}
				outsideMusicFunction ();
			}
		}
	}
}
