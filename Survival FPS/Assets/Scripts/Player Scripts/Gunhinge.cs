using UnityEngine;
using System.Collections;

public class Gunhinge : MonoBehaviour {
	
	private Transform myTransform;
	
	private Vector3 adjustedPosition;
	
	// Use this for initialization
	void Start () {
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		gunHingePositioning();
	}

	//Move the positioning of the weapon based on the angle of view rotation. Gives slightly better weapon rotation.
	void gunHingePositioning(){
		
		adjustedPosition = myTransform.localPosition;
		
		if (myTransform.localRotation.x > 0.0f){
			adjustedPosition.y = 0.4f + myTransform.localRotation.x / 2.0f;
			adjustedPosition.z = 0.4f + -myTransform.localRotation.x * 1.5f;
		} 
		else if (myTransform.localRotation.x <= 0.0f){
			adjustedPosition.y = 0.4f + -myTransform.localRotation.x;
			adjustedPosition.z = 0.4f + myTransform.localRotation.x * 0.25f;
		}
		
		myTransform.localPosition = adjustedPosition;
	}
}
