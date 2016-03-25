using UnityEngine;
using System.Collections;

public class CleanableDirt : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "CleaningArm"){
			Debug.Log ("Cleaning arm entered trigger");

			GameObject.Destroy (gameObject);
		}
	}
}
