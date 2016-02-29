using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObject){

		if (otherObject.transform.tag == "Player") {
			Vector3 jumpVector = (transform.position - otherObject.transform.position).normalized;

			jumpVector.x *= 10;
			jumpVector.y = 15;
			jumpVector.z *= 10;

			otherObject.GetComponent<CharacterMotor>().SetVelocity(jumpVector);
		}
	}
}
