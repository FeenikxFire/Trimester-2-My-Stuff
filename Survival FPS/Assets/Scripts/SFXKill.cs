using UnityEngine;
using System.Collections;

public class SFXKill : MonoBehaviour {
	
	public float killTime;
	
	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		killTime = Time.time + killTime;
		
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= killTime){
			Destroy (this.gameObject);
		}
		
		if(myTransform.GetComponent<AudioSource>() != null)
			myTransform.GetComponent<AudioSource>().pitch = Time.timeScale;
	}
}
