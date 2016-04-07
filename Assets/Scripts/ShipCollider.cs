using UnityEngine;
using System.Collections;

public class ShipCollider : MonoBehaviour {


	float randomX, randomY;
	public GameObject oilDirt, pizzaDirt, toxicDirt, sodaDirt;
	public Vector2 spawnPosOffset;
//	Vector3 posToSpawn;
	int randomDirt;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnDirt (GameObject dirt, Vector3 posToSpawn, Vector2 offset) {
		GameObject dirty = Instantiate (dirt, new Vector3 (posToSpawn.x+offset.x, posToSpawn.y+offset.y, posToSpawn.z), Quaternion.identity) as GameObject;
		dirty.transform.gameObject.name = "DIRT!";
//		GameObject dirty = Instantiate (dirt, new Vector3 (transform.position.x+spawnPosOffset.x, transform.position.y+spawnPosOffset.y, 0), Quaternion.identity) as GameObject;
		dirty.transform.parent = GameObject.FindGameObjectWithTag("Spaceship").transform;
	}

}
