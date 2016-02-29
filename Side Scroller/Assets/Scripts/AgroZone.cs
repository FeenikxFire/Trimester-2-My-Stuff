using UnityEngine;
using System.Collections;

public class AgroZone : MonoBehaviour
{
	public GameObject[] enemyList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.transform.tag == "Player")
		{
			foreach(GameObject enemy in enemyList)
			{
				enemy.GetComponent<Enemy>().attackTarget = otherObject.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider otherObject)
	{
		if(otherObject.transform.tag == "Player")
		{
			foreach(GameObject enemy in enemyList)
			{
				enemy.GetComponent<Enemy>().attackTarget = null;
			}
		}
	}
}
