using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private Transform myTransform;

	//Health
	public int health = 20;

	//Patroling Variables
	public float patrolSpeed = 2.0f;
	public GameObject[] patrolPoints;
	public int currentPatrolPoint = 0;
	public float patrolPointDistance = 1.25f;

	//Attacking variables
	public GameObject attackTarget;
	public float attackDistance = 1.0f;

	//Chase Variables
	private float chaseSpeed = 2.0f;

	//Enemy States Enumerator Construct
	public enum EnemyState
	{
		Patrolling,
		Chasing,
		Attacking,
		Dead
	}

	public EnemyState enemyState;


	// Use this for initialization
	void Start () {
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		EnemyLogic ();
		EnemyActions ();
	}

	//Behavaiour Logic
	void EnemyLogic()
	{
		if(attackTarget == null)
		{
			enemyState = EnemyState.Patrolling;
		}
		else if(attackTarget != null)
		{
			if(Vector3.Distance (myTransform.position, attackTarget.transform.position) > attackDistance)
			{
				enemyState = EnemyState.Chasing;
			}
			else
			{
				enemyState = EnemyState.Attacking;
			}
		}
		if(health <= 0)
		{
			enemyState = EnemyState.Dead;
		}
	}

	//Behaviour Actions
	void EnemyActions()
	{
		switch(enemyState)
		{
		case EnemyState.Patrolling:
			Patrol();
			break;

		case EnemyState.Chasing:
			Chase();
			break;

		case EnemyState.Attacking:
			Attack();
			break;

		case EnemyState.Dead:
			Dead();
			break;

		}
	}

	//Enemy Action - patrol between provided points
	void Patrol()
	{
		//Snap rotation towards curren patrol point //CURRENTLY HOW THE SKELETON TURNS
		myTransform.LookAt (patrolPoints [currentPatrolPoint].transform.position);

		//Move in direction of patrol point
		myTransform.Translate (Vector3.forward * Time.deltaTime * patrolSpeed);

		//Play walk animation
		GetComponent<Animation> ().Blend ("walk");

		//Close to/arrived at patrol point. Switch to the next/first patrol point;
		if(Vector3.Distance (myTransform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolPointDistance)
		{
			//Circular/Reset patrol technique
			if(currentPatrolPoint == patrolPoints.Length - 1)
			{
				currentPatrolPoint = 0;
			}
			else
			{
				++currentPatrolPoint;
			}
		}
	}

	//Enemy Action - Chase down player;
	void Chase()
	{
		//Snap rotation towards curren patrol point //CURRENTLY HOW THE SKELETON TURNS
		myTransform.LookAt (attackTarget.transform.position);
		
		//Move in direction of patrol point
		myTransform.Translate (Vector3.forward * Time.deltaTime * chaseSpeed);

		//Play the run animation
		GetComponent<Animation> ().Blend ("run");
	}

	//Enemy Action - Attack Player;
	void Attack()
	{
		GetComponent<Animation>().Play ("attack2");
	}

	void Dead()
	{
		GetComponent<Animation>().Play("death1");
		StartCoroutine (DeathDelay (GetComponent<Animation>()["death1"].length));
	}

	IEnumerator DeathDelay(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		Destroy (this.gameObject);
	}
}
