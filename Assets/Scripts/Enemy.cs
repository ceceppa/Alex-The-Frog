using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private int Speed = 10;
	public float RotateY = 0;
	public float RotateZ = 0;

	// Use this for initialization
	void Start () {
		//Rotate the model
		this.transform.Rotate(0, RotateY, RotateZ);
		this.transform.localPosition = Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3( transform.localPosition.x + Speed * Time.deltaTime,
			0, 0 );
		this.transform.localPosition = pos;
	}

	/*
	 * Internal routine
	 */
	public void setSpeed( int speed ) {
		this.Speed = speed;
	}
}
