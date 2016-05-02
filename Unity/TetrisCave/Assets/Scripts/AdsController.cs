using UnityEngine;
using System.Collections;

public class AdsController : MonoBehaviour {
	static int intervalMin = 3;
	static int intervalMax = 5;
	static int actionsTillAd = 3;

	public static void Add () {
		actionsTillAd--;
	}

	public static bool AdTime () {
		if (actionsTillAd <= 0) {
			actionsTillAd = Random.Range(intervalMin, intervalMax);
			return true;
		}
		return false;
	}
}
