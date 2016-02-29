using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	private Transform myTransform;

	private float spawnTimer = 3.0f;
	private float spawnTime;

	public GameObject enemy;

	GameManager gameManager;

	private int xLocation = 0;
	private int zLocation = 35;
	private Vector3 spawnLocation;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;

		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > spawnTime)
		{
			xLocation = Random.Range (-(int) (gameManager.xBoundary) + 30, (int)gameManager.xBoundary - 30);

			spawnLocation = new Vector3(xLocation, 0, zLocation);

			myTransform.position = spawnLocation;

			Instantiate(enemy, myTransform.position, Quaternion.Euler(0,180,0));

			spawnTime = Time.time + spawnTimer;
		}
	
	}
}
