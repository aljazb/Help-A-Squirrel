using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndLine : MonoBehaviour {

	public SquirrelController squirrel;

	List<GameObject> colObjects = new List<GameObject>();
	public TouchController touchController;

	public float timeToWin;
	int blockCount = 0;
	bool ending = false;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Block") {
			blockCount++;

			if (!colObjects.Contains (col.gameObject))
				colObjects.Add (col.gameObject);

			if (!IsInvoking()) {
				Invoke ("End", timeToWin);
				squirrel.Prepare ();
				ending = true;
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "Block") {
			blockCount--;

			if (colObjects.Contains (col.gameObject))
				colObjects.Remove (col.gameObject);

			if (blockCount <= 0) {
				CancelInvoke("End");
				squirrel.Relax ();
				ending = false;
			}
		}
	}

	void End () {
		float topY = -10f;
		foreach (GameObject colObject in colObjects) {
			if (colObject && colObject.transform.position.y > topY)
				topY = colObject.transform.position.y;
		}

		squirrel.Jump (topY);
		touchController.DisableTouch ();
	}

	public bool IsEnding () {
		return ending;
	}
}
