using UnityEngine;
using System;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject prefab;
	public int Speed = 10;
	public bool RightToLeft = false;

	
	/**
	 * Time in ms I have to wait before spawn the new enemy
	 */
	public Single repeatRate = 0.2f;

	/**
	 * The pattern define the number of objects to create for each row and can be modified via the Unity Editor
	 */
	public int[] Pattern = new int[10];

	/*
	 * The pattern "real" length length
	 */
	private int PatternLength;

	//Current "time"
	private int CurrentTime = 0;

	//Fog enemies
	GameObject[] Enemies;

	/**
	 * Instantiate all the enemy prefabs and store them into the array
	 */
	void Awake () {
		Array arr = Pattern;
		PatternLength = arr.Length;

		Enemies = new GameObject[PatternLength];

		/**
		 * According to the Pattern generate all the required "enemies". 
		 * So, if the value is:
		 *   1) Instantiate the prefab defined as "prefab" value
		 * 	 0) empty game object
		 */
		for (int i = 0; i < PatternLength; ++i) {
			Vector3 position = new Vector3 (0, 0, 0);
			GameObject clone;

			if (Pattern [i] == 1) {
				clone = Instantiate (prefab, position, Quaternion.identity) as GameObject;

				clone.transform.SetParent (this.transform);

				Enemy script = clone.GetComponent<Enemy> ();
				script.setSpeed (Speed * (RightToLeft ? -1 : 1));
			} else {
				clone = new GameObject ();
			}

			Enemies [i] = clone;
		}
	}

	void Start () {
		InvokeRepeating ("SpawnEnemy", 0f, repeatRate);
	}

	/**
	 * Check, accordingly to the Pattern, if I have to spawn the enemy.
	 * If so, do it, set its position and 'Start' it...
	 */
	void SpawnEnemy() {
		GameObject clone = Enemies[CurrentTime];

		clone.transform.localPosition = Vector3.zero;

		if (++CurrentTime >= PatternLength) {
			CurrentTime = 0;
		}
	}
}
