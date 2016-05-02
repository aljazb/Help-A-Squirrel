using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class GameController : MonoBehaviour {
//	[MenuItem("PlayerPrefs/Clear")]
	public static void ClearPlayerPrefs(){
		PlayerPrefs.DeleteAll();
	}
	
	public static void SetCompleted(int level, int stars) {
		if (PlayerPrefs.GetInt ("Level" + level + "Completed") == 0) {
			PlayerPrefs.SetInt ("Level" + level + "Completed", 1);
			PlayerPrefs.SetInt ("Level" + level + "Stars", stars);
			Unlock(level+1);
		} else {
			if (PlayerPrefs.GetInt("Level" + level + "Stars") < stars)
				PlayerPrefs.SetInt ("Level" + level + "Stars", stars);
		}
		
		PlayerPrefs.Save ();
	}
	
	public static bool IsCompleted(int level) {
		if (PlayerPrefs.GetInt ("Level" + level + "Completed") == 0)
			return false;
		return true;
	}

	public static void Unlock(int level) {
		PlayerPrefs.SetInt ("Level" + level + "Unlocked", 1);
		PlayerPrefs.Save ();
	}
	
	public static bool IsUnlocked(int level) {
		if (level == 1)
			return true;
		if (PlayerPrefs.GetInt ("Level" + level + "Unlocked") == 0)
			return false;
		return true;
	}

	public static int GetStars(int level) {
		return PlayerPrefs.GetInt ("Level" + level + "Stars", 0);
	}

	/*public static List<bool> GetCompletedList() {
		List<bool> completed = new List<bool> ();
		int count = 0;
		while (IsCompleted(count)) {
			completed.Add (true);
			count++;
		}
		return completed;
	}*/
	
	/*public static List<bool> GetUnlockedList() {
		List<bool> unlocked = new List<bool> ();
		int count = 0;
		while (IsUnlocked(count)) {
			unlocked.Add (true);
			count++;
		}
		return unlocked;
	}*/
}
