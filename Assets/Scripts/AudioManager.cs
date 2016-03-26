using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	[Header ("Spaceship")]
	public AudioClip boosting; 
	public AudioClip boostStart; 
	public AudioClip boostEnd; 
	public AudioClip boostFull; 
	public AudioClip shieldOn; 
	public AudioClip shieldDepleted; 
	public AudioClip shieldFullyCharged; 
	public AudioClip wingBroken; 
	public AudioClip wingRepaired; 
	public AudioClip radarBroken; 
	public AudioClip radarRepaired; 
	public AudioClip thrusters; 
	public AudioClip sideThrusters;

	[Header ("Characters")]
	public List <AudioClip> characterOw;
	public AudioClip cleaningSquish;
	public AudioClip magnetBootsOn;
	public AudioClip magnetBootsOff;
	public AudioClip safetyCordReattach;

	[Header ("Obstacles")]
	public AudioClip asteroidField;
	public AudioClip asteroidHit;
	public AudioClip sodaCupHit;
	public AudioClip pizzaHit;
	public AudioClip pizzaTrayHit;
	public AudioClip barrelHit;

	[Header ("Music")]
	public AudioClip titleScreenMusic;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
