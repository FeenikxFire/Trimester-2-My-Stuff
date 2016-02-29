using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	//Camera movement
	private int scrollDistance = 5;
	private float scrollSpeed = 10.0f;

	//Mouse Zoom
	public float cameraHeight = 10.0f;
	public GameObject theCamera;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		MoveCamera ();
		CameraZooming ();
	}

	//Move Camera Using Mouse;
	void MoveCamera ()
	{
		if(Input.mousePosition.x < scrollDistance)
		{
			transform.Translate (Vector3.right * -scrollSpeed * Time.deltaTime);
		}

		if(Input.mousePosition.x >= Screen.width - scrollDistance)
		{
			transform.Translate (Vector3.right * scrollSpeed * Time.deltaTime);
		}

		if(Input.mousePosition.y < scrollDistance)
		{
			transform.Translate (transform.forward * -scrollSpeed * Time.deltaTime);
		}

		if(Input.mousePosition.y >= Screen.height - scrollDistance)
		{
			transform.Translate (transform.forward * scrollSpeed * Time.deltaTime);
		}
	}

	//Controls affecting the zoom and placement of the camera
	void CameraZooming ()
	{
		//Move camera focus and height based on raycast from maincamera
		RaycastHit hit;
		Vector3 direction = (transform.position - theCamera.transform.position).normalized;

		if(Physics.Raycast (theCamera.transform.position, direction, out hit, 1000.0f))
		{

			Debug.DrawLine (theCamera.transform.position, hit.point);

			//Adjust heigh of the cameraheight based on the difference of the focus point;
			if(Vector3.Distance (transform.position, theCamera.transform.position) != cameraHeight && hit.transform.tag == "Ground")
			{
				Vector3 newPos = theCamera.transform.position;
				newPos.y = hit.point.y + cameraHeight;
				theCamera.transform.position = newPos;
			}
		}

		//Adjust the camera height but make it scroll wheel;
		if(Input.GetAxis ("Mouse ScrollWheel") > 0)
		{
			cameraHeight -= 1.0f;

			if(cameraHeight < 5.0f)
			{
				cameraHeight = 5.0f;
			}
		}
		else if(Input.GetAxis ("Mouse ScrollWheel") < 0)
		{
			cameraHeight += 1.0f;
			
			if(cameraHeight > 30.0f)
			{
				cameraHeight = 30.0f;
			}
		}
	}
}
