using UnityEngine;
using System.Collections;

public class MoveIndicator : MonoBehaviour {

    public void EnableIndicator(Vector2 targetPosition)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.position = targetPosition;
    }

    public void DisableIndicator()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
