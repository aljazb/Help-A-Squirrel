using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuLevelController : MonoBehaviour {

	public int level;
	public Text levelNum;
	public GameObject block;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public Animator animator;
	public MenuAudioController menuAudioController;

	void Awake () {
		levelNum.text = level + "";

		if (GameController.IsUnlocked (level)) {
			block.SetActive (true);
			animator.SetBool ("LockedBool", false);
		}

		if (GameController.GetStars (level) > 0) {
			star1.SetActive (true);
			if (GameController.GetStars (level) > 1) {
				star2.SetActive (true);
				if (GameController.GetStars (level) > 2) {
					star3.SetActive (true);
				}
			}
		}
	}

	public void LoadLevel () {
		if (GameController.IsUnlocked (level)) {
			menuAudioController.ClickSound ();
			menuAudioController.PlayGameMusic ();
			Application.LoadLevel ("Level" + level);
		} else
			menuAudioController.LockedLevel ();
	}
}
