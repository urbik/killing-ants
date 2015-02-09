using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float attackDistance = 1f;
	public float attackSpeed = 1.5f;

	public int dmg = 2;
	public int health = 1;

	private GameObject player;
	private NavMeshAgent agent;

	private bool isGoing = true;
	private float startTimer;

	void Start () {
		player = GameObject.FindWithTag("Player");
		agent = gameObject.GetComponent<NavMeshAgent>();
	}

	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
		{
			agent.destination = player.transform.position;
			isGoing = true;
			stopAttackTimer();
		}
		else
		{
			agent.Stop();
			tryAttack();
		}
	}

	void tryAttack()
	{
		if (isGoing)
		{
			isGoing = false;
			startTimer = Time.time;
			doAttack();
		}
		else
		{
			if((Time.time - startTimer) >= attackSpeed)
			{
				doAttack();
			}
		}
	}

	void doAttack()
	{
		player.GetComponent<PlayerController>().SendMessage("applyDmg", dmg);
		startTimer = Time.time;
	}

	void stopAttackTimer()
	{
		startTimer = 0f;
	}
}