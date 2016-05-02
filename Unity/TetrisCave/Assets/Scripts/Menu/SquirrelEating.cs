using UnityEngine;
using System.Collections;

public class SquirrelEating : MonoBehaviour {

	public MenuAudioController menuAudioController;

	void StartEating () {
		menuAudioController.StartEating ();
	}

	void StopEating () {
		menuAudioController.StopEating ();
	}
}
