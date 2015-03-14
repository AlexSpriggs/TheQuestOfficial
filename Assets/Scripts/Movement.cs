using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Movement : MonoBehaviour
{
	public float speed = 3;

	private Vector2 collPosition;
	private Vector3 direction;
	private Vector3 clickPosition;
	private bool isMoving = false;
	private float minDistanceToClick = 0.01f;
	private string targetColliderTag = "";

	private PlayerBehavior _PlayerBehavior;
	private Animator animator;

	void Start() 
	{
		_PlayerBehavior = gameObject.GetComponent<PlayerBehavior> ();
		animator = gameObject.GetComponent<Animator>();
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
			if (ReachedTarget()) // if (ReachedTarget() || HitObstacle())
				stopMoving();
			transform.Translate (direction * Time.fixedDeltaTime * speed);
		}
		if (Input.GetMouseButtonDown(0))
		{
			clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			direction = new Vector3(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y, 0);
			direction.Normalize();
			isMoving = true;
			UpdateAnimatorMovementVars();
			UpdateAnimatorIsMoving();
		}
	}

	bool ReachedTarget()
	{
		Vector3 distance = new Vector3(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y, 0);
		if (distance.magnitude < minDistanceToClick)
			return true;
		else 
			return false;
	}

	bool HitObstacle()
	{
		if (collPosition.x == transform.position.x && (direction.x != 0))
			return true;
		else if (collPosition.y == transform.position.y && (direction.y != 0))
			return true;
		else
			return false;
	}
	
	void UpdateAnimatorIsMoving()
	{
		animator.SetBool("is_moving", isMoving);
	}

	void UpdateAnimatorMovementVars()
	{
		animator.SetFloat("dir_x", direction.x);
		animator.SetFloat("dir_y", direction.y);
		if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) animator.SetBool("is_y_greaterthan_x", true);
		else animator.SetBool("is_y_greaterthan_x", false);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		collPosition = transform.position;
		stopMoving();
        if (targetColliderTag == col.transform.tag)
        {
            _PlayerBehavior.Trigger(targetColliderTag, col.gameObject.GetComponent<Collider2D>());
        }
       
        targetColliderTag = "";
	}

	public void setTargetColliderTag(string tag) { targetColliderTag = tag; }
	public void stopMoving() { 
		isMoving = false;
		UpdateAnimatorIsMoving();
	}
}
