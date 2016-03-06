using UnityEngine;
using System.Collections;

public class BGInstance : MonoBehaviour {

	[HideInInspector]
	public Vector3 startPos;
	public bool resetting;
	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
//		if (transform.localPosition.z < (startPos.z - 100)) {
//			resetting = true;
//			ResetPos ();
//
//		}
	}

	public void ResetPos () {
		transform.localPosition = new Vector3 (startPos.x, startPos.y, startPos.z + 150);
		startPos = transform.localPosition;
		resetting = false;
	}
}
