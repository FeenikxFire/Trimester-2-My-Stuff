using UnityEngine;
using System.Collections;

public class PlayerUnit : MonoBehaviour {

	private Transform myTransform;

	//A* Navigation
	private NavMeshAgent agent;
	private float moveSpeed = 10.0f;
	private Vector3 movePosition;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		myTransform = this.transform;

		movePosition = myTransform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Move Unit
		agent.SetDestination (movePosition);

		Debug.DrawLine (myTransform.position, movePosition, Color.magenta);
	}

	public void Move(Vector3 newMovePosition)
	{
		movePosition = newMovePosition;
	}
}
