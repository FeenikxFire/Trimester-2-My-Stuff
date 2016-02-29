using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float zBoundaryTop = 60.0f;
	public float zBoundaryBottom = 80.0f;
	public float xBoundary = 100.0f;

	public float zBoundaryMoving = 0.0f;

	public int innerClip = 30;

	//enemy arary
	public GameObject[] enemyUnitList;

	//UI
	public GameObject enemiesUIObject;
	public int enemiesKilled;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//update enemy unit list
		enemyUnitList = GameObject.FindGameObjectsWithTag ("Enemy");
		enemiesUIObject.GetComponent<Text> ().text = "Enemies Killed : " + enemiesKilled;

	}
}
