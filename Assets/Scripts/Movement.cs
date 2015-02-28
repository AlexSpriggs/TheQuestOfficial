using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Movement : MonoBehaviour
{
	public float speed = 3;

	private Vector3 direction;
	private Vector3 clickPosition;
	private bool isMoving;
	private float minDistanceToClick;
	private string targetColliderTag;
	private PlayerBehavior _PlayerBehavior;

	void Start() 
	{
		isMoving = false;
		minDistanceToClick = 0.01f;
		targetColliderTag = "";
		_PlayerBehavior = gameObject.GetComponent<PlayerBehavior> ();
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
			if (ReachedTarget())
				isMoving = false;
			transform.Translate (direction * Time.fixedDeltaTime * speed);
		}
		if (Input.GetMouseButtonDown(0))
		{
			clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			direction = new Vector3(clickPosition.x - transform.position.x, clickPosition.y - transform.position.y, 0);
			direction.Normalize();
			isMoving = true;
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
	
	void OnCollisionEnter2D(Collision2D col)
	{
		isMoving = false;
		if (targetColliderTag == col.transform.tag)
			_PlayerBehavior.Trigger(targetColliderTag);
		else
			targetColliderTag = "";
	}

	public void setTargetColliderTag(string tag) { targetColliderTag = tag; }
}
