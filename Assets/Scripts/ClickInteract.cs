using UnityEngine;
using System.Collections;

public class ClickInteract : MonoBehaviour {
	
	public GameObject player;
	
	private Movement _Movement;
	private SpriteRenderer spriterenderer;
	private float highlightSinValue;		// tracks how dark to color the interactable object (by using sine and incrementing till PI)
	private float highlightFrequency;		// affects how fast interactable objects flash
	private float colorFlashRange;
	private float baseColorValue;
	private const float maxColorValue = 255.0f;
	
	void Start() {
		_Movement = player.GetComponent<Movement>();
		spriterenderer = gameObject.GetComponent<SpriteRenderer>();
		
		highlightSinValue = 0.0f;
		highlightFrequency = 0.03f;
		colorFlashRange = 50.0f;
		baseColorValue = maxColorValue - colorFlashRange;
	}
	
	void Update() {
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			flashSprite();
		else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			resetSpriteColor();
	}
	
	void OnMouseDown() {
		_Movement.setTargetColliderTag (transform.tag);
	}
	
	void OnMouseOver() {
		flashSprite();
	}
	
	void OnMouseExit() {
		resetSpriteColor();
	}
	
	void flashSprite() {
		highlightSinValue += highlightFrequency;
		if (highlightSinValue > Mathf.PI)	highlightSinValue = 0.0f;
		
		float color = baseColorValue + colorFlashRange * Mathf.Sin(highlightSinValue);
		color = color / maxColorValue;		// normalize color to a range of 0 to 1
		
		spriterenderer.color = new Color(color, color, color);
	}
	
	void resetSpriteColor() {
		spriterenderer.color = Color.white;
	}
}
