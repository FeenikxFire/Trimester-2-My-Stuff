using UnityEngine;
using System.Collections;

public class PlayerMissile : MonoBehaviour {

	GameManager gameManager; //Gets game manager.

	private Transform myTransform;
	private float projectileSpeed = 150.0f;
	private GameObject closestEnemyUnit;
	private float rotationSpeed = 10.0f;
	private int damage = 1;

	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;

		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> (); //Gets game Manager.
	}
	
	// Update is called once per frame
	void Update () 
	{
		//movement
		myTransform.position += Time.deltaTime * projectileSpeed * transform.forward;

		closestEnemyUnit = FindClosestEnemyUnit ();

		if (closestEnemyUnit != null)
		{
			//Smooth Lock
			//Determine the target rotation. This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation (closestEnemyUnit.transform.position - myTransform.position);

			//Smoothly rotate towards the target point.
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}


	//algorithm controlling the detection of closest enemy target using global enemy list.
	//Return the closest enemy in enemylist.
	private GameObject FindClosestEnemyUnit ()
	{
		float distance = Mathf.Infinity;
		Vector3 position = myTransform.position;

		foreach (GameObject enemyUnit in gameManager.enemyUnitList)
		{
			Vector3 diff = enemyUnit.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if(curDistance < distance)
			{
				closestEnemyUnit = enemyUnit;
				distance = curDistance;
			}
		}
		return closestEnemyUnit;
	}

	private void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "Enemy")
		{
			//otherObject.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);  //SHIT WAY SO EXPENSIVE
			otherObject.GetComponent<Enemy>().takeDamage (damage);
			Destroy(this.gameObject);
			
		}
	}
}
