using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("triggered");
		if (other.transform.tag == "Spaceship") {
			Debug.Log (gameObject.name + " collided with " + other.transform.name);
		}
	}
}
