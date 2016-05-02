using UnityEngine;
using System.Collections;

public class SoundIcon : MonoBehaviour {

	public Animator animator;
	SoundController soundController = SoundController.GetInstance ();

	void Awake () {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1)
			animator.SetBool ("Sound", true);
	}

	void DisableSound () {
		soundController.Disable ();
	}

	void EnableSound () {
		soundController.Enable ();
	}
}
