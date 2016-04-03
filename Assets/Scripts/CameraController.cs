using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject [] players;
	Vector3 curPos, startPos, targetPos;
	public float distBetweenPlayers;
	float startSize, size;
	Camera cam;
	public Transform spaceship;
	Transform tf;
	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		tf = gameObject.transform;
		startPos = tf.position;
		cam = this.GetComponent<Camera>();
		startSize = cam.orthographicSize;

	}
	
	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < players.Length; i++) {
//			distBetweenPlayers += Mathf.Abs (players[i].transform.position
//		}
		targetPos = new Vector3 (spaceship.position.x, spaceship.position.y, startPos.z);
		if (tf.position != targetPos) {
//			tf.position = Vector3.Lerp (tf.position, targetPos, Time.deltaTime*5);
			tf.position = targetPos;
		}
//		distBetweenPlayers = Mathf.Abs (Vector3.Distance(players[0].transform.position, players[1].transform.position));
//		if (cam.orthographicSize + distBetweenPlayers < 15) {
//			tf.position = new Vector3 (tf.position.x, startPos.y + distBetweenPlayers, startPos.z);
//			cam.orthographicSize = startSize+distBetweenPlayers;
//		}
	}
}
