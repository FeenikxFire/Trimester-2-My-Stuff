using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {

	private float cameraZ = -3.5f;

	private float yOffset = 1.0f;

	public GameObject trackTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//movement with updated Z and Y positions
		Vector3 newPosition = trackTarget.transform.position;
		newPosition.z = cameraZ;
		newPosition.y = newPosition.y + yOffset;
		transform.position = newPosition;
	}
}
