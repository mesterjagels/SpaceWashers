using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Uniduino;

public class SpaceshipController : MonoBehaviour {

	public Arduino arduino;
	public int pinUp = 2;
	public int pinDown = 3;
	public int pinLeft = 4;
	public int pinRight = 5;
	public int pinBtn1 = 6;
	public int pinBtn2 = 7;

	public int pinBtn1Light = 8;
	public int pinBtn2Light = 9;

	public Vector2 startPos, lPos, rPos;
	public Vector3 targetPos;
	public Quaternion targetRot, startRot;
	Transform tf;
	bool move;
	float moveTime, rotTime;
	public AnimationClip left, right;
	Animation anim;
	public float minSpeed, maxSpeed;
	public float moveSpeed, laneSwitchSpeed, sideThrustSpeed, sideDecelerationSpeed, sideThrustAccelerationSpeed;
	float moveTo;
	public Vector2 sideVel;
	public Vector2 velocity;
	Rigidbody2D rb;
	public bool gridMovement;
	public bool decel;
	float sideAccTimer;
	public bool sideMove;
	public Text distTravelled, timeElapsed;
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
		sideAccTimer = 0;

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) | arduino.digitalRead (pinLeft) == 1) {
			GoLeft();
			sideMove = true;
		}
		if (Input.GetMouseButtonUp(0) && !gridMovement && rb.velocity.x > 0){
			decel = true;
			sideMove = false;
		}
		if (Input.GetMouseButton (1) | arduino.digitalRead (pinRight) == 1) {
			GoRight ();
			sideMove = true;
		}
		if (Input.GetMouseButtonUp(1) && !gridMovement && rb.velocity.x < 0){
			decel = true;
			Debug.Log ("decelerate");
			sideMove = false;
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && moveSpeed < maxSpeed){
			moveSpeed ++;
		}else if (Input.GetAxis("Mouse ScrollWheel") < 0 && moveSpeed > minSpeed){
			moveSpeed --;
		}


		if (move && gridMovement) {
			moveTime += Time.deltaTime*0.25f;
			rotTime = moveTime*2;
			if (tf.position.x != targetPos.x) {
				tf.position = Vector3.Lerp (tf.position, new Vector3 (targetPos.x, tf.position.y, tf.position.z), moveTime*laneSwitchSpeed);
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

		if (decel) {
			Decelerate();
		}
		if (sideMove){
			sideAccTimer += Time.deltaTime*sideThrustAccelerationSpeed;
		}else {
			sideAccTimer = 0;
		}

		moveTo = (Time.deltaTime * moveSpeed);
//		tf.position += Vector3.up * moveTo;
//		moveSpeed += Time.deltaTime*0.1f;
		rb.velocity = new Vector2 (rb.velocity.x,moveSpeed);
		velocity = GetComponent<Rigidbody2D> ().velocity;
//		Quaternion vibRot = new Quaternion (tf.rotation.x, 0.5f+(Mathf.PingPong (Time.time * vibSpeed,1)), tf.rotation.z, tf.rotation.w);
//		tf.rotation = vibRot;
//		transform.rotation.x = Mathf.Sin (Time.time * vibSpeed);
		distTravelled.text = (tf.position.y-startPos.y).ToString();
		timeElapsed.text += Time.unscaledDeltaTime.ToString();

	}

	void Decelerate () {
		if (rb.velocity.x < 0){
			rb.velocity = new Vector2 (rb.velocity.x+(sideDecelerationSpeed*Time.deltaTime), rb.velocity.y);
		}else {
			rb.velocity = new Vector2 (rb.velocity.x-(sideDecelerationSpeed*Time.deltaTime), rb.velocity.y);
		}
		if (rb.velocity.x == 0) {
			decel = false;
		}
	}

	//Method for setting up the different pins
	void ConfigurePins()
	{
		arduino.pinMode(pinUp, PinMode.INPUT);
		arduino.pinMode(pinDown, PinMode.INPUT);
		arduino.pinMode(pinLeft, PinMode.INPUT);
		arduino.pinMode(pinRight, PinMode.INPUT);
		arduino.pinMode(pinBtn1, PinMode.INPUT);

		arduino.pinMode(pinBtn1Light, PinMode.OUTPUT);

		//Only need to activate once for one pin, becuase all pins are on the same Port
		arduino.reportDigital((byte)(pinUp / 8), 1);

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

//			anim.clip = left;
//			anim.Play ();
			move = true;

		}
		if (!gridMovement) {
			Debug.Log ("left");
			if (sideVel.x > (-sideThrustSpeed)){
				sideVel = Vector2.left*sideThrustSpeed*sideAccTimer;
			}else {
				sideVel = Vector2.left*sideThrustSpeed;
			}
			rb.velocity = new Vector2 (sideVel.x, rb.velocity.y);
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
//			anim.clip = right;
//			anim.Play ();
			move = true;
		}
		if (!gridMovement) {
			sideVel = Vector2.right*sideThrustSpeed;
			if (sideVel.x < (sideThrustSpeed)){
				sideVel = Vector2.right*sideThrustSpeed*sideAccTimer;
			}else {
				sideVel = Vector2.right*sideThrustSpeed;
			}
			rb.velocity = new Vector2 (sideVel.x, rb.velocity.y);
		}
	}


}
