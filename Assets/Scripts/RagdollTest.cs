using UnityEngine;
using System.Collections;

public class RagdollTest : MonoBehaviour {

<<<<<<< HEAD
=======
	public float force = 10;
>>>>>>> origin/master
	private Rigidbody2D rb;

	void Awake () {

		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
<<<<<<< HEAD
		if (Input.GetKeyDown ("w")) {
			rb.velocity = new Vector2(rb.velocity.x, 20);
		}

		if (Input.GetKeyDown ("a")) {
			rb.velocity = new Vector2(-20, rb.velocity.y);
		}

		if (Input.GetKeyDown ("d")) {
			rb.velocity = new Vector2(20, rb.velocity.y);
=======
		if (Input.GetKey ("w")) {
			rb.velocity = new Vector2(rb.velocity.x, force);
		}

		if (Input.GetKey ("a")) {
			rb.velocity = new Vector2(-force, rb.velocity.y);
		}

		if (Input.GetKey ("d")) {
			rb.velocity = new Vector2(force, rb.velocity.y);
		}

		if (Input.GetKey ("s")) {
			rb.velocity = new Vector2(rb.velocity.x, -force);
>>>>>>> origin/master
		}
	}
}
