using UnityEngine;
using System.Collections;

public class RagdollTest : MonoBehaviour {

	private Rigidbody2D rb;
	public float force = 1;


	void Awake () {

		rb = GetComponent<Rigidbody2D>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("w")) {
			rb.velocity = new Vector2(rb.velocity.x, force);
		}

		if (Input.GetKeyDown ("a")) {
			rb.velocity = new Vector2(-force, rb.velocity.y);
		}

		if (Input.GetKeyDown ("d")) {
			rb.velocity = new Vector2(force, rb.velocity.y);
		}

		if (Input.GetKeyDown ("s")) {
			rb.velocity = new Vector2(rb.velocity.x, -force);
		}

	}
}
