using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject obstacle;
	public float timeSinceStart, difficultyMultiplier;
	public int numberToSpawn;
	public float [] xPos = new float[3];
//	GameObject spaceship;
	float spaceZSize;
	// Use this for initialization
	void Start () {
		spaceZSize = GameObject.FindGameObjectWithTag ("Spaceship").transform.localScale.z;

		for (int i = 0; i < numberToSpawn; i++) {
			int randomX = Random.Range (0, xPos.Length);
			Debug.Log ("randomx: " + randomX);
			GameObject obs = Instantiate (obstacle, new Vector3 (xPos[randomX], 1.69f, spaceZSize*i*2), Quaternion.identity) as GameObject;
			obs.transform.localScale = new Vector3 (obstacle.transform.localScale.x+i, obstacle.transform.localScale.y, obstacle.transform.localScale.z);
		}
	}
}
