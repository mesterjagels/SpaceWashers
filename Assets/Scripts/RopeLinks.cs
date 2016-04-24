using UnityEngine;
using System.Collections;

public class RopeLinks : MonoBehaviour {

	public Transform link;
	Transform tf, cnctdRb, cnctdRb2;
	LineRenderer line;
	DistanceJoint2D distJoint, distJoint2;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer> ();
		distJoint = GetComponent<DistanceJoint2D>();
		cnctdRb = distJoint.connectedBody.transform;
		tf = gameObject.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
//		line.SetPosition (0, tf.position);
//		line.SetPosition (1, cnctdRb.position);
	}
}
