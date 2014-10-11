using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class Movement : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(50, 50);
	
	// 2 - Store the movement
	private Vector2 movement;
	
	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 4 - Movement per direction
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);



		if (Input.GetKey (KeyCode.A)) {

			gameObject.transform.localScale = new Vector3(1,1,1);

				}
		if (Input.GetKey (KeyCode.D)) {
			
			gameObject.transform.localScale = new Vector3(-1,1,1);
			
		}
		
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}
}
