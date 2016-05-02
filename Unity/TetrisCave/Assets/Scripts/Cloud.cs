using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float minSpeed;
	public float maxSpeed;
	float border = 5.5f;
	float speed;
	
	void Awake () {
		speed = Random.Range (minSpeed, maxSpeed);
	}

	void Update () {
		transform.position += Vector3.left * (speed * Time.deltaTime);
		if (transform.position.x < -border) {
			transform.position = new Vector3 (border, transform.position.y, transform.position.z);
			speed = Random.Range (minSpeed, maxSpeed);
		}
	}
}
