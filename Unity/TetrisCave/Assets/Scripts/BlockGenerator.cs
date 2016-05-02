using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockGenerator : MonoBehaviour {

	public float gravityMultiplier;

	public GridController gridController;
	public AudioController audioController;
	public BlockCounter blockCounter;
	public GameObject blockPrefab;
	public GameObject tempBlockPrefab;
	public GameObject blockGroupPrefab;

	List<GridSquare> frameScriptList;
	List<GameObject> tempBlockList;
	List<GameObject> blockGroupList;

	int widthSquareNum;
	int heightSquareNum;

	bool[,] blockList;
	int blockCount = 0;

	bool tutorial;
	bool secStage;

	void Awake () {
		widthSquareNum = gridController.GetWidthSquareNum ();
		heightSquareNum = gridController.GetHeightSquareNum ();

		if (widthSquareNum == 0) {
			tutorial = true;
			widthSquareNum = 6;
			heightSquareNum = 10;
		}

		blockList = new bool[widthSquareNum, heightSquareNum];
		frameScriptList = new List<GridSquare> ();
		tempBlockList = new List<GameObject> ();
		blockGroupList = new List<GameObject> ();
	}

	public void NewHit (GameObject hitFrame) {
		if (tutorial)
			GameObject.Find ("Finger").GetComponent<SpriteRenderer>().enabled = false;

		GridSquare hitFrameScript = hitFrame.GetComponent<GridSquare> ();
		if (hitFrameScript && hitFrameScript.IsEnabled ()) {
			int x = hitFrameScript.GetX ();
			int y = hitFrameScript.GetY ();

			if (!blockList[x, y]) {
				if (tempBlockList.Count == 0 ||
				    (x != widthSquareNum-1 && blockList[x+1, y]) ||
				    (x != 0 && blockList[x-1, y]) ||
				    (y != heightSquareNum-1 && blockList[x, y+1]) ||
				    (y != 0 && blockList[x, y-1])) {
					frameScriptList.Add(hitFrameScript);
					newBlock (x, y);
				}
			}
		}
	}

	public void newBlock (int x, int y) {
		if (blockCounter.GetBlocksLeft () <= 0) {
			blockCounter.NoMoreBlocks ();
			return;
		}
		blockList [x, y] = true;
		tempBlockList.Add ((GameObject)Instantiate (tempBlockPrefab, gridController.GetPosition (x, y), Quaternion.identity));
		blockCount++;
		blockCounter.TempDecrease ();
		audioController.NewBlock ();
	}

	public void TouchEnd () {
		if (tutorial) {
			if (blockCount == 7 || secStage && blockCount == 4) {
				if (!secStage) {
					GameObject.Find ("RedGrid2").transform.Translate(10, 0, 0);
					GameObject.Find ("RedGrid1").SetActive (false);
					GameObject.Find ("Finger").GetComponent<SpriteRenderer>().enabled = true;
				}
				GameObject.Find ("Finger").GetComponent<Animator>().SetTrigger("Next");
				secStage = true;
			} else {
				blockCounter.Discard ();
				ResetValues ();
				GameObject.Find ("Finger").GetComponent<SpriteRenderer>().enabled = true;
				return;
			}
		}

		foreach (GridSquare frameScript in frameScriptList) {
			if (!frameScript.IsEnabled ()) {
				ResetValues ();
				blockCounter.Discard ();
				break;
			}
		}

		GameObject blockGroup = (GameObject)Instantiate (blockGroupPrefab, Vector2.zero, Quaternion.identity);
		blockGroupList.Add (blockGroup);
		blockGroup.transform.parent = transform;
		blockGroup.GetComponent<Rigidbody2D> ().gravityScale = blockCount * gravityMultiplier;

		for (int y = 0; y < heightSquareNum; y++) {
			for (int x = 0; x < widthSquareNum; x++) {
				if (blockList[x, y]) {
					GameObject block = (GameObject)Instantiate(blockPrefab, gridController.GetPosition (x, y), Quaternion.identity);
					block.transform.parent = blockGroup.transform;
				}
			}
		}

		blockCounter.FinalizeCount ();

		ResetValues ();
	}

	void ResetValues () {
		blockCount = 0;
		blockList = new bool[widthSquareNum, heightSquareNum];
		frameScriptList = new List<GridSquare> ();
		foreach (GameObject tempBlock in tempBlockList) {
			Destroy(tempBlock);
		}
		tempBlockList = new List<GameObject>();
	}

	public void DestroyEverything () {
		foreach (GameObject blockGroup in blockGroupList) {
			if (blockGroup)
				Destroy (blockGroup);
		}
	}
}
