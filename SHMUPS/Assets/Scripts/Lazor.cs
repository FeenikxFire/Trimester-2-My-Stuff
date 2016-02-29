using UnityEngine;
using System.Collections;

public class Lazor : MonoBehaviour {

	private float projectileSpeed = 300.0f;

	public Transform myTransform;

	private Vector3 lazorPosition;

	private float lifeTime;
	private float lifeTimeDuration = 0.8f;

	private float damage = 1;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;

		lifeTime = Time.time + lifeTimeDuration;
	}
	
	// Update is called once per frame
	void Update () {
		lazorPosition = myTransform.position;

		lazorPosition += Time.deltaTime * projectileSpeed * transform.forward;

		myTransform.position = lazorPosition;


		//kill projectile.
		if(Time.time>lifeTime)
		{
			Destroy (this.gameObject);
		}
	}



	private void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "Enemy")
		{
			//otherObject.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);  //SHIT WAY SO EXPENSIVE
			otherObject.GetComponent<Enemy>().takeDamage (damage);
			Destroy(this.gameObject);

		}
	}
}
