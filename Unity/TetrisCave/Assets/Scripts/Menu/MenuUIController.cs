using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

	public Animator animator;
	public ScrollRect scrollRect;
	float transitionTime = 0.3f;

	void Awake () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base.LevelMenu")) {
				ToggleMenu ();
				ScrollRectToDefault ();
			} else
				Application.Quit ();
		}
	}

	public void ToggleMenu () {
		animator.SetTrigger ("ToggleMenu");
	}

	public void ScrollRectToDefault () {
		StartCoroutine ("MoveContent");
	}

	IEnumerator MoveContent () {
		float moveTime = 0;
		while (moveTime < transitionTime) {
			scrollRect.verticalNormalizedPosition += Time.deltaTime / transitionTime;
			moveTime += Time.deltaTime;
			yield return null;
		}
	}
}
