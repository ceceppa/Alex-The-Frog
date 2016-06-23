using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {
	Vector3 FrogPosition, StartFrogPosition, FrogEndPosition;
	float Jump = 1.8f;
	int RotateFrog = 0;
	Animation animation;

	/**
	 * At the beginning of the frog can't move, SceneManager will tell
	 * him when he can
	 */
	delegate void MoveTheFrog();
	MoveTheFrog handlerMoveTheFrog;

	/*
	 * This delegate take care when the frog is on top of a river model,
	 * as have to follow his movement.
	 */
	delegate void StayOnObject();
    StayOnObject handler;

	/**
	 * Now when the frong is on top of the model I can't just use the X model,
	 * otherwise can't move left/right properly.
	 * So I need to follow just his direction, by adding to the frog
	 * his X position change.
	 */
	GameObject RiverModel;
	Vector3 RiverModelPosition;

	void Start () {
		FrogPosition = StartFrogPosition = this.transform.localPosition;
		
		handler = doNothing;
		handlerMoveTheFrog = doNothing;

		animation = this.GetComponentInChildren<Animation> ();
	}

	// Update is called once per frame
	void Update() {
		handlerMoveTheFrog ();
	}

	void MoveTheModel () {
		bool play = false, stopFollowing = false;

		FrogPosition = this.transform.localPosition;
		handler();

		if (Input.anyKeyDown) {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				FrogPosition += Vector3.forward * Jump;
				RotateFrog = 0;

				play = true;
				stopFollowing = true;
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				FrogPosition += Vector3.back * Jump;
				RotateFrog = 180;

				play = true;
				stopFollowing = true;
			} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				FrogPosition += Vector3.left * Jump;
				RotateFrog = -90;

				play = true;
			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				FrogPosition += Vector3.right * Jump;
				RotateFrog = 90;

				play = true;
			}

			Debug.Log( "Key" );

			if (play) {
				//I'm jumping forward/backwards, don't have to follow the object anymore
				if (stopFollowing) {
					handler = doNothing;
				}
//				animation.Play ();
			}
		}

		this.transform.localPosition = FrogPosition;
//		this.transform.localRotation = Quaternion.identity;
		this.transform.localRotation = Quaternion.Euler(0, RotateFrog, 0);
	}

    /* Do really nothing */
    void doNothing() {
    }
    
    /**
     * Follow the model underneath the frog.
     * 
     * The function is used to allow the Frog to stay on top of the log
     */
    void FollowTheModel() {
		float x = RiverModel.transform.position.x - RiverModelPosition.x;
		FrogPosition += new Vector3 (x, 0, 0);

		RiverModelPosition = RiverModel.transform.position;
    }

	/**
	 * Am I died?
	 *
	 */
	void OnTriggerEnter(Collider col) {
        /*
         * If is a RiverModel log the frog have to stay on it, follow its movement
         */
		if (col.gameObject.tag == "RiverObject") {
			RiverModel = col.gameObject;
			RiverModelPosition = RiverModel.transform.position;
            
			handler = FollowTheModel;
			return;
            
		} else if (col.gameObject.tag == "FootPath") {
			handler = doNothing;
			RiverModel = null;
            
			return;
		} else if (col.gameObject.tag == "Finish") {
			handler = doNothing;
			RiverModel = null;
//			return;
		} else if (col.gameObject.tag != "Enemy") {
			return;
		}

		//Died
	    handler = doNothing;
		RiverModel = null;
		this.transform.localPosition = FrogPosition = StartFrogPosition;
//		this.transform.localRotation = Quaternion.identity;
		this.transform.localRotation = Quaternion.Euler(0, 0, 0);
	}

	/***
	 * Public events. This events can be sent by the scenemanager script
	 */
	public void CanMove() {
		handlerMoveTheFrog = MoveTheModel;
	}
}
