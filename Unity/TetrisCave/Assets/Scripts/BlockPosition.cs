using UnityEngine;
using System.Collections;

public class BlockPosition : MonoBehaviour {

	float startTime = 0.5f;
	float repeatRate = 0.5f;
	float destroyOffset = 30f;
	float velocityThreshold = 0.4f;

	Transform levelBottom;
	GameObject parent;
	Rigidbody2D parentRigidbody;

	CameraController cameraController;
	
	AudioController audioController;
	public enum BlockMaterial { Wood, Stone };
	public BlockMaterial blockMaterial;

	void Start () {
		InvokeRepeating ("CheckPosition", startTime, repeatRate);
		parent = transform.parent.gameObject;
		parentRigidbody = parent.GetComponent<Rigidbody2D> ();
		cameraController = Camera.main.GetComponent<CameraController> ();
		levelBottom = cameraController.levelBottom;

		audioController = Component.FindObjectOfType<AudioController> ();
	}

	void CheckPosition () {
		if (transform.position.y < levelBottom.position.y - destroyOffset)
			Destroy (parent);

		bool isMoving = false;
		if (Mathf.Abs (parentRigidbody.velocity.y) > velocityThreshold)
			isMoving = true;

		cameraController.ReportPosition(transform.position.y, isMoving);
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "Block") {
			if (blockMaterial == BlockMaterial.Wood) {
				float volume = 0.25f * col.relativeVelocity.magnitude;
				volume = volume > 1f ? 1f : volume;
				volume = volume < 0.3f ? 0.3f : volume;
				audioController.WoodSound (volume);
			}

			if (blockMaterial == BlockMaterial.Stone) {
				float volume = 0.15f * col.relativeVelocity.magnitude;
				volume = volume > 1f ? 1f : volume;
				volume = volume < 0.15f ? 0.15f : volume;
				audioController.StoneSound (volume);
			}
		}
	}
}
