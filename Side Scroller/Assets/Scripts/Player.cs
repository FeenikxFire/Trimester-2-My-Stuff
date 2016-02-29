 using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Transform myTransform;

	public float walkSpeed = 2.0f;
	public float jumpForce = 250.0f;
	private float distToGround;
	private float distToWall;
	
	//Facing Direction
	public GameObject playerModel;
	private int facingDirection = 1;
	private Quaternion facingRotation;

	//Animation Variables
	Animation animationComponent;
	private bool animationLock = false;

	//Attacking Bool
	public bool isAttacking;

	//Player State Enumerator Construct;
	public enum PlayerState
	{
		Idle,
		Walk,
		Run,
		Jump,
		Attack
	}

	public PlayerState playerState;

	// Use this for initialization
	void Start () {
		myTransform = this.transform;

		animationComponent = GetComponent<Animation> ();
		
		distToGround = myTransform.GetComponent<Collider>().bounds.extents.y;
		distToWall = myTransform.GetComponent<Collider>().bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update () {

		if(!Input.anyKey && animationLock == false)
		{
			playerState = PlayerState.Idle;
		}
		else
		Controls ();
		
		DirectionFacing();
	}

	IEnumerator AnimationDelay(PlayerState thisPlayerState, float waitTime)
	{
		playerState = thisPlayerState;
		animationLock = true;
		yield return new WaitForSeconds (waitTime);
		animationLock = false;
		isAttacking = false;
	}

	void Controls(){

		//Attacking
		if(playerState != PlayerState.Attack && Input.GetKeyDown ("f"))
		{
			isAttacking = true;
			StartCoroutine(AnimationDelay(PlayerState.Attack, GetComponent<Animation>()["Attack"].length / GetComponent<Animation>()["Attack"].speed));
		}

	
		 //Move Right
		if (Input.GetKey ("d")) {
			myTransform.Translate(0f, 0f, walkSpeed * Time.deltaTime);
			facingDirection = 1;

			if (animationLock == false)
			{
			playerState = PlayerState.Walk;
			}
		}
		
		//Move Left
		if (Input.GetKey ("a")) {
			myTransform.Translate(0f, 0f, walkSpeed * Time.deltaTime);
			facingDirection = -1;
			if (animationLock == false)
			{
				playerState = PlayerState.Walk;
			}
		}
	
		//Jumping
		if (Input.GetKeyDown ("space") && CheckGrounded() == true) {
			myTransform.GetComponent<Rigidbody>().AddForce (0,jumpForce,0);
			if (animationLock == false)
			{
				StartCoroutine(AnimationDelay(PlayerState.Jump, GetComponent<Animation>()["Skill"].length / GetComponent<Animation>()["Skill"].speed));
			}
		}

	}
	
	//Fix Player Mesh Transform Rotation
	void DirectionFacing(){
		//Get current player model rotation values
		facingRotation = playerModel.transform.rotation;
	
		//Adjust the Y value based on direction facing
		if (facingDirection == 1) {
			facingRotation.eulerAngles = new Vector3(facingRotation.eulerAngles.x, 90, facingRotation.eulerAngles.z);
			playerModel.transform.rotation = facingRotation;
		} else if (facingDirection == -1) {
			facingRotation.eulerAngles = new Vector3(facingRotation.eulerAngles.x, -90, facingRotation.eulerAngles.z);
			playerModel.transform.rotation = facingRotation;
		}
	}

	//Raycast down to check if grounded
	public bool CheckGrounded(){
		return Physics.Raycast(myTransform.position, -Vector3.up, distToGround + 0.1f);
	}

	//Platform Parenting - Keeps player parented to a moving platform
	void OnCollisionStay(Collision collingObject)
	{
		if (collingObject.gameObject.tag == "Platform")
		{
			myTransform.parent = collingObject.transform;
		}
	}

	//Playform Deparenting - Remove player from the playform when no longer touching;
	void OnCollisionExit(Collision collingObject)
	{
		if (collingObject.gameObject.tag == "Platform")
		{
			myTransform.parent = null;
		}
	}
}