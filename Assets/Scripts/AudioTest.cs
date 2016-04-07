using UnityEngine;
using System.Collections;

public class AudioTest : MonoBehaviour {

	private AudioMain audiomain;

	// Use this for initialization
	void Start () {
	
		audiomain = GetComponent<AudioMain>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("b")) {

			audiomain.SpaceshipAudio ("boost");
		}

		if (Input.GetKeyDown ("c")) {

			audiomain.SpaceshipAudio ("boostfull");
		}

		if (Input.GetKeyDown ("v")) {

			audiomain.SpaceshipAudio ("boostunable");
		}

		if (Input.GetKeyDown ("s")) {

			audiomain.SpaceshipAudio ("shield");
		}

		if (Input.GetKeyDown ("a")) {

			audiomain.SpaceshipAudio ("shielddry");
		}

		if (Input.GetKeyDown ("d")) {

			audiomain.SpaceshipAudio ("shieldunable");
		}

		if (Input.GetKeyDown ("f")) {

			audiomain.SpaceshipAudio ("shieldimpact");
		}

		if (Input.GetKeyDown ("1")) {

			audiomain.SpaceshipAudio ("thrusters");
		}

	}
}
