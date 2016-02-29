using UnityEngine;
using System.Collections;
using System.Collections.Generic; // <--------------------------------------------ADDED for lists.

public class SelectionMovement : MonoBehaviour
{
	//Selection Variables;
	public List<GameObject> selectedUnits = new List<GameObject> ();
	public GameObject selectionTrigger;
	public bool selecting = false;

	//Mouse clicking variables;
	public Vector3 mousePosition;
	public Vector3 mousePositionA;
	public Vector3 mousePositionB;

	//Buidling Placement
	public bool placingBuilding = false;
	public GameObject preBuilding;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Fire a ray at the world.
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		RaycastHit hit;
		
		if(Physics.Raycast (ray, out hit))
		{
			mousePosition = hit.point;
		}

		//Left Click - Select NPC
		if(Input.GetMouseButton(0) && placingBuilding == false)
		{
			//Selecting Characters - multi-selection drag;
			if(selecting == false)
			{
				mousePositionA = hit.point;
				GameObject thisSelectionTrigger = Instantiate(selectionTrigger, mousePositionA, transform.rotation) as GameObject;
				thisSelectionTrigger.GetComponent<SelectionTrigger>().mousePositionA = mousePositionA;
				thisSelectionTrigger.GetComponent<SelectionTrigger>().mousePositionB = mousePositionB;

				selecting = true;
			}
		}

		//Right Click - Move SelectedUnit to mousePosition;
		if(Input.GetMouseButton(1) && selectedUnits.Count > 0 && placingBuilding == false)
		{
			Debug.DrawLine (Camera.main.transform.position, mousePosition, Color.green);

			foreach(GameObject unit in selectedUnits)
			{
				unit.GetComponent<PlayerUnit>().Move(mousePosition);
			}
		}
	}

	//Method to place buildings - places the prebuilding and passes on the building object;
	public void PlaceBuilding(GameObject building)
	{
		if(placingBuilding == false)
		{
			placingBuilding = true;
			selectedUnits = null;

			GameObject thisPreBuilding = Instantiate(preBuilding, mousePosition, transform.rotation) as GameObject;

			thisPreBuilding.GetComponent<PreBuilding>().building = building;
		}
	}

}
