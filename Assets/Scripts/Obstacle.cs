using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	bool hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("triggered");
		if (other.transform.tag == "SpaceshipCollider" && !hit) {
			Debug.Log (gameObject.name + " collided with " + other.transform.name);
			other.transform.GetComponent<ShipCollider>().SpawnDirt();
			hit = true;
			Destroy (gameObject, 0.5f);
		}
	}


}
