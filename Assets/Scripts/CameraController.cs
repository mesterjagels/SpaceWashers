using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject [] players;
	Vector3 curPos, startPos;
	public float distBetweenPlayers;
	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < players.Length; i++) {
//			distBetweenPlayers += Mathf.Abs (players[i].transform.position
//		}
		distBetweenPlayers = Mathf.Abs (Vector3.Distance(players[0].transform.position, players[1].transform.position));
		if (startPos.y + distBetweenPlayers < 30) {
			transform.position = new Vector3 (startPos.x, startPos.y + distBetweenPlayers, startPos.z);
		}
	}
}
