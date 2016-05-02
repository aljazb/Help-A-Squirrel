using UnityEngine;
using System.Collections;

public class GridController : MonoBehaviour {

	public GameObject squareFramePrefab;

	public int widthSquareNum;
	public int heightSquareNum;
	float offsetTop = 0;
	float offsetLeft = -3f;
	
	GameObject[,] squareFrames;
	GridSquare[,] squareScripts;

	void Awake () {
		squareFrames = new GameObject[widthSquareNum, heightSquareNum];
		squareScripts = new GridSquare[widthSquareNum, heightSquareNum];

		for (int y = 0; y < heightSquareNum; y++) {
			for (int x = 0; x < widthSquareNum; x++) {
				squareFrames[x, y] = (GameObject)Instantiate(squareFramePrefab, new Vector2(offsetLeft + x, offsetTop - y), Quaternion.identity);
				squareFrames[x, y].transform.parent = transform;
				squareScripts[x, y] = squareFrames[x, y].GetComponent<GridSquare>();
				squareScripts[x, y].SetPosition(x, y);
			}
		}
	}

	public int GetWidthSquareNum () {
		return widthSquareNum;
	}

	public int GetHeightSquareNum () {
		return heightSquareNum;
	}

	public Vector2 GetPosition (int x, int y) {
		return new Vector2 (offsetLeft + x, offsetTop - y);
	}
}
