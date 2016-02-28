using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public KeyCode left, right, up, down, magnetBoots;
	public float moveSpeed, nonRbSpeed, bracingSlow;
	Transform tf;
	Vector3 movePos;
	bool magnet;
	float slow;
	Rigidbody rb;
	public Vector3 curVelocity;
	Vector3 velX, velZ;
	// Use this for initialization
	void Start () {
		tf = gameObject.transform;
		movePos = new Vector3 (tf.position.x, 2.5f, tf.position.z);
		rb = gameObject.GetComponent<Rigidbody> ();
		slow = 1;
		magnet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)) 
		{
			if (!magnet) 
			{
				rb.AddForce (Vector3.left * moveSpeed * slow * Time.deltaTime);
				velX += (Vector3.left * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
				movePos.x -= (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}


		}

		if (Input.GetKey (right)) 
		{
			if (!magnet) 
			{
				rb.AddForce (Vector3.right * moveSpeed * slow * Time.deltaTime);
				velX += (Vector3.right * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB ();
			} else 
			{
				movePos.x += (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}


		}

		if (Input.GetKey (up)) 
		{
			if (!magnet) 
			{
//				rb.AddForce (Vector3.forward * moveSpeed * slow * Time.deltaTime);
				velZ += Vector3.forward * moveSpeed * slow * Time.deltaTime*0.2f;
				MoveRB ();
			} else 
			{
				movePos.z += (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}
		}

		if (Input.GetKey (down)) 
		{
			if (!magnet) 
			{
//				rb.AddForce (Vector3.back * moveSpeed * slow * Time.deltaTime);
				velZ += Vector3.back * moveSpeed * slow * Time.deltaTime*0.2f;
				MoveRB ();
			} else 
			{
				movePos.z -= (nonRbSpeed * slow * Time.deltaTime);
				Move ();
			}
		}

		if (magnet && rb.velocity != Vector3.zero) {
			rb.velocity = Vector3.zero;
		}

		if (Input.GetKeyDown (magnetBoots) )
		{
			Invoke ("Boots", 1.5f);

		}
//		curVelocity = rb.velocity;
	}

	void Move () {
		tf.position = Vector3.Lerp (tf.position, movePos, 1);
	}

	void MoveRB () {
		rb.velocity = new Vector3 (velX.x, 0, velZ.z);
	}

	void Boots () {
		if (magnet) 
		{
			slow = 1;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
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
