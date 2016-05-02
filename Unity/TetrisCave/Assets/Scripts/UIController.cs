using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;

public class UIController : MonoBehaviour {

	public LevelController levelController;
	public TouchController touchController;
	public AudioController audioController;
	public Animator pauseAnimator;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseAnimator.SetTrigger("Pause");
			Pause ();
		}
	}

	public void Pause () {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			touchController.DisableTouch ();

			if (AdsController.AdTime ())
				Chartboost.cacheInterstitial(CBLocation.GameOver);
		} else {
			Time.timeScale = 1;
			touchController.EnableTouch ();
		}
	}

	public void Restart () {
		Ad ();
		Time.timeScale = 1;
		Application.LoadLevel ("Level" + levelController.level);
	}

	public void Exit () {
		Ad ();
		Time.timeScale = 1;
		Application.LoadLevel ("LevelMenu");
	}

	public void Next () {
		Ad ();
		Application.LoadLevel ("Level" + (levelController.level + 1));
	}

	void Ad () {
		AdsController.Add ();
		if (AdsController.AdTime ()) {
			Chartboost.showInterstitial (CBLocation.GameOver);
		}
	}

	void StarSound () {
		audioController.StarSound ();
	}
}
