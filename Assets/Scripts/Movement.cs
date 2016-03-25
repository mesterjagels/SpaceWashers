using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public KeyCode left, right, up, down, magnetBoots, detach;
	public float moveSpeed, moveSpeedWBoots, bracingSlow;
	Transform tf;
	Vector3 movePos;
	bool magnet;
	float slow;
	Rigidbody2D rb, spaceshipRb;
	public Vector3 curVelocity, curShipVel;
	public Vector2 velX, velZ;
	public Transform cord;
	LineRenderer line;
	public float distToCord;
	public bool washing;
	public bool moving;
	public float normalDrag, magnetDrag;
//	GameObject spaceship;


	void Start () {
		tf = gameObject.transform;
		movePos = new Vector3 (tf.position.x, 2.5f, tf.position.z);
		rb = gameObject.GetComponent<Rigidbody2D> ();
		slow = 1;
		magnet = false;
//		spaceship = GameObject.FindGameObjectWithTag("Spaceship");
		spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Rigidbody2D>();
		rb.drag = normalDrag;
//		line = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)
//		    || Input.GetAxis("Horizontal") < 0
		    ) {
			if (!magnet) {
//				rb.AddForce (Vector2.left * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.left * moveSpeed * slow * Time.deltaTime * 0.2f);
				MoveRB ();
			} else {
//				movePos.x -= (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
				velX += (Vector2.left * moveSpeedWBoots * slow * Time.deltaTime * 0.2f);
				MoveRB ();
			}
		} else if (Input.GetKeyUp (left)
//		           || Input.GetAxis("Horizontal") == 0
		           ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (right)
//		    || Input.GetAxis("Horizontal") > 0
		    ) {
			if (!magnet) 
			{
//				rb.AddForce (Vector2.right * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.right * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
//				movePos.x += (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
				velX += (Vector2.right * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB ();
			}


		}else if (Input.GetKeyUp (right)
//		          || Input.GetAxis("Horizontal") == 0
		          ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (up)
//		    || Input.GetAxis("Vertical") > 0
		    ) {
			if (!magnet) 
			{
//				rb.AddForce (Vector3.forward * moveSpeed * slow * Time.deltaTime);
				velZ += (Vector2.up * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
				velZ += (Vector2.up * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB ();
//				movePos.z += (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
			}
		}else if (Input.GetKeyUp (up)
//		          || Input.GetAxis("Vertical") == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (down)
//		    || Input.GetAxis("Vertical") < 0
		    ) {

			if (!magnet) 
			{
//				rb.AddForce (Vector3.back * moveSpeed * slow * Time.deltaTime);
				velZ += (Vector2.down * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
				velZ += (Vector2.down * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB ();
//				movePos.z -= (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
			}
		}else if (Input.GetKeyUp (down)
//		          || Input.GetAxis("Vertical") == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

//		if (magnet && rb.velocity != Vector2.zero) {
//			rb.velocity = Vector2.zero+spaceshipRb.velocity;
//		}

		if (Input.GetKeyDown (magnetBoots) )
		{
			Invoke ("Boots", 1.5f);
		}

		if (Input.GetAxis ("HorizontalR") != 0 || Input.GetAxis("VerticalR") != 0) {
			washing = true;
		}else{
			washing = false;
		}

		if (Input.GetKey (detach)) {
			cord.gameObject.GetComponent<RopeStart> ().Detach (tf.position);
		}
//		line.SetPosition (0, tf.position);
//		line.SetPosition (1, cord.position);
		distToCord = Vector3.Distance (tf.position, cord.position);
		curShipVel = spaceshipRb.velocity;
		curVelocity = rb.velocity;
		if (!moving){
			rb.velocity = spaceshipRb.velocity;
		}
	}

	void Move () {
		tf.position = Vector3.Lerp (tf.position, movePos, 1);
	}

	void MoveRB () {
		rb.velocity = spaceshipRb.velocity + new Vector2 (velX.x, velZ.y);
		moving = true;
	}

	void Boots () {
		if (magnet) 
		{
			slow = 1;
			rb.velocity = Vector2.zero+spaceshipRb.velocity;
			rb.angularVelocity = 0+spaceshipRb.angularVelocity;
			rb.drag = magnetDrag;
			magnet = false;
			velZ = Vector2.zero;
			velX = Vector2.zero;
		} else 
		{
			slow = bracingSlow;
//			rb.detectCollisions = true;
			magnet = true;
			velZ = Vector2.zero;
			velX = Vector2.zero;
			rb.drag = normalDrag;
		}
		movePos = tf.position;
	}
}
