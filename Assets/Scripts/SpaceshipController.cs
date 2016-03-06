using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour {

	public Vector2 startPos, lPos, rPos;
	public Vector3 targetPos;
	public Quaternion targetRot, startRot;
	Transform tf;
	bool move;
	float moveTime, rotTime;
	public AnimationClip left, right;
	Animation anim;
	public float moveSpeed;
	float moveTo;
	public Vector2 velocity;
	Rigidbody2D rb;
//	public float vibSpeed;
//	public float timeToMove;
	// Use this for initialization
	void Start () {
		tf = gameObject.transform;
		startPos = tf.position;
		startRot = tf.rotation;
		lPos = new Vector3 (startPos.x-10, startPos.y);
		rPos = new Vector3 (startPos.x+10, startPos.y);
		anim = GetComponent<Animation>();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			GoLeft();
		}
		if (Input.GetMouseButtonDown (1)) {
			GoRight ();
		}

		if (move) {
			moveTime += Time.deltaTime*0.25f;
			rotTime = moveTime*2;
			if (tf.position.x != targetPos.x) {
				tf.position = Vector3.Lerp (tf.position, new Vector3 (targetPos.x, tf.position.y, tf.position.z), moveTime);
//				if (moveTime < 0.5f){
////					targetRot = new Quaternion (startRot.x, startRot.y-25, startRot.z, startRot.w);
////					tf.rotation = Quaternion.Lerp (tf.rotation, targetRot, rotTime);
////					tf.Rotate (Vector3.up, 25);
//				}else {
////					targetRot = startRot;
////					tf.rotation = Quaternion.Lerp (tf.rotation, targetRot, rotTime);
//				}
			}else {
				moveTime = 0;
				move = false;
			}
		}

		moveTo = (Time.deltaTime * moveSpeed);
//		tf.position += Vector3.up * moveTo;
		moveSpeed += Time.deltaTime*0.1f;
		rb.velocity = new Vector2 (0,moveSpeed);
		velocity = GetComponent<Rigidbody2D> ().velocity;
//		Quaternion vibRot = new Quaternion (tf.rotation.x, 0.5f+(Mathf.PingPong (Time.time * vibSpeed,1)), tf.rotation.z, tf.rotation.w);
//		tf.rotation = vibRot;
//		transform.rotation.x = Mathf.Sin (Time.time * vibSpeed);
	}

	void GoLeft () {
		if (tf.position.x != lPos.x && !move) {
			if (tf.position.x == startPos.x) {
//				lPos = new Vector2 (lPos.x, lPos.y);
				targetPos = new Vector3 (lPos.x, tf.position.y, tf.position.z);
			}else if ( tf.position.x == rPos.x) {
//				startPos = new Vector3 (startPos.x, startPos.y);
				targetPos = new Vector3 (startPos.x, tf.position.y, tf.position.z);
			}

			anim.clip = left;
			anim.Play ();
			move = true;

		}
	}

	void GoRight () {
		if (tf.position.x != rPos.x && !move) {
			if (tf.position.x == startPos.x) {
//				rPos = new Vector3 (rPos.x, rPos.y, tf.position.z);
				targetPos = new Vector3 (rPos.x, tf.position.y, tf.position.z);
			}else if (tf.position.x == lPos.x) {
//				startPos = new Vector3 (startPos.x, startPos.y, tf.position.z);
				targetPos = new Vector3 (startPos.x, tf.position.y, tf.position.z);
			}
			anim.clip = right;
			anim.Play ();
			move = true;
		}
	}
}
