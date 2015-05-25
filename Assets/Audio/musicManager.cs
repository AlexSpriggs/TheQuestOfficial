using UnityEngine;
using System.Collections;

public class musicManager : MonoBehaviour {

	public AudioSource musicSource1;
	public AudioClip insideMusic;
	public AudioClip outsideMusic;

	public bool havePlayerBehaviorScript = false;

	//for lowering the inside music
	private bool isLowerVolume = true;
	//checks if outside music can play
	private bool isOutsideMusicPlay = false;
	
	private GameManager GM;
	private PlayerBehavior PB;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		musicSource1 = (AudioSource)gameObject.AddComponent <AudioSource>();

		GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		if (Application.loadedLevel != 4) {
			insideMusicFunction ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!havePlayerBehaviorScript && Application.loadedLevel == 1){
			PB = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
			havePlayerBehaviorScript = true;
		}

		if (isLowerVolume == true) {
			swapMusic ();
		}
		if (Application.loadedLevel == 4) {
			outsideMusicFunction ();		
		}

		print("Music Volume: " + musicSource1.volume);
		print ("Is Music Playing: " + musicSource1.isPlaying);
		print ("Is Volume Lowering: " + isLowerVolume);
		print ("Is Music playing on awake: " + musicSource1.playOnAwake);
	}

	public void insideMusicFunction(){
		if (Application.loadedLevel != 4) {
			musicSource1.clip = insideMusic;
			musicSource1.Play ();
			musicSource1.loop = true;
		}
	}

	public void outsideMusicFunction(){
		if (isOutsideMusicPlay == true) {
			musicSource1.clip = outsideMusic;
			musicSource1.volume = 0.5f;
			musicSource1.Play ();
			musicSource1.loop = true;
			isOutsideMusicPlay = false;
		}
	}

	public void swapMusic(){
		if (GM.isFirstPlaythrough == false && PB.sceneEnding == true && isLowerVolume == true) {
				musicSource1.volume -= 0.3f * Time.deltaTime;
				if (musicSource1.volume <= 0f && isLowerVolume == true) {
					musicSource1.volume = 0f;
					musicSource1.Pause();
					musicSource1.volume = 0.5f;
					isLowerVolume = false;
					isOutsideMusicPlay = true;
				}
		}
	}
}
