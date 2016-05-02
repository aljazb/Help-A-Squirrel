using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockCounter : MonoBehaviour {

	public int blocksLeft;
	public int twoStars;
	public int threeStars;
	public Text UIText;
	int tempBlockCount = 0;
	
	public GameObject star2;
	public GameObject star3;

	public Animator blocksLeftAnim;
	public Animator UIAnimator;
	public EndLine endLine;
	float timeToLose = 2f;

	void Awake () {
		UpdateText ();
	}

	public void TempDecrease () {
		blocksLeft--;
		tempBlockCount++;
		UpdateText ();
	}

	public void FinalizeCount () {
		tempBlockCount = 0;
		if (blocksLeft == 0) {
			Invoke ("LevelFailed", timeToLose);
		}
	}

	public void Discard () {
		blocksLeft += tempBlockCount;
		tempBlockCount = 0;
		UpdateText ();

		if (blocksLeft >= threeStars)
			star3.SetActive (true);
		if (blocksLeft >= twoStars)
			star2.SetActive (true);
	}

	public int GetBlocksLeft () {
		return blocksLeft;
	}

	public void NoMoreBlocks () {
		blocksLeftAnim.SetTrigger ("NoMore");
	}

	void LevelFailed () {
		if (endLine.IsEnding ()) {
			Invoke ("LevelFailed", timeToLose);
		} else {
			UIAnimator.SetTrigger ("Failed");
		}
	}

	void UpdateText () {
		UIText.text = blocksLeft + "";
		if (blocksLeft < threeStars)
			star3.SetActive (false);
		if (blocksLeft < twoStars)
			star2.SetActive (false);
	}

	public int GetStars () {
		if (blocksLeft >= threeStars)
			return 3;
		if (blocksLeft >= twoStars)
			return 2;
		return 1;
	}
}
