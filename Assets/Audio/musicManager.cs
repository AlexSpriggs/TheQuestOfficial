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
	
	private GameManager GameManager;
	private PlayerBehavior PlayerBehavior;

	void Awake(){
        DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		musicSource1 = (AudioSource)gameObject.AddComponent <AudioSource>();

		GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		if (Application.loadedLevel != 4) {
			insideMusicFunction ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!havePlayerBehaviorScript && Application.loadedLevel == 1){
			PlayerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
			havePlayerBehaviorScript = true;
		}

		if (isLowerVolume == true) {
			swapMusic ();
		}

		if (Application.loadedLevel == 4) {
			outsideMusicFunction ();		
		}
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
		if (GameManager.isFirstPlaythrough == false && PlayerBehavior.sceneEnding == true && isLowerVolume == true) {
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
