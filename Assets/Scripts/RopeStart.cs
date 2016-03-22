using UnityEngine;
using System.Collections;

public class RopeStart : MonoBehaviour {

//	string linkName;
//	public GameObject [] links;
	public float reelSpeed;
	Transform tf;

	// Use this for initialization
	void Start () {
//		linkName =
		tf = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Detach (Vector3 pos) {
//		tf.position = pos;
		tf.position = Vector3.Lerp (tf.position, pos, reelSpeed*Time.deltaTime);
	}
}
