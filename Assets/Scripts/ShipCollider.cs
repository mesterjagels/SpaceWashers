using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCollider : MonoBehaviour {

	float randomX, randomY;
	public GameObject oilDirt, pizzaDirt, toxicDirt, sodaDirt, particle;
	public List <GameObject> dirts;
	[Header ("For the top colliders")]
	public Vector3 topPos;
	Vector3 posToMoveTo;
//	Vector3 posToSpawn;
	bool particleSpawned;
	int randomDirt, randomTopPos;
	GameObject part;
	public bool isTop;

	// Use this for initialization
	void Start () {
		if (this.gameObject.name.Contains ("top") && !this.gameObject.name.Contains("middle")){
			isTop = true;
		}else {
			isTop = false;
		}
	}

	public void SpawnDirt (GameObject dirt, Vector3 posToSpawn, Vector2 offset) {
		if (GetComponent<PolygonCollider2D>()){
			randomX = Random.Range (GetComponent<PolygonCollider2D>().bounds.min.x, GetComponent<PolygonCollider2D>().bounds.max.x);
			randomY = Random.Range (GetComponent<PolygonCollider2D>().bounds.min.y, GetComponent<PolygonCollider2D>().bounds.max.y);
		}else {
			randomX = Random.Range (GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.max.x);
			randomY = Random.Range (GetComponent<BoxCollider2D>().bounds.min.y, GetComponent<BoxCollider2D>().bounds.max.y);
		}
		particleSpawned = true;
		posToSpawn = new Vector3 (posToSpawn.x, randomY, posToSpawn.z);
//		if (!isTop){
//			GameObject dirty = Instantiate (dirt, new Vector3 (posToSpawn.x+offset.x, posToSpawn.y, posToSpawn.z), Quaternion.identity) as GameObject;		
//			dirty.transform.gameObject.name = "DIRT!";
//			dirty.transform.parent = transform;
//
//		}else {
		int randomDirt = Random.Range (0, dirts.Count);
		GameObject dirty = Instantiate (dirts[randomDirt], transform.position+topPos, dirts[randomDirt].transform.rotation) as GameObject;
		dirty.transform.gameObject.name = "DIRT!";
		dirty.transform.parent = transform;
		int randomPosition = Random.Range (0, dirty.GetComponent<DirtController>().randomPos.Count);
		if (this.transform.name.Contains ("left")){
			dirty.transform.localPosition = dirty.GetComponent<DirtController>().randomPos[randomPosition];
		}else if (this.transform.name.Contains ("right")) {
			dirty.transform.localPosition = new Vector3 
				(-dirty.GetComponent<DirtController>().randomPos[randomPosition].x, dirty.GetComponent<DirtController>().randomPos[randomPosition].y, dirty.GetComponent<DirtController>().randomPos[randomPosition].z);
			dirty.transform.localScale = new Vector3 (-dirty.transform.localScale.x, dirty.transform.localScale.y, dirty.transform.localScale.z);
		}
//		}
	}
}