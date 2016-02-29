using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public AudioSource audiosource;

	public Player player;
	public Enemy enemy;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Spider" && player.isAttacking == true)
		{
			audiosource.Play ();
			enemy.health -= 5;

		}
	}


}
