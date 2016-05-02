using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSquare : MonoBehaviour {

	public int x;
	public int y;

	List<GameObject> colObjects;

	SpriteRenderer spriteRenderer;
	bool spriteEnabled = true;

	void Awake() {
		colObjects = new List<GameObject> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!colObjects.Contains (col.gameObject)) {
			colObjects.Add (col.gameObject);
			spriteRenderer.enabled = false;
			spriteEnabled = false;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (colObjects.Contains (col.gameObject))
			colObjects.Remove (col.gameObject);
		if (colObjects.Count == 0) {
			spriteRenderer.enabled = true;
			spriteEnabled = true;
		}
	}

	public bool IsEnabled () {
		return spriteEnabled;
	}

	public void SetPosition (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public int GetX () {
		return x;
	}

	public int GetY () {
		return y;
	}
}
