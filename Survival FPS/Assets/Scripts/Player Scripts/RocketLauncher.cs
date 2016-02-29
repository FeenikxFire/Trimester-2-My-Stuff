using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	private Transform myTransform;
	
	//Player reference
	Player player;
	
	//Weapon Variables
	private bool fireReady = true;
	private float fireTime;
	private float fireRate = 0.3f;		//This needs to match the firing animation length
	public int clipSize = 1;
	private float dmg = 5;

	//Projectile
	public GameObject rocketProjectile;
	
	//Muzzle + Raycast
	public GameObject muzzle;
	
	//Sound Clips
	public GameObject fireSound;
	public GameObject reloadSound;
	
	//Visual Effects
	public GameObject muzzleBlast;
	
	// Use this for initialization
	void Start () {

		myTransform = this.transform;
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		player.currentAmmo = clipSize;
	}
	
	// Update is called once per frame
	void Update () {
		
		rotateMuzzle();
		
		firingCheck();
	}
	
	
	//rotate muzzle to face focusPoint
	void rotateMuzzle(){
		muzzle.transform.LookAt(player.focusPoint.transform.position);
	}
	
	
	//Play Weapon Fire Controls - Animation, Audio and Projectiles
	void firingCheck(){
		
		if (Input.GetMouseButton (0) && player.currentAmmo > 0 && GetComponent<Animation>().IsPlaying("rocket launcher fire") == false && GetComponent<Animation>().IsPlaying("rocket launcher reload") == false) {
			
			GetComponent<Animation>().Play ("rocket launcher fire");
			
			if (Time.time > fireTime) {
				
				//Reduce current player ammo
				player.currentAmmo--;

				//Projectile
				GameObject thisRocket = Instantiate (rocketProjectile, muzzle.transform.position, muzzle.transform.rotation) as GameObject;

				//Play fire sound
				GameObject thisSound = Instantiate(fireSound, myTransform.position, myTransform.rotation) as GameObject;
				thisSound.transform.parent = transform;

				//Muzzle Effect
				GameObject thisMuzzleBlast = Instantiate (muzzleBlast, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
				thisMuzzleBlast.transform.parent = myTransform;

				fireTime = Time.time + fireRate;
			}
		} else if (GetComponent<Animation>().IsPlaying("rocket launcher fire") == false && player.currentAmmo <= 0) {
			
			GetComponent<Animation>().Play("rocket launcher reload");
			
			//Play reload sound
			GameObject thisSound = Instantiate(reloadSound, myTransform.position, myTransform.rotation) as GameObject;
			thisSound.transform.parent = transform;
			
			//Refill current ammo with maxclipsize
			player.currentAmmo = clipSize;
		}
	}
}
