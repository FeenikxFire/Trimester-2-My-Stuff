using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform myTransform;

	public GameObject enemyPlayer;

	public float range = 25.0f;
	public float fireRate = 1.0f;
	private float fireTime;

	public GameObject turretProjectile;

	//Rotation variables
	public float rotationSpeed = 5.0f;
	private float adjRotSpeed;
	private Quaternion targetRotation;

	// Use this for initialization
	void Start () {

		myTransform = this.transform;

		enemyPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		turretTracking ();
	}

	void turretTracking(){
		//Raycast Detection
		RaycastHit hit;
		if(Physics.Raycast (myTransform.position, 
		   -(myTransform.position - enemyPlayer.transform.position).normalized, 
		                    out hit, range)){

			if(hit.transform.tag == "Player"){

				//Track Player - Linear Interpolation (LERP)
				targetRotation = Quaternion.LookRotation (enemyPlayer.transform.position - myTransform.position);
				adjRotSpeed = Mathf.Min (rotationSpeed * Time.deltaTime, 1);
				myTransform.rotation = Quaternion.Lerp (myTransform.rotation, targetRotation, adjRotSpeed);

				//Shoot at player
				if (Time.time > fireTime){
					Instantiate (turretProjectile, myTransform.position, myTransform.rotation);
					fireTime = Time.time + fireRate;
				}

				//Draw red debug line
				Debug.DrawLine (myTransform.position, hit.point, Color.red);
			} else {
				//Draw green debug line
				Debug.DrawLine (myTransform.position, hit.point, Color.green);
			}
		}
	}
}
