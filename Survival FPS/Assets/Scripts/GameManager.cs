using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float mouseSensitivity = 5.0f;

	public bool yInvert = false;

	public int score = 0;
	public int scoreMultiplier = 1;
	public float scoreMultiplierTime;
	public float scoreMultiplierStopTime = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Reset scoreMultiplier back to 1 if Time.time > scoreMultiplierTime
		if (Time.time > scoreMultiplierTime) {
			scoreMultiplier = 1;
		}
	}

	public void scoreBonusMultiplier(int points){

		//Sequence of code important here for the first kill in bonus streak
		score += points * scoreMultiplier;
		scoreMultiplier += 1;
		scoreMultiplierTime = Time.time + scoreMultiplierStopTime;
	}
}
