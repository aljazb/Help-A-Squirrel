using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

	public BlockGenerator blockGenerator;
	GameObject lastHit;
	bool touched;
	bool touchDisabled = true;
	int pausePixelY;
	int pausePixelX;

	void Awake () {
		pausePixelY = (int)(Screen.height * 0.9f);
		pausePixelX = (int)(Screen.width * 0.19f);
	}

	void Update () {
		if (!touchDisabled && Input.touchCount > 0) {
		    if (Input.GetTouch (0).position.y > pausePixelY && Input.GetTouch (0).position.x < pausePixelX && !touched)
				return;
			touched = true;
			Vector2 rayPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			RaycastHit2D hit = Physics2D.Raycast (rayPos, Vector2.zero);

			if (hit && hit.collider && hit.collider.tag == "Frame") {
				if (hit.collider.gameObject != lastHit) {
					lastHit = hit.collider.gameObject;
					blockGenerator.NewHit (hit.collider.gameObject);
				}
			}
		} else {
			if (touched) {
				touched = false;
				lastHit = null;
				blockGenerator.TouchEnd ();
			}
		}
	}

	public void EnableTouch () {
		touchDisabled = false;
	}

	public void DisableTouch () {
		touchDisabled = true;
	}
}
