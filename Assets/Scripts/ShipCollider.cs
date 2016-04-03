using UnityEngine;
using System.Collections;

public class ShipCollider : MonoBehaviour {

	public Vector3 pos, size, test, startPos;
	float randomX, randomY;
	public GameObject dirt;
	Vector3 posToSpawn;
	// Use this for initialization
	void Start () {
//		pos = transform.localPosition;
//		size = transform.localScale;
//		startPos = new Vector3 (pos.x-(size.x/2), pos.y - (size.y/2), 0);
//		test = startPos+size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnDirt () {
		pos = transform.localPosition;
		size = transform.localScale;
		startPos = new Vector3 (pos.x-(size.x/2), pos.y - (size.y/2), 0);
		test = startPos+size;

		randomX = Random.Range (startPos.x, test.x);
		randomY = Random.Range (startPos.y, test.y);
		posToSpawn = new Vector3 (randomX, randomY, -5);
		GameObject dirty = Instantiate (dirt, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
		dirty.transform.parent = GameObject.FindGameObjectWithTag("Spaceship").transform;
	}

//	void OnDrawGizmosSelected() {
//		Vector3 center = pos;
//		float radius = size.x;
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireSphere(center, radius);
//	}
}
