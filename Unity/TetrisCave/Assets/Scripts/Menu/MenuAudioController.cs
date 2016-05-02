using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuAudioController : MonoBehaviour {
	
	AudioPlayer audioPlayer;
	
	public List<AudioClip> gameMusic;
	public AudioClip menuMusic;
	public AudioClip click;
	public AudioClip whoosh;
	public AudioClip eating;
	public AudioClip locked;
	
	void Awake () {
		audioPlayer = AudioPlayer.Instance;
		StopMusic ();
		PlayMenuMusic ();
	}
	
	public void ClickSound () {
		audioPlayer.PlayOneShotSoundSFX (click);
	}
	
	public void StopMusic () {
		audioPlayer.StopBackgroundMusic ();
	}
	
	public void PlayMenuMusic () {
		audioPlayer.PlayBackgroundMusic (menuMusic, true);
		audioPlayer.BackgroundMusicVolume = 0.5f;
	}
	
	public void PauseMusic () {
		audioPlayer.PauseBackgroudMusic ();
	}
	
	public void ResumeMusic () {
		audioPlayer.ResumeBackgroundMusic ();
	}
	
	public void PlayGameMusic () {
		audioPlayer.PlayBackgroundMusic (gameMusic, true);
		audioPlayer.BackgroundMusicVolume = 0.5f;
	}
	
	public void WhooshSound () {
		StopEating ();
		audioPlayer.PlayOneShotSoundSFX (whoosh);
	}
	
	public void StartEating () {
		audioPlayer.PlayOneShotSoundSFX (eating, 0.1f);
	}
	
	public void StopEating () {
		audioPlayer.StopAllSfx ();
	}

	public void LockedLevel () {
		audioPlayer.PlayOneShotSoundSFX (locked, 0.5f);
	}
}
