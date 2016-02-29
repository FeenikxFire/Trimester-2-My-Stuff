using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	//Pause Menu Controls
	private bool pauseMenu = false;
	public GameObject pauseMenuObject;
	
	// Update is called once per frame
	void Update () {
		//Pause Menu
		if(Input.GetKeyDown ("escape") && pauseMenu == false){
			pauseMenu = true;
			pauseMenuObject.SetActive(true);
			Time.timeScale = 0.0f;
			Cursor.visible = true;
		}
	}

	public void returnToMain(){
		Application.LoadLevel ("main menu");
	}

	public void returnToGame(){
		Time.timeScale = 1.0f;
		pauseMenuObject.SetActive(false);
		pauseMenu = false;
		Cursor.visible = false;
	}
	

}
