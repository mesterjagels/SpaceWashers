using UnityEngine;
using System.Collections;

public class ShipCollider : MonoBehaviour {

//	public Bounds bnds;
	float randomX, randomY;
	public GameObject oilDirt, pizzaDirt, toxicDirt, sodaDirt, particle;
//	public Vector2 spawnPosOffset;
//	public Vector3 max, min;
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
		if (!isTop){
			GameObject dirty = Instantiate (dirt, new Vector3 (posToSpawn.x+offset.x, posToSpawn.y, posToSpawn.z), Quaternion.identity) as GameObject;
			part = Instantiate (particle, new Vector3 (posToSpawn.x+offset.x, posToSpawn.y, posToSpawn.z-5), Quaternion.identity) as GameObject;
			dirty.transform.gameObject.name = "DIRT!";
			dirty.transform.parent = GameObject.FindGameObjectWithTag("Spaceship").transform;
		}else {
			randomTopPos = Random.Range (0, 3);
			GameObject dirty = Instantiate (dirt, transform.position+topPos, Quaternion.identity) as GameObject;
			part = Instantiate (particle, transform.position+topPos, Quaternion.identity) as GameObject;
			part.transform.position = new Vector3 (part.transform.position.x, part.transform.position.y, part.transform.position.z-5);
			dirty.transform.gameObject.name = "DIRT!";
			dirty.transform.parent = GameObject.FindGameObjectWithTag("Spaceship").transform;
		}
	}

}
