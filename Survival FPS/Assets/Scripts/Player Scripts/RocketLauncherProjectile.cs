using UnityEngine;
using System.Collections;

public class RocketLauncherProjectile : MonoBehaviour {

	private Transform myTransform;
	
	public float projectileSpeed = 15.0f;
	public float projectileSpeedIncrease = 3.5f;
	public float projectileTerminalSpeed = 35.0f;
	public float projectileDamage = 5.0f;
	public float despawnTime = 5.0f;
	//public float boostTime = 0.25f;

	//Projectile Effects
	public GameObject rocketExplosionEffect;
	public GameObject rocketSmoke;

	public float rocketSmokeTime = 0.1f;
	private float rocketSmokeTimer;

	//Audio
	public GameObject explosionSound;
	public GameObject rocketFlying;

	//Damage Game Object
	public GameObject explosionDamage;
	
	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		
		despawnTime = Time.time + despawnTime;

		GameObject thisSound = Instantiate(rocketFlying, myTransform.position, myTransform.rotation) as GameObject;
		thisSound.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {

		//Increase Rocket Speed towards cap
		projectileSpeed += projectileSpeedIncrease;
		
		if (projectileSpeed > projectileTerminalSpeed)
			projectileSpeed = projectileTerminalSpeed;

		//Release Smoke
		if (rocketSmokeTimer <= Time.time) {

			Instantiate(rocketSmoke, myTransform.position, myTransform.rotation);

			rocketSmokeTimer = Time.time + rocketSmokeTime;
		}
		
		//Projectile Movement
		myTransform.Translate(0f, 0f, projectileSpeed * Time.deltaTime);
		
		//Projectile Expiry
		if(Time.time >= despawnTime){
			Destroy (this.gameObject);
		}
	}
	
	//When colliding with another object...
	void OnCollisionEnter(Collision collidingObject){

		Instantiate (rocketExplosionEffect, myTransform.position, myTransform.rotation);

		Instantiate (explosionSound, myTransform.position, myTransform.rotation);

		Instantiate (explosionDamage, myTransform.position, myTransform.rotation);
		
		Destroy (this.gameObject);
	}
}
