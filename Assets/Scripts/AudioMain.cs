using UnityEngine;
using System.Collections;

public class AudioMain : MonoBehaviour {

	public float thrusterSpeed = 50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpaceshipAudio(string s){

		if (s == "boost") {
			AkSoundEngine.PostEvent("spaceshipBoost", gameObject);
		}


		if (s == "boostfull") {
			AkSoundEngine.SetSwitch ("Spaceship_Boost_Status", "Full", gameObject);
			AkSoundEngine.PostEvent("spaceshipBoostStatus", gameObject);
		}

		if (s == "boostunable") {
			AkSoundEngine.SetSwitch ("Spaceship_Boost_Status", "Unable", gameObject);
			AkSoundEngine.PostEvent("spaceshipBoostStatus", gameObject);
		}


		if (s == "shield") {
			AkSoundEngine.PostEvent ("spaceshipShield", gameObject);
		}

		if (s == "shielddry") {
			AkSoundEngine.PostEvent ("spaceshipShieldDry", gameObject);
		}

		if (s == "shieldunable") {
			AkSoundEngine.PostEvent ("spaceshipShieldUnable", gameObject);
		}

		if (s == "shieldimpact") {
			AkSoundEngine.PostEvent ("spaceshipShieldImpact", gameObject);
		}

		if (s == "thrusters") {
			AkSoundEngine.PostEvent ("spaceshipThrusters", gameObject);
			AkSoundEngine.SetRTPCValue ("thrusterSpeed", thrusterSpeed); 
		}
	}
}


