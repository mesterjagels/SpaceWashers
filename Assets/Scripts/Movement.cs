using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public KeyCode left, right, up, down, magnetBoots, detach;
	public float moveSpeed, nonRbSpeed, bracingSlow;
	Transform tf;
	Vector3 movePos;
	bool magnet;
	float slow;
	Rigidbody2D rb;
	public Vector3 curVelocity;
	Vector2 velX, velZ;
	public Transform cord;
	LineRenderer line;
	public float distToCord;
	// Use this for initialization
	void Start () {
		tf = gameObject.transform;
		movePos = new Vector3 (tf.position.x, 2.5f, tf.position.z);
		rb = gameObject.GetComponent<Rigidbody2D> ();
		slow = 1;
		magnet = false;
//		line = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) {
			if (!magnet) {
//				rb.AddForce (Vector2.left * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.left * moveSpeed * slow * Time.deltaTime * 0.2f);
				MoveRB ();
			} else {
				movePos.x -= (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}
		} else if (Input.GetKeyUp (left)) {
			velX = Vector2.zero;
		}

		if (Input.GetKey (right)) 
		{
			if (!magnet) 
			{
//				rb.AddForce (Vector2.right * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.right * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
				movePos.x += (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}


		}else if (Input.GetKeyUp (right)) {
			velX = Vector2.zero;
		}

		if (Input.GetKey (up)) 
		{
			if (!magnet) 
			{
//				rb.AddForce (Vector3.forward * moveSpeed * slow * Time.deltaTime);
				velZ += Vector2.up * moveSpeed * slow * Time.deltaTime*0.2f;
				MoveRB ();
			} else 
			{
				movePos.z += (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}
		}else if (Input.GetKeyUp (up)) {
			velZ = Vector2.zero;
		}

		if (Input.GetKey (down)) 
		{
			if (!magnet) 
			{
//				rb.AddForce (Vector3.back * moveSpeed * slow * Time.deltaTime);
				velZ += Vector2.down * moveSpeed * slow * Time.deltaTime*0.2f;
				MoveRB ();
			} else 
			{
				movePos.z -= (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}
		}else if (Input.GetKeyUp (down)) {
			velZ = Vector2.zero;
		}

		if (magnet && rb.velocity != Vector2.zero) {
			rb.velocity = Vector2.zero;
		}

		if (Input.GetKeyDown (magnetBoots) )
		{
			Invoke ("Boots", 1.5f);

		}

		if (Input.GetKeyDown (detach)) {
			cord.gameObject.GetComponent<RopeStart> ().Detach (tf.position);
		}
//		line.SetPosition (0, tf.position);
//		line.SetPosition (1, cord.position);
		distToCord = Vector3.Distance (tf.position, cord.position);
//		curVelocity = rb.velocity;
	}

	void Move () {
		tf.position = Vector3.Lerp (tf.position, movePos, 1);
	}

	void MoveRB () {
		rb.velocity = new Vector2 (velX.x, velZ.y);
	}

	void Boots () {
		if (magnet) 
		{
			slow = 1;
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0;
			magnet = false;
		} else 
		{
			slow = bracingSlow;
//			rb.detectCollisions = true;
			magnet = true;
		}
		movePos = tf.position;
	}
}
