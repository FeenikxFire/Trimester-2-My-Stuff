using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	public float spawnTime = 3.0f;
	public float spawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > spawnTimer) {

			Instantiate (enemy, transform.position, transform.rotation);

			spawnTimer = Time.time + spawnTime;
		}	      
	}
}
