using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Movement : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	//public Vector2 speed = new Vector2(50, 50);
	public float speed = 1;
	// 2 - Store the movement
	private Vector2 movement;
	
	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 4 - Movement per direction
		//movement = new Vector2(
			//speed.x * inputX,
			//speed.y * inputY);



		if (Input.GetKey (KeyCode.A)) {



				}
		if (Input.GetKey (KeyCode.D)) {
			

			
		}
		
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		//rigidbody2D.velocity = movement;
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
}
