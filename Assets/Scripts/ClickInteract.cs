using UnityEngine;
using System.Collections;

public class ClickInteract : MonoBehaviour {

	public GameObject player;
	private Movement _Movement;

	void Start() {
		_Movement = player.GetComponent<Movement>();
	}

	void OnMouseDown() {
		Debug.Log (transform.tag);
		_Movement.setTargetColliderTag (transform.tag);
	}
}
