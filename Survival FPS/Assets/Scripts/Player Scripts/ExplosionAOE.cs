using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionAOE : MonoBehaviour {

	public float damage = 30.0f;
	public float radius = 10.0f;

	private float lifeTime;
	public float lifeTimeduration = 0.15f;

	public List<Transform> damageTargets = new List<Transform>();

	// Use this for initialization
	void Start () {
	
		//Set size of explosion
		transform.GetComponent<SphereCollider> ().radius = radius;

		lifeTime = Time.time + lifeTimeduration;
	}
	
	// Update is called once per frame
	void Update () {

		//Explosion finish, damage targets and remove AOE
		if (Time.time > lifeTime) {
			foreach (Transform target in damageTargets) {
				if (target != null){
					damage = ((radius - Vector3.Distance (target.transform.position, transform.position)) / radius) * damage;
					print (damage);
					target.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
				}
			}
			Destroy (this.gameObject);
		}

	}
	
	void OnTriggerEnter(Collider otherObject){
		if (otherObject.gameObject.tag == "Enemy") {
			damageTargets.Add (otherObject.gameObject.transform);

			/*
			Vector3 pushVector = -(transform.position - otherObject.transform.position).normalized;
			pushVector *= 25;
			pushVector.y = 15;
			otherObject.GetComponent<Rigidbody>().velocity = pushVector;
			*/
		}

		if (otherObject.gameObject.tag == "Player") {
			Vector3 jumpVector = -(transform.position - otherObject.transform.position).normalized;
			jumpVector *= 25;
			otherObject.GetComponent<CharacterMotor>().SetVelocity (jumpVector);
		}	
	}
}
