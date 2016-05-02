using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

	AudioPlayer audioPlayer;

	public AudioClip click;
	public AudioClip newBlock;
	public AudioClip star;
	public List<AudioClip> wood;
	public List<AudioClip> stone;

	float blockSoundThreshold = 0.3f;
	float lastBlockSoundTime = 0;
	
	void Awake () {
		audioPlayer = AudioPlayer.Instance;
	}
	
	public void ClickSound () {
		audioPlayer.PlayOneShotSoundSFX (click);
	}

	public void NewBlock () {
		audioPlayer.PlayOneShotSoundSFX (newBlock);
	}

	public void StarSound () {
		audioPlayer.PlayOneShotSoundSFX (star, 0.5f);
	}

	public void WoodSound (float volume) {
		if (Time.time - lastBlockSoundTime > blockSoundThreshold) {
			audioPlayer.PlayOneShotSoundSFXVol (wood, volume);
			lastBlockSoundTime = Time.time;
		}
	}

	public void StoneSound (float volume) {
		if (Time.time - lastBlockSoundTime > blockSoundThreshold) {
			audioPlayer.PlayOneShotSoundSFXVol (stone, volume);
			lastBlockSoundTime = Time.time;
		}
	}
}
