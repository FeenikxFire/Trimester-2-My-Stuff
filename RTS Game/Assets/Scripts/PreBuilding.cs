using UnityEngine;
using System.Collections;

public class PreBuilding : MonoBehaviour
{
	SelectionMovement selection;

	//Building To Place;
	public GameObject building;

	//Placement variables;
	public bool canPlace = true;
	public Color validPlacement = Color.green;
	public Color invalidPlacement = Color.red;

	// Use this for initialization
	void Start () 
	{
		selection = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SelectionMovement> ();

		//Rescale and recolour prebuilding placement;
		transform.localScale = building.transform.GetChild (0).transform.localScale; //Temporary - may cause problems later;
		transform.GetComponent<Renderer> ().material.color = validPlacement;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//PreBuilding follow cursor;
		if(selection.placingBuilding == true)
		{
			transform.position = selection.mousePosition;
		}

		//PreBuilding placement
		if(Input.GetMouseButton (0) && canPlace == true)
		{
			selection.placingBuilding = false;
			Instantiate(building, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider otherObject)
	{
		if(otherObject.tag == "Player" || otherObject.tag == "Obstacle" || otherObject.tag == "Building")
		{
			transform.GetComponent<Renderer> ().material.color = invalidPlacement;
			canPlace = false;
		}
	}

	void OnTriggerExit(Collider otherObject)
	{
		if(otherObject.tag == "Player" || otherObject.tag == "Obstacle" || otherObject.tag == "Building")
		{
			transform.GetComponent<Renderer> ().material.color = validPlacement;
			canPlace = true;
		}
	}
}
