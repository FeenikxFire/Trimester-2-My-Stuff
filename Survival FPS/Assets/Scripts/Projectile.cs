using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Transform myTransform;

	public float projectileSpeed = 20.0f;
	public float projectileDamage = 5.0f;
	public float despawnTime = 5.0f;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		despawnTime = Time.time + despawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);

		//Projectile Expiry
		if (Time.time >= despawnTime)
			Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision collidingObject){
		if (collidingObject.transform.tag == "Player") {
			collidingObject.transform.SendMessage ("takeDamage", projectileDamage, SendMessageOptions.DontRequireReceiver);
		}
	}







}
