using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	bool hit;
	Vector3 dirtSpawnPos;
	float xOffset;
	Vector2 offSet;
	public GameObject dirt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log ("triggered");
		if (other.transform.tag == "SpaceshipCollider" && !hit) {
//			Debug.Log (gameObject.name + " collided with " + other.transform.name);
			if (!other.transform.parent.parent.GetComponent<SpaceshipController>().shieldActive){
				RaycastHit2D rayhit = Physics2D.Raycast (transform.position, Vector2.down*2);
				if (rayhit){
					if (other.transform.name.Contains ("left")){
						offSet = new Vector2 (dirt.transform.localScale.x/2, dirt.transform.localScale.y/2);
					}
					if (other.transform.name.Contains ("right")){
						offSet = new Vector2 (-(dirt.transform.localScale.x/2), -(dirt.transform.localScale.y/2));
					}
					dirtSpawnPos = rayhit.point;
					other.transform.GetComponent<ShipCollider>().SpawnDirt(dirt, dirtSpawnPos, offSet);
					Debug.Log ("rayhit: " + rayhit.transform.gameObject);
				}

			}
			hit = true;
			Destroy (gameObject, 0.5f);
		}
	}


}
