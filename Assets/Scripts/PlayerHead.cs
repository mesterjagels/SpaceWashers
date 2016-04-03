using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHead : MonoBehaviour {

	public GameObject head;
	public List <GameObject> children = new List<GameObject>();

	// Use this for initialization
	void Start () {
		head = transform.GetChild(0).GetChild(0).gameObject;
		for (int i = 0; i < transform.GetChild(0).childCount; i++) {
			children.Add (transform.GetChild(0).GetChild(i).gameObject);
		}
		for (int j = 0; j < children.Count; j++) {
			if (children[j] != head){
				head.GetComponent<Movement>().childrenNotThis.Add (children[j]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
