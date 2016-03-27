using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject [] obstacles;
//	public float timeSinceStart, difficultyMultiplier;
	public int easyZones, mediumZones, hardZones;
	[Header ("Randomizes the order of the zones' difficulty")]
	public bool randomOrder;
	public float [] xPos = new float[3];
	[Header ("Read only, don't change")]
	public int numberOfZones;
	int numberSpawned;
	public float distToTravel;
//	GameObject spaceship;
	float spaceYSize;
	int curEz, curMed, curHard;
	// Use this for initialization
	void Start () {
		spaceYSize = GameObject.FindGameObjectWithTag ("Spaceship").transform.localScale.y;
		numberOfZones = easyZones + mediumZones + hardZones;


//		for (int i = 0; i < numberToSpawn; i++) {
//			int randomX = Random.Range (0, xPos.Length);
//			int randomObs = Random.Range (0, obstacles.Length);
//			Debug.Log ("randomx: " + randomX);
//			GameObject obs = Instantiate (obstacles[randomObs], new Vector3 (xPos[randomX], spaceYSize +(spaceYSize*i*2), 0), Quaternion.identity) as GameObject;
////			obs.transform.localScale = new Vector3 (obs.transform.localScale.x+i, obs.transform.localScale.y, obs.transform.localScale.z);
//		}

		for (int i = 0; i < numberOfZones; i++) {
			int randomX = Random.Range (-100, 100);
			int randomObs = Random.Range (0, obstacles.Length);

			if (!randomOrder) 
			{
				if (i <= (easyZones - 1)) {
					GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), Quaternion.identity) as GameObject;
					obs.transform.name = "Easy" + i;
					obs.transform.parent = gameObject.transform;
				} else if (i > (easyZones - 1) && i <= (easyZones + mediumZones - 1)) {
					GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), Quaternion.identity) as GameObject;
					obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 1.5f, obs.transform.localScale.y, obs.transform.localScale.z);
					obs.transform.name = "Medium" + i;
					obs.transform.parent = gameObject.transform;
				} else if (i > (easyZones + mediumZones - 1) && i <= (easyZones + mediumZones + hardZones - 1)) {
					GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), Quaternion.identity) as GameObject;
					obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 2, obs.transform.localScale.y, obs.transform.localScale.z);
					obs.transform.name = "Hard" + i;
					obs.transform.parent = gameObject.transform;
				}
			} else 
			{
				int diff = Random.Range (0, 3);
				if (diff == 0) {
					SpawnEasy (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), i);
					curEz++;
				}else if (diff == 1) {
					SpawnMedium (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), i);
					curMed++;
				}else if (diff == 2) {
					SpawnHard (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), i);
					curMed++;
				}
			}
		}

//		for (int i = 0; i < easyZones; i++) 
//		{
//			int randomX = Random.Range (0, xPos.Length);
//			int randomObs = Random.Range (0, obstacles.Length);
//			GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * i * 2.5f), 0), Quaternion.identity) as GameObject;
//			obs.transform.name = "Easy" + i;
//			numberSpawned++;
//			Debug.Log ("easy " + numberSpawned);
//		}
//
//		for (int j = 0; j < mediumZones; j++) 
//		{
//			int randomX = Random.Range (0, xPos.Length);
//			int randomObs = Random.Range (0, obstacles.Length);
//			if (j == 0) {
//				GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, (spaceYSize*2) + (spaceYSize * (j + 1) * numberSpawned * 2), 0), Quaternion.identity) as GameObject;
//				obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 1.5f, obs.transform.localScale.y, obs.transform.localScale.z);
//				obs.transform.name = "Medium" + j;
//			} else {
//				GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * (j + 1) * numberSpawned * 2), 0), Quaternion.identity) as GameObject;
//				obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 1.5f, obs.transform.localScale.y, obs.transform.localScale.z);
//				obs.transform.name = "Medium" + j;
//			}
//
//			numberSpawned++;
//			Debug.Log ("medium " + numberSpawned);
//		}
//
//		for (int k = 0; k < hardZones; k++) 
//		{
//			int randomX = Random.Range (0, xPos.Length);
//			int randomObs = Random.Range (0, obstacles.Length);
//			if (k == 0) {
//				GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, (spaceYSize*2) + (spaceYSize * (k + 2) * numberSpawned * 1.75f), 0), Quaternion.identity) as GameObject;
//				obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 2, obs.transform.localScale.y, obs.transform.localScale.z);
//				obs.transform.name = "Hard" + k;
//			} else {
//				GameObject obs = Instantiate (obstacles [randomObs], new Vector3 (randomX, spaceYSize + (spaceYSize * (k + 2) * numberSpawned * 1.75f), 0), Quaternion.identity) as GameObject;
//				obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 2, obs.transform.localScale.y, obs.transform.localScale.z);
//				obs.transform.name = "Hard" + k;
//			}
//
//			numberSpawned++;
//			Debug.Log ("hard " + numberSpawned);
//		}

	}

	void SpawnEasy (GameObject obj, Vector3 pos, int nr) {
		GameObject obs = Instantiate (obj, pos, Quaternion.identity) as GameObject;
		obs.transform.name = "Easy " + nr;
		obs.transform.parent = gameObject.transform;
	}

	void SpawnMedium (GameObject obj, Vector3 pos, int nr) {
		GameObject obs = Instantiate (obj, pos, Quaternion.identity) as GameObject;
		obs.transform.name = "Medium " + nr;
		obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 1.5f, obs.transform.localScale.y, obs.transform.localScale.z);
		obs.transform.parent = gameObject.transform;
	}

	void SpawnHard (GameObject obj, Vector3 pos, int nr) {
		GameObject obs = Instantiate (obj, pos, Quaternion.identity) as GameObject;
		obs.transform.name = "Hard " + nr;
		obs.transform.localScale = new Vector3 (obs.transform.localScale.x * 2, obs.transform.localScale.y, obs.transform.localScale.z);
		obs.transform.parent = gameObject.transform;
	}
}


//specified number of zones, each with a specified difficulty. Eg. one level is 10 zones, zone 1-3 is easy, zone 4-7 is medium, zone 8-10 is hard.