using UnityEngine;
using System.Collections;

public class RopeLinks : MonoBehaviour {

	public Transform link;
	Transform tf;
	LineRenderer line;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		tf = gameObject.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		line.SetPosition (0, tf.position);
		line.SetPosition (1, link.position);
	}
}
