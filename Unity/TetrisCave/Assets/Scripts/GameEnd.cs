using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class GameEnd : MonoBehaviour {

	public Animator levelAnimator;
	public Animator UIAnimator;

	public CameraController cameraController;
	public BlockGenerator blockGenerator;
	public BlockCounter blockCounter;
	public LevelController levelController;
	public GameObject grid;
	public AudioSource audioSource;

	void GameEnded () {
		if (AdsController.AdTime ())
			Chartboost.cacheInterstitial (CBLocation.GameOver);

		grid.SetActive (false);
		cameraController.DisableCamera ();
		blockGenerator.DestroyEverything ();
		UIAnimator.SetInteger ("Stars", blockCounter.GetStars ());
		levelAnimator.SetTrigger ("End");
		UIAnimator.SetTrigger ("End");

		GameController.SetCompleted (levelController.level, blockCounter.GetStars ());
	}

	void JumpSound () {
		if (AudioPlayer.Instance.Mute == false)
			audioSource.Play ();
	}
}
