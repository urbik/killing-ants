using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	static bool leftBtn = false;
	static bool rightBtn = false;
	static bool upBtn = false;
	static bool downBtn = false;

	void Start () {

	}

	void Update () {

	}

	// GAME EVENTS

	void playerDead()
	{
		Application.Quit ();
	}

	// GUI
	void OnGUI () {
		if (leftBtn)
		{
			if (GUI.Button (new Rect (20, 40, 80, 20), "Go left")) {
				GameObject.FindWithTag("Player").GetComponent<PlayerController>().goLeft();
				hideCrossroadButtons();
			}
		}
		
		if (rightBtn)
		{
			if (GUI.Button (new Rect (20, 80, 80, 20), "Go right")) {
				GameObject.FindWithTag("Player").GetComponent<PlayerController>().goRight();
				hideCrossroadButtons();
			}
		}
		
		if (upBtn)
		{
			if (GUI.Button (new Rect (20, 120, 80, 20), "Go up")) {
				GameObject.FindWithTag("Player").GetComponent<PlayerController>().goUp();
				hideCrossroadButtons();
			}
		}
		
		if (downBtn)
		{
			if (GUI.Button (new Rect (20, 160, 80, 20), "Go down")) {
				GameObject.FindWithTag("Player").GetComponent<PlayerController>().goDown();
				hideCrossroadButtons();
			}
		}
	}
	
	public static void showCrossroadButtons(PathPoint pathPoint)
	{
		if(pathPoint.left) leftBtn = true;
		if(pathPoint.right) rightBtn = true;
		if(pathPoint.up) upBtn = true;
		if(pathPoint.down) downBtn = true;
	}
	
	public static void hideCrossroadButtons()
	{
		leftBtn = false;
		rightBtn = false;
		upBtn = false;
		downBtn = false;
	}
}
