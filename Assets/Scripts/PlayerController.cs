using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Threading;

public class PlayerController : MonoBehaviour {

	// PLAYER SPECS
	public int health = 20;

	// PATH HANDLING
	public GameObject path;
	private PathPoint pathPoint;
	private bool isGoing = true;
	private bool isWait = false;

	private CharacterController controller;
	private NavMeshAgent agent;

	private float speed = 50.0F;
	private float gravity = 10.0F;
	private Vector3 moveDirection = Vector3.zero;

	private float moveEps = 3F;
	private float crossEps = .5F;

	// ATTACKING
	private RaycastHit rayHit;

	void Start()
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// MOVEMENT

		if(!isWait)
		{
			bool isCrossroad = false;
			pathPoint = path.GetComponent<PathPoint>();
			if ((pathPoint.left && pathPoint.right) || (pathPoint.left && pathPoint.up) || (pathPoint.left && pathPoint.down) ||
							(pathPoint.right && pathPoint.up) || (pathPoint.right && pathPoint.down) ||
							(pathPoint.up && pathPoint.down)) {
				isCrossroad = true;
			}

			Vector3 mag = path.transform.position - transform.position;

			float eps = moveEps;
			if(isCrossroad) eps = crossEps;

			if (mag.magnitude > eps)
			{
				agent.destination = path.transform.position;
			}
			else
			{
				// NEFAKA
				/*foreach(string pathEvent in pathPoint.events)
				{
					string[] function = pathEvent.Split(';');
					MethodInfo mi = this.GetType().GetMethod(function[0]);
					function[0] = "";
					mi.Invoke(this, null);
				}*/

				if(pathPoint.spawnPoints.Length > 0)
				{
					foreach(GameObject spawnPoint in pathPoint.spawnPoints)
					{
						spawnPoint.GetComponent<SpawnPoint>().SendMessage("startSpawn");
					}
				}

				if(isCrossroad || pathPoint.isStop)
				{
					GameManager.showCrossroadButtons(pathPoint);
				}
				else
				{
					if(pathPoint.left) path = pathPoint.left;
					else if(pathPoint.right) path = pathPoint.right;
					else if(pathPoint.up) path = pathPoint.up;
					else if(pathPoint.down) path = pathPoint.down;
				}
			}
		}

		GameObject.FindWithTag("MainCamera").GetComponent<Camera>().SendMessage("updatePosition", transform.position);

		// ATTACKING

		if(Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out rayHit)) {
				GameObject hitObject = rayHit.transform.gameObject;
				if(hitObject.tag.Substring(0, 5) == "Enemy")
				{
					Destroy(hitObject);
				}
			}
		}
	}

	private void wait(int time)
	{
		isWait = true;
		// NORMALNI kurva TIMER POMOCI GAMETIME
		//Timer timer = new Timer(stopWaiting,null,time*100,Timeout.Infinite);
	}

	private void stopWaiting(object state)
	{
		isWait = false;
	}

	public void applyDmg(int dmg)
	{
		health -= dmg;
		Debug.Log (health);
		if(health <= 0)
		{
			GameObject.FindWithTag("GameController").GetComponent<GameManager>().SendMessage("playerDead");
		}
	}
	
	public void goLeft(){path = pathPoint.left;}
	public void goRight(){path = pathPoint.right;}
	public void goUp(){path = pathPoint.up;}
	public void goDown(){path = pathPoint.down;}
}










