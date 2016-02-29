using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	private Transform myTransform;

	private Vector3 playerPosition;

	private float moveSpeed = 100.0f;

	public float tilt;

	GameManager gameManager;

	//lazor weapon
	public GameObject lazor;
	//public GameObject muzzle1;    this is if we only wanted the one muzzle
	public GameObject[] muzzle;
	public float lazorFireTime;
	public float lazorFireRate = 0.2f;

	//Missile
	public GameObject missile;
	private float missileFireTime;
	private float missileFireRate = 0.2f;


	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Unity Tutorial version on how to move.
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement1 = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement1 * moveSpeed;

		//Adds Body Tilt
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

	
		movement ();

		checkBoundary ();

		fireLazors ();

		fireMissile ();


	}

	private void fireLazors ()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			if (Input.GetKey(KeyCode.Space) && Time.time > lazorFireTime)
			{
			//Instantiate (lazor,muzzle1.transform.position, muzzle1.transform.rotation); //single muzzle;

				for (int i = 0; i < muzzle.Length; i++)
				{
					Instantiate (lazor, muzzle[i].transform.position,muzzle[i].transform.rotation);
					GetComponent<AudioSource>().Play();
				}

				lazorFireTime = Time.time + lazorFireRate;
			}
		}
	}

	void fireMissile ()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (Input.GetKey(KeyCode.LeftControl) && Time.time > lazorFireTime)
			{
				//Instantiate (lazor,muzzle1.transform.position, muzzle1.transform.rotation); //single muzzle;
				
				for (int i = 0; i < muzzle.Length; i++)
				{
					Instantiate (missile, muzzle[i].transform.position,muzzle[i].transform.rotation);
					GetComponent<AudioSource>().Play();
				}
				
				missileFireTime = Time.time + missileFireRate;
			}
		}
	}

	private void movement()
	{

		playerPosition = myTransform.position;

		if(Input.GetKey("a"))
		{
			playerPosition.x = playerPosition.x - moveSpeed * Time.deltaTime;
		}

		if(Input.GetKey("d"))
		{
			playerPosition.x = playerPosition.x + moveSpeed * Time.deltaTime;
		}

		if(Input.GetKey("s"))
		{
			playerPosition.z = playerPosition.z - moveSpeed * Time.deltaTime;
		}

		if(Input.GetKey("w"))
		{
			playerPosition.z = playerPosition.z + moveSpeed * Time.deltaTime;
		}

		myTransform.position = playerPosition;

	}

	private void checkBoundary()
	{
		playerPosition = myTransform.position;

		//horizontal boundary check
		if(playerPosition.x <= -gameManager.xBoundary)
		{
			playerPosition.x = -gameManager.xBoundary;
		}
		else if (playerPosition.x >= gameManager.xBoundary)
		{
			playerPosition.x = gameManager.xBoundary;
		}

		//moving vertical
		if (playerPosition.z >= gameManager.zBoundaryTop)
		{
			playerPosition.z = gameManager.zBoundaryTop;
		}
		else if(playerPosition.z <= -gameManager.zBoundaryBottom)
		{
			playerPosition.z = -gameManager.zBoundaryBottom;
		}

		myTransform.position = playerPosition;
	}
}
