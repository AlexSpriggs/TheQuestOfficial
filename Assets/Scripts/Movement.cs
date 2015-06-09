using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Player controller
/// </summary>
public class Movement : MonoBehaviour
{
	public float speed = 3;

    // THINGS TO NOTE:
    // 1) Make sure the Ray Origin objects are aligned with the player sprite
    // 2) Make sure the objects have been dragged into the script's inspector (there should be at least 10)
    // 3) The player sprite must also have a trigger collider that is slightly bigger than itself
    // 4) Include the place into this script's inspector the MoveIndicator prefab, to enable the move indication feature

    // These are empty game objects aligned with the top left / right / mid, btm left / right / mid, and mid left x2 / right x2 of the player sprite
    // We will use the locations of these objects to fire rays from to check for obstacle collision
    public List<GameObject> rayOrigins;
    public MoveIndicator _MoveIndicator;
    public bool useOffset = true;

	private Vector3 direction;
	private Vector3 clickPosition;
	private bool isMoving = false;
    private bool isMouseOnInteractable = false;
	private float minDistanceToClick = 0.01f;
    private string targetColliderTag = "";
    [SerializeField] private float offsetValue = 0.3f;
    private Vector3 moveoffset; // offsets clickPosition, so that the sprite's feet is aligned with clickPosition (instead of the sprite's origin)

	private PlayerBehavior _PlayerBehavior;
	private Animator animator;

    void Start()
    {
        _PlayerBehavior = gameObject.GetComponent<PlayerBehavior>();
        animator = gameObject.GetComponent<Animator>();

        moveoffset = Vector3.zero;
        if (useOffset)
        {
            moveoffset.y = offsetValue;
        }
    }

	void FixedUpdate()
	{	
		MouseMovement ();
	}

	void KeyBoardMovement()
	{
		if (Input.GetKey(KeyCode.A)) 
		{
			transform.Translate(new Vector3(-speed,0,0) * Time.deltaTime);
			gameObject.transform.localScale = new Vector3(1,1,1);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(speed,0,0) * Time.deltaTime);
			gameObject.transform.localScale = new Vector3(-1,1,1);
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate(new Vector3(0,speed,0) * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate(new Vector3(0,-speed,0) * Time.deltaTime);
		}
	}

	void MouseMovement()
	{
		if (isMoving)
        {
            transform.Translate(direction * Time.fixedDeltaTime * speed);
            if (IsReachedTarget()) 
				stopMoving();
		}
        if (Input.GetMouseButton(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!isMouseOnInteractable && _MoveIndicator)
            {
                _MoveIndicator.EnableIndicator(clickPosition);
                targetColliderTag = "";
            }
            else // if mouse is on interactable when clicking, then the move indicator is already active, so use its position
            {
                clickPosition = _MoveIndicator.transform.position;
            }

            clickPosition += moveoffset;
            direction = GetDirection();
            isMoving = true;
            UpdateAnimatorMovementVars();
            UpdateAnimatorIsMoving();

        }
	}

    Vector3 GetDirection()
    {
        Vector3 dir = new Vector3(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y, 0);
        dir.Normalize();
        return dir;
    }

    // Check if player is within proximity of the click position
	bool IsReachedTarget()
	{
		Vector3 distance = new Vector3(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y, 0);
		if (distance.magnitude < minDistanceToClick)
			return true;
		else 
			return false;
	}

    // Start raycasting for obstacles if player is moving AND there are objects within the player's trigger zone (so that we don't have to raycast redundantly if there's no obtacles nearby)
    void OnTriggerStay2D(Collider2D col)
    {
        if (isMoving && IsRaycastHit())
        {
            stopMoving();
        }
    }

    bool IsRaycastHit()
    {
        // Fire a ray from each object in the rayOrigins list
        foreach (GameObject origin in rayOrigins)
        {
            RaycastHit2D ray = Physics2D.Raycast(origin.transform.position, direction, 0.1f);
            if (ray.collider != null)
            {
                // If player did not click on an interactable object, and player has collided with an obstacle
                if (targetColliderTag == "")
                {
                    return true;
                }
                // If player did not click on an interactable object, and has collided with the object
                else if (ray.transform.tag == targetColliderTag)
                {
                    _PlayerBehavior.Trigger(targetColliderTag, ray.collider.gameObject.GetComponent<Collider2D>());
                    targetColliderTag = "";
                    return true;
                }
            }
        }

        return false;
    }

    // Called by the interactable object that the player clicked on
	public void setTargetColliderTag(string tag) 
    {
        targetColliderTag = tag;
    }

    // There are 2 situations where stopMoving() will be called:
    // 1) Player has reached the location that was clicked on
    // 2) Player has raycast-collided with an obstacle
	public void stopMoving() { 
		isMoving = false;
		UpdateAnimatorIsMoving();

        if (_MoveIndicator)
        {
            _MoveIndicator.DisableIndicator();
        }
	}

    public void UpdateAnimatorIsMoving()
    {
        animator.SetBool("is_moving", isMoving);
    }

    // Helps the animator determine which direction to animate the player towards
    public void UpdateAnimatorMovementVars()
    {
        animator.SetFloat("dir_x", direction.x);
        animator.SetFloat("dir_y", direction.y);
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) animator.SetBool("is_y_greaterthan_x", true);
        else animator.SetBool("is_y_greaterthan_x", false);
    }

    public void SetIsMouseOnInteractable(bool isMouse)
    {
        isMouseOnInteractable = isMouse;
    }

    public void SetMoveIndicator(Vector2 targetPosition)
    {
        _MoveIndicator.EnableIndicator(targetPosition);
    }
}
