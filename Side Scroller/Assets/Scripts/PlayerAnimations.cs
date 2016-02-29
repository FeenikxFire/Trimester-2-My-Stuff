using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	Player player;
	Animation animationComponent;

	// Use this for initialization
	void Start () 
	{
		player = GetComponent<Player> ();
		animationComponent = GetComponent<Animation> ();

		//Adjusted Animation Speeds
		GetComponent<Animation>()["Skill"].speed = 1.1f;
		GetComponent<Animation>()["Attack"].speed = 1.25f;

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Animation Switch
		switch (player.playerState)
		{
		case Player.PlayerState.Idle:
			animationComponent.Play("Idle");
			break;
		case Player.PlayerState.Walk:
			animationComponent.Play("Walk");
			break;
		case Player.PlayerState.Run:
			animationComponent.Play("Run");
			break;
		case Player.PlayerState.Jump:
			animationComponent.Play("Skill");
			break;
		case Player.PlayerState.Attack:
			animationComponent.Play("Attack");
			break;

		}
	
	}
}
