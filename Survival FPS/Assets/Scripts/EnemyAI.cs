using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float health = 20.0f;

	public int points = 5;

	private NavMeshAgent agent;

	private GameObject player;

	public bool grounded = false;

	GameManager gameManager;

	// Use this for initialization
	void Start () {
	
		agent = GetComponent<NavMeshAgent> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Move towards player
		//transform.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);

		/*
		Vector3 adjustedVelocity = transform.GetComponent<Rigidbody> ().velocity;
		adjustedVelocity.x -= 1 * Time.deltaTime;
		adjustedVelocity.y -= 1 * Time.deltaTime;
		adjustedVelocity.z -= 1 * Time.deltaTime;

		transform.GetComponent<Rigidbody> ().velocity = adjustedVelocity;

		if (transform.GetComponent<Rigidbody> ().velocity.y >= 0.1) {
			transform.GetComponent<NavMeshAgent> ().enabled = false;
		} else {
			transform.GetComponent<NavMeshAgent> ().enabled = true;
			agent.SetDestination (player.transform.position);
		}*/

		agent.SetDestination (player.transform.position);
	}

	//Method for receiving damage from other sources
	void takeDamage(float dmg){
		health -= dmg;
		print ("Enemy health: " + health);

		if (health <= 0) {
			gameManager.scoreBonusMultiplier(points);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionStay(Collision otherObject){

		if (otherObject.transform.tag == "Ground") {
			grounded = true;
		}
	}

	void OnCollisionExit(Collision otherObject){
		
		if (otherObject.transform.tag == "Ground") {
			grounded = false;
		}
	}
}
