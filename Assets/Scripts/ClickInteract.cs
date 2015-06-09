using UnityEngine;
using System.Collections;

public class ClickInteract : MonoBehaviour {
	
	public GameObject player;

    [SerializeField] private Vector3 indicatorOffset; // set in inspector to finetune where the indicator appears, relative to the interactable object
    private bool hasBeenSpokenTo = false;
	private Movement _Movement;
	private PlayerBehavior _PlayerBehavior;
	private SpriteRenderer spriterenderer;
	private bool isTouchingPlayer = false;
	private float highlightSinValue;		// tracks how dark to color the interactable object (by using sine and incrementing till PI)
	private float highlightFrequency;		// affects how fast interactable objects flash
	private float colorFlashRange;
	private float baseColorValue;
	private const float maxColorValue = 255.0f;
	
	void Start() {
		_Movement = player.GetComponent<Movement>();
		_PlayerBehavior = player.GetComponent<PlayerBehavior>();
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
	
	void OnMouseDown() 
    {
        LMBDownOnItem();
	}
	
	void OnMouseOver() 
    {
		flashSprite();
        _Movement.SetIsMouseOnInteractable(true);

        if (Input.GetMouseButton(0))
        {
            LMBDownOnItem();
        }
	}

    void LMBDownOnItem()
    {
        _Movement.setTargetColliderTag(transform.tag);
        _Movement.SetMoveIndicator(transform.position + indicatorOffset);

        if (isTouchingPlayer)
        {
            // _Movement.stopMoving();

            if (!hasBeenSpokenTo)
            {
                // hasBeenSpokenTo = true;
                _PlayerBehavior.Trigger(transform.tag, GetComponent<Collider2D>());
            }
        }
    }

    void OnMouseExit()
    {
        resetSpriteColor();
        _Movement.SetIsMouseOnInteractable(false);
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

	void OnCollisionStay2D(Collision2D col)
	{
        if (col.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			isTouchingPlayer = false;
			// _Movement.setTargetColliderTag ("");
		}
	}
}
