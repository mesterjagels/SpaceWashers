using UnityEngine;
using System.Collections;

public class RagdollTest : MonoBehaviour {

	private Rigidbody2D rb;

	void Awake () {

		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("w")) {
			rb.velocity = new Vector2(rb.velocity.x, 20);
		}

		if (Input.GetKeyDown ("a")) {
			rb.velocity = new Vector2(-20, rb.velocity.y);
		}

		if (Input.GetKeyDown ("d")) {
			rb.velocity = new Vector2(20, rb.velocity.y);
		}
	}
}
