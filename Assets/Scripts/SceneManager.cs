using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	//The frog
	Frog Frog;

	//TheTimer
	TextMesh CountDownLabel;
	int CurrentTime = 3;

	// Use this for initialization
	void Start () {
		Frog = FindObjectOfType<Frog>();
		CountDownLabel = (TextMesh) GameObject.Find ("CountDown").GetComponent<TextMesh>();

		//Countdown
		InvokeRepeating ("CountDown", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Countdown
	void CountDown() {
		Debug.Log (CurrentTime);
		if (--CurrentTime < 1) {
			Frog.CanMove ();
			CancelInvoke ("CountDown");

			GameObject.Find ("CountDown").GetComponent<MeshRenderer> ().enabled = false;
			return;
		}

		CountDownLabel.text = "" + CurrentTime;
	}

//	void Awake () {
//		QualitySettings.vSyncCount = 0;  // VSync must be disabled
//		Application.targetFrameRate = 45;
//	}
}
