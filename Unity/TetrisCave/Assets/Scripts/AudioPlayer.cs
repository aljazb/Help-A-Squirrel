using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{
	private bool AppPaused;
	private const int MaxSfxSounds = 7;
	private List<AudioSource> SfxSounds;
	private bool mute;
	private AudioSource BackgroundMusic;
	
	private GameObject DestroyedCheck;
	
	#region properties

	public float BackgroundMusicVolume {
		get { return BackgroundMusic == null ? -1 : BackgroundMusic.volume; }
		set {
			if (BackgroundMusic != null) {
				BackgroundMusic.volume = value;
			}
		}
	}
	
	public float BackgroundMusicPitch {
		get { return BackgroundMusic == null ? -1 : BackgroundMusic.pitch; }
		set {
			if (BackgroundMusic != null) {
				BackgroundMusic.pitch = value;
			}
		}
	}
	
	public bool Mute { 
		get {
			return mute;
		} 
		set {
			if (mute != value) {
				mute = value;
				
				foreach (AudioSource audioSfx in SfxSounds) { // find stopped audio sources
					if (audioSfx.isPlaying) {
						audioSfx.mute = mute;
					}
				}
				BackgroundMusic.mute = mute;
				
			}
		} 
	}
	
	#endregion
	
	private static AudioPlayer instance;
	
	public static AudioPlayer Instance{ get{
			if(instance==null){
				GameObject audioSource=new GameObject("AudioSource");
				instance=audioSource.AddComponent<AudioPlayer>();
			}
			return instance;
		}}
	
	public void Awake ()
	{
		DestroyedCheck = gameObject;
		DontDestroyOnLoad (this);
		BackgroundMusic = gameObject.AddComponent<AudioSource> ();
		SfxSounds = new List<AudioSource> (MaxSfxSounds);
		if (PlayerPrefs.GetInt ("Sound") == 0)
			Mute = true;
		Application.LoadLevel ("Menu");
	}
	
	public void PlayOneShotSoundSFX (AudioClip[] clip)
	{
		PlayOneShotSoundSFX (SelectRandomCLip (clip));
	}
	
	public AudioClip SelectRandomCLip (AudioClip[] clips)
	{
		if (clips == null)
			return null;
		
		if (clips.Length == 0)
			return null;
		
		return clips [Random.Range (0, clips.Length - 1)];
	}
	
	public AudioClip GetAudioClip (string resourcePath)
	{
		return Resources.Load (resourcePath) as AudioClip;
	}
	
	public void PlayOneShotSoundSFX (AudioClip clip)
	{
		PlayOneShotSoundSFX (clip, 1f);
	}

	public void PlayOneShotSoundSFX (AudioClip clip, float volume)
	{
		PlayOneShotSoundSFX (clip, 1f, volume, 0);
	}
	
	public void PlayOneShotSoundSFX (AudioClip clip, float pitch, float delay)
	{
		StartCoroutine (PlayDelayed (clip, delay, pitch));
	}
	
	public void PlayOneShotSoundSFX (AudioClip clip, float pitch, float volume, int none)
	{
		PlayOneShotSoundSFX (clip, pitch, true, volume);
	}
	
	private IEnumerator PlayDelayed (List<string> list, float delay)
	{
		yield return new WaitForSeconds (delay);
		PlayOneShotSoundSFX (list);
	}
	
	private IEnumerator PlayDelayed (AudioClip clip, float delay, float pitch)
	{
		yield return new WaitForSeconds (delay);
		PlayOneShotSoundSFX (clip, pitch);
	}
	
	public void PlayOneShotSoundSFX (List<string> list, float delay)
	{
		StartCoroutine (PlayDelayed (list, delay));
	}

	public void PlayOneShotSoundSFX (List<string> list)
	{
		PlayOneShotSoundSFX (list, true);
	}

	public void PlayOneShotSoundSFX (List<string> list, bool stopOnSceneChange)
	{
		
		AudioClip pickedSound = GetAudioClip (list [Random.Range (0, list.Count)]);
		PlayOneShotSoundSFX (pickedSound);
	}

	public void PlayOneShotSoundSFXVol (List<string> list, bool stopOnSceneChange, float volume)
	{
		
		AudioClip pickedSound = GetAudioClip (list [Random.Range (0, list.Count)]);
		PlayOneShotSoundSFX (pickedSound, volume);
	}
	
	public void PlayOneShotSoundSFX (List<AudioClip> list)
	{
		AudioClip pickedSound = list [Random.Range (0, list.Count)];
		PlayOneShotSoundSFX (pickedSound);
	}

	public void PlayOneShotSoundSFXVol (List<AudioClip> list, float volume)
	{
		AudioClip pickedSound = list [Random.Range (0, list.Count)];
		PlayOneShotSoundSFX (pickedSound, volume);
	}
	
	public void PlayOneShotSoundSFX (AudioClip clip, float pitch, bool stopOnSceneChange, float volume)
	{
		if (AppPaused) {
			return;
		}
		
		AudioSource reusedAudioSource = null;
		
		// reuse if any available
		foreach (AudioSource audioSfx in SfxSounds) { // find stopped audio sources
			if (!audioSfx.isPlaying) {
				reusedAudioSource = audioSfx;
				break;
			}
		}
		
		if (reusedAudioSource != null) { // reuse old
			SfxSounds.Remove (reusedAudioSource);
			
		} else { // reuse last or create new
			
			if (SfxSounds.Count > MaxSfxSounds) {
				SfxSounds [MaxSfxSounds - 1].Stop ();
				reusedAudioSource = SfxSounds [MaxSfxSounds - 1];
				SfxSounds.RemoveAt (MaxSfxSounds - 1);
			}
			
			if (reusedAudioSource == null) {
				reusedAudioSource = gameObject.AddComponent<AudioSource> ();
			}
		}
		
		SfxSounds.Insert (0, reusedAudioSource);
		SfxSounds [0].clip = clip;
		SfxSounds [0].playOnAwake = false;
		SfxSounds [0].loop = false;
		SfxSounds [0].mute = Mute;
		SfxSounds [0].pitch = pitch;
		SfxSounds [0].volume = volume;
		SfxSounds [0].Play ();
	}
	
	
	public void PlayBackgroundMusic (List<string> list, bool loop)
	{
		string pickedSound = list [Random.Range (0, list.Count)];
		PlayBackgroundMusic (pickedSound, loop);
	}
	
	public void PlayBackgroundMusic (List<AudioClip> list, bool loop)
	{
		AudioClip pickedSound = list [Random.Range (0, list.Count)];
		PlayBackgroundMusic (pickedSound, loop);
	}
	
	public void PlayBackgroundMusic (string name, bool loop)
	{
		PlayBackgroundMusic (GetAudioClip (name), loop);
	}
	
	public void PlayBackgroundMusic (AudioClip audioClip, bool loop)
	{
		if (AppPaused) {
			return;
		}
		//			if (Logger.DebugEnabled)
		//				Logger.DebugT(Tag, "PlayBackgroundMusic clip {0} resume {1}", audioClip.name, resumeIfPaused);
		
		if (BackgroundMusic.clip != null && BackgroundMusic.clip.name == audioClip.name) {
			// just resume
		} else {
			BackgroundMusic.clip = audioClip;
			
		}
		
		BackgroundMusic.loop = loop;
		BackgroundMusic.mute = Mute;
		BackgroundMusic.Play ();
	}
	
	public bool IsPlayingSfx ()
	{
		for (int i = 0; i < SfxSounds.Count; i++) {
			if (SfxSounds [i].isPlaying) {
				return true;
			}
		}
		return false;
	}
	
	public bool IsPlayingSfxClip (AudioClip audioClip)
	{
		for (int i = 0; i < SfxSounds.Count; i++) {
			if (SfxSounds [i].isPlaying && SfxSounds [i].clip == audioClip) {
				return true;
			}
		}
		return false;
	}
	
	public bool IsPlayingBackgroundMusic ()
	{
		return BackgroundMusic.isPlaying;
	}
	
	public bool IsPlaying ()
	{
		if (IsPlayingSfx ()) {
			return true;
		}
		
		return IsPlayingBackgroundMusic ();
	}
	
	public void PauseBackgroudMusic ()
	{
		BackgroundMusic.Pause ();
	}
	
	public void ResumeBackgroundMusic ()
	{
		BackgroundMusic.Play ();
	}
	
	public void StopBackgroundMusic ()
	{
		StopBackgroundMusic (true);
	}
	
	public void StopBackgroundMusic (bool force)
	{
		if (DestroyedCheck != null)//
			StopAllCoroutines ();
		
		if (force) {
			BackgroundMusic.Stop ();
			BackgroundMusic.clip = null;
		}
		
	}
	
	public void StopAllSounds ()
	{
		StopBackgroundMusic ();
		StopAllSfx ();
	}
	
	public void StopAllSfx ()
	{
		StopAllSfx (true, true);
	}
	
	public void StopAllSfx (bool forceStop, bool sceneChanged)
	{
		StopAllCoroutines ();
		if (SfxSounds != null) {
			for (int i = 0; i < SfxSounds.Count; i++) {
				if (forceStop) {
					SfxSounds [i].Stop ();
					SfxSounds [i].clip = null;
				}
			}
		}
	}
	
	public void OnApplicationPause (bool paused)
	{
		AppPaused = paused;
	}
	
}
