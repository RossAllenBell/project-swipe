using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

	void Start () {
		transform.localScale = new Vector2(0f, 1f);
	}
	
	void Update () {

	}

	public void setStartAndEnd(Vector2 start, Vector2 end) {
		transform.position = (end + start) / 2;

		float distance = Vector2.Distance (start, end);
		transform.localScale = new Vector2 (distance, 1f);

		Vector2 vectorBetween = start - end;
		float angle = (vectorBetween.y == 0? (vectorBetween.x > 0? 0f : Mathf.PI) : Mathf.Atan2(vectorBetween.y, vectorBetween.x)) * -180 / Mathf.PI;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
	}

	public void destroy() {
		Destroy (gameObject);
	}

}
