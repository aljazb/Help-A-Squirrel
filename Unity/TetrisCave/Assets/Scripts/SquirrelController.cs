using UnityEngine;
using System.Collections;

public class SquirrelController : MonoBehaviour {

	public Animator animator;
	float jumpTime = 0.81f;

	float yDifference;

	public void Prepare () {
		animator.SetTrigger ("Prepare");
	}

	public void Relax () {
		animator.SetTrigger ("Relax");
	}

	public void Jump (float y) {
		yDifference = y - transform.position.y - 0.15f;
		StartCoroutine ("PositionCorrection");
		animator.SetTrigger ("Jump");
	}

	IEnumerator PositionCorrection () {
		float timeCount = 0;
		while (timeCount < jumpTime) {
			transform.position += Vector3.up * (Time.deltaTime * yDifference / jumpTime);
			timeCount += Time.deltaTime;
			yield return null;
		}

		timeCount = 0;
		while (timeCount < jumpTime) {
			transform.position -= Vector3.up * (Time.deltaTime * yDifference / jumpTime);
			timeCount += Time.deltaTime;
			yield return null;
		}
	}
}
