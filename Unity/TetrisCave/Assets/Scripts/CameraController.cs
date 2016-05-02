using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {
	
	public float yOffset;
	public float speed;
	public Transform levelBottom;

	public LevelController levelController;
	public TouchController touchController;
	public Animator animator;

	float startTime = 0;
	float repeatRate = 0.6f;

	float topY;
	float tempTopY;
	float lerpThreshold = 0.3f;


	const float RATIO = 800f / 1280f;

	void Awake () {
		Camera.main.orthographicSize /= Camera.main.aspect / RATIO;

		tempTopY = levelBottom.position.y - yOffset;
		animator.SetInteger ("Level", levelController.level);
	}

	void AnimationFinished () {
		animator.enabled = false;
		InvokeRepeating ("ChangePosition", startTime, repeatRate);
		touchController.EnableTouch ();
	}

	public void DisableCamera () {
		CancelInvoke ("ChangePosition");
		StopCoroutine ("Move");
	}

	public void ReportPosition (float y, bool isMoving) {
		if (y > tempTopY && (!isMoving || y < transform.position.y - yOffset)) {
			tempTopY = y;
		}
	}

	void ChangePosition () {
		topY = tempTopY;
		tempTopY = levelBottom.position.y - yOffset;

		StopCoroutine ("Move");
		StartCoroutine ("Move");
	}

	IEnumerator Move () {
		while (Mathf.Abs(transform.position.y - topY - yOffset) > lerpThreshold) {
			transform.position = Vector3.Lerp(transform.position, 
			                                  new Vector3(transform.position.x, topY + yOffset, transform.position.z),
			                                  Time.deltaTime * speed);
			yield return null;
		}
	}
}
