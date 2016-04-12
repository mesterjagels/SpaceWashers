﻿using UnityEngine;
using System.Collections;

public class PlanetSpawner : MonoBehaviour {

	public GameObject planets;
	public float interval = 0;
	public int distanceFromCam;
	private GameObject cam;


	void Awake(){
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		StartCoroutine ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Spawn(){
		while(true){
			Instantiate (planets, new Vector3 (Random.Range (cam.transform.position.x - 100, cam.transform.position.x + 100), cam.transform.position.y - distanceFromCam, cam.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds (interval);
		}
	}
}