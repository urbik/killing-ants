using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

	public GameObject[] enemies;

	public List<SpawnAction> spawnProgress = new List<SpawnAction>();

	private SpawnAction actualAction;
	private float startSpawnTime = -1f;
	private bool isSpawn = false;
	private bool endSpawn = false;

	void Start(){}

	void Update()
	{
		if(isSpawn && !endSpawn)
		{
			if(actualAction.time <= Time.time - startSpawnTime)
			{
				if(actualAction.action == "spawn")
				{
					Instantiate(enemies[(int) actualAction.spawnParameter], transform.position, Quaternion.identity);
				}
				if(spawnProgress.Count > 0)
				{
					actualAction = spawnProgress[0];
					spawnProgress.RemoveAt(0);
				}
				else
				{
					endSpawn = true;
				}
			}
		}
	}

	void startSpawn()
	{
		if(!isSpawn && !endSpawn)
		{
			isSpawn = true;
			startSpawnTime = Time.time;
			actualAction = spawnProgress [0];
			spawnProgress.RemoveAt (0);
		}
	}
}