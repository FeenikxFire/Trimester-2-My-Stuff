using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject focusPoint;

    public int currentAmmo;
    public int reserveAmmo;

	// Use this for initialization
	void Start ()
    {
        currentAmmo = 30;
        reserveAmmo = 45;
	}
	
	// Update is called once per frame
	void Update () {
		//print (currentAmmo);
	}
}
