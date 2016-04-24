using UnityEngine;
using System.Collections.Generic;

public class RopeStart : MonoBehaviour {

//	string linkName;
//	public GameObject [] links;
	public float reelSpeed;
	Transform tf;
	BoxCollider2D box;
	public List <BoxCollider2D> shipBoxs;
	public List <PolygonCollider2D> shipPolys;
	GameObject [] shipColliders;
	public bool touchingBox, touchingPoly;
	// Use this for initialization
	void Start () {
//		linkName =
		shipColliders = GameObject.FindGameObjectsWithTag ("SpaceshipCollider");
		foreach (GameObject bx in shipColliders){
			if (bx.GetComponent<BoxCollider2D>()){
				shipBoxs.Add (bx.GetComponent<BoxCollider2D>());
			}
			if (bx.GetComponent<PolygonCollider2D>()) {
				shipPolys.Add (bx.GetComponent<PolygonCollider2D>());
			}
		}
		tf = gameObject.transform;
		box = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < shipBoxs.Count; i++) {
//			if (Physics2D.IsTouching (box, shipBoxs[i])){
//				touchingBox = true;
////				Debug.Log ("touching box");
//			}else {
//				touchingBox = false;
////				Debug.Log ("not touching box");
//			}
//		}
//		
//		for (int i = 0; i < shipPolys.Count; i++) {
//			if (Physics2D.IsTouching (box, shipPolys[i])){
//				touchingPoly = true;
////				Debug.Log ("touching poly");
//			}else {
//				touchingPoly = false;
////				Debug.Log ("not touching poly");
//			}
//		}
//		if (Physics2D.IsTouchingLayers (box, 1)){
//			Debug.Log ("test touch");
//		}else {
//			Debug.Log ("no touch");
//		}

	}

	public void Detach (Vector3 pos) {
//		tf.position = pos;
//		RaycastHit hit;
//		if (Physics.Raycast (tf.position, Vector3.forward, out hit)){
//		RaycastHit2D hit = Physics2D.Raycast (tf.position, 
//		if (hit.transform){
		Ray ray = new Ray (tf.position, Vector3.forward);
//		RaycastHit2D [] hits = Physics2D.GetRayIntersectionNonAlloc (ray, RaycastHit2D , 3);
//
//		if (hitCount > 0){
//			Debug.Log ("hit");
//			touchingBox = true;
//		}else {
//			touchingBox = false;
//			Debug.Log ("not hit");
//		}


//		if (touchingBox || touchingPoly){
			tf.position = Vector3.Lerp (tf.position, pos, reelSpeed*Time.deltaTime);
//		}
//			Debug.Log ("hit " + hit.transform.name);
//		}
//		}
	}
}
