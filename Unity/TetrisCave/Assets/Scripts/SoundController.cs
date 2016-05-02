using UnityEngine;
using System.Collections;

public class SoundController {

	static SoundController instance = new SoundController();
	bool sound;

	private SoundController () {
	}

	void Awake () {
		sound = LoadState ();
	}

	public static SoundController GetInstance () {
		return instance;
	}

	public bool IsEnabled () {
		return sound;
	}

	public void Disable () {
		AudioPlayer.Instance.Mute = true;
		sound = false;
		SaveState ();
	}

	public void Enable () {
		AudioPlayer.Instance.Mute = false;
		sound = true;
		SaveState ();
	}

	void SaveState () {
		if (sound)
			PlayerPrefs.SetInt ("Sound", 1);
		else
			PlayerPrefs.SetInt ("Sound", 0);
		PlayerPrefs.Save ();
	}

	bool LoadState () {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1)
			return true;
		return false;
	}
}
