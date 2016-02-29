using UnityEngine;
using System.Collections;
using System.Collections.Generic; //<------------------------ADDED FOR LISTS;

public class SelectionTrigger : MonoBehaviour
{

	//Selection Varaiables;
	SelectionMovement selectionMovement;

	public List<GameObject> selectedUnits = new List<GameObject> ();

	//Mouse Clicking;
	public Vector3 mousePositionA;
	public Vector3 mousePositionB;

	//Scaling Variables;
	private Vector3 scale;
	private Vector3 position;


	// Use this for initialization
	void Start ()
	{
		selectionMovement = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SelectionMovement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Get World positions for mouse click and drag;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		//Drag the selection trigger and resize between the starting and current mouse position;
		if(Input.GetMouseButton (0) && Physics.Raycast (ray, out hit))
		{
			mousePositionB = hit.point;
			Debug.DrawLine (Camera.main.transform.position, mousePositionA, Color.green);
			Debug.DrawLine (Camera.main.transform.position, mousePositionB, Color.red);

			scale = mousePositionA - mousePositionB;
			scale.y = 3;
			transform.localScale = scale;

			transform.position = (mousePositionA + mousePositionB)/2;
		}
		else
		{
			selectionMovement.selecting = false;
			selectionMovement.selectedUnits = selectedUnits;
			Destroy(this.gameObject);
		}

	}
	
	//Fill trigger with units;
	void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "Player")
		{
			selectedUnits.Add (otherObject.gameObject);
		}
	}

	void OnTriggerExit(Collider otherObject)
	{
		if(otherObject.tag == "Player")
		{
			selectedUnits.Remove (otherObject.gameObject);
		}
	}
}
