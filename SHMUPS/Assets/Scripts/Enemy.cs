using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameManager gameManager; //Gets game manager.

	private float health = 1.0f;

	private float moveSpeed = 30.0f;

	//rotation variables
	private float rotationSpeed = 2.0f;
	private float adjRotSpeed;
	private Quaternion targetRotation;
	//Vector3 AiDirection = Vector3.left;
	//Vector3 MoveDirection = Vector3.zero;
	//Vector3 newposition = Vector3.zero;

	//player reference
	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> (); //Gets game Manager.
	}

	void Update () {

		//Movement
		//Rotate to look at player
		targetRotation = Quaternion.LookRotation (player.transform.position - transform.position);
		adjRotSpeed = Mathf.Min (rotationSpeed * Time.deltaTime, 1);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, adjRotSpeed);
		transform.position += Time.deltaTime * moveSpeed * transform.forward;

		if(health <= 0)
		{
			GetComponent<AudioSource>().Play();
			gameManager.enemiesKilled++;
			Destroy (this.gameObject);
		}

		//Vector3 newposition = AiDirection * (moveSpeed * Time.deltaTime);
		//newposition = transform.position + newposition;
		//newposition.x = Mathf.Clamp (newposition.x, -10, 15);
		//transform.position = newposition;
		//if(newposition.x == 15)
		//{
		//	AiDirection = Vector3.left;
		//}
		//else if (newposition.x == -10);
		//{
		//	AiDirection = Vector3.right;
		//}




	}

	public void takeDamage(float damage)
	{
		health -= damage;
		Debug.Log ("Taking Damage");
	}

}
