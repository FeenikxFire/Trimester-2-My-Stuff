using UnityEngine;
using System.Collections;

public class IngameGUI : MonoBehaviour {

	//GameManager gameManager;
	Player player;
	
	public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Cursor.visible = false; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//GUI elements
	void OnGUI () {

		//Crosshair
		GUI.Box(new Rect(Screen.width/2-5,Screen.height/2-8,100,30), "+", guiStyle);

		//Current Ammo - presently configured only for AK74u 
		GUI.Box(new Rect(Screen.width * 0.75f, Screen.height * 0.9f, 120, 30), "Ammo: " + player.currentAmmo + " / " + player.reserveAmmo, guiStyle);
	}
}
