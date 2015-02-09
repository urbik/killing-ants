using UnityEngine;
using System.Collections;

public class PathPoint : MonoBehaviour {
	public GameObject left;
	public GameObject right;
	public GameObject up;
	public GameObject down;

	public bool isStop = false;

	public string[] events;
	
	public GameObject[] spawnPoints;
}