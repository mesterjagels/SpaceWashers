using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Uniduino;

public class SpaceshipController : MonoBehaviour {

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
<<<<<<< HEAD
=======
	float timeHasElapsed, shieldRegenTimer;
	public float maxShield, shieldRegenTime, shieldDepleteTime, minShieldNeeded, curShield;
	public float maxBoost, boostRegenTime, boostDepleteTime, curBoost, boostSpeed;
	public KeyCode shieldButton, boostButton;
	public RawImage shieldBar, boostBar;
	float shieldBarScaleStart, boostBarScaleStart;
	RectTransform shieldBarRect, boostBarRect;
	Vector3 shieldBarScale, boostBarScale;
	public bool shieldActive, boostActive;
	public GameObject shield;
	float curBoostSpeed;

	//Used for setting up the arduino
	Arduino arduino;
	private int pinUp = 2;
	private int pinDown = 3;
	private int pinLeft = 4;
	private int pinRight = 5;
	private int pinBtn1 = 6;
	private int pinBtn2 = 7;
	private int pinBtn1Light = 8;
	private int pinBtn2Light = 9;

	//Used to check state changes on the arduino. Technically to create a buttonDown/buttonUp method for the arduino
	private int pinUpLast = 0
		, pinDownLast = 0
		, pinLeftLast = 0
		, pinRightLast = 0
		, pinBtn1Last = 0
		, pinBtn2Last = 0;

>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d
//	public float vibSpeed;
//	public float timeToMove;

	void Start () {
		anim = GetComponent<Animation>();
		rb = GetComponent<Rigidbody2D> ();

		tf = gameObject.transform;
		startPos = tf.position;
		startRot = tf.rotation;
		lPos = new Vector3 (startPos.x-10, startPos.y);
		rPos = new Vector3 (startPos.x+10, startPos.y);
<<<<<<< HEAD
		anim = GetComponent<Animation>();
		rb = GetComponent<Rigidbody2D> ();
		sideAccTimer = 0;
=======

		sideAccTimer = 0;

		shieldBarRect = shieldBar.rectTransform;
		shieldBarScale = shieldBarRect.localScale;
		shieldBarScaleStart = shieldBarScale.x;
		curShield = maxShield;
		shield.SetActive (false);

		curBoost = maxBoost;

		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if (Input.GetMouseButton (0)) {
			GoLeft();
			sideMove = true;
		}
		if (Input.GetMouseButtonUp(0) && !gridMovement && rb.velocity.x > 0){
			decel = true;
			sideMove = false;
		}
		if (Input.GetMouseButton (1)) {
			GoRight ();
			sideMove = true;
		}
		if (Input.GetMouseButtonUp(1) && !gridMovement && rb.velocity.x < 0){
			decel = true;
			Debug.Log ("decelerate");
			sideMove = false;
=======
		//Moved the movement into a seperate method to clean update a bit up
		MovementControls ();

		moveTo = (Time.deltaTime * moveSpeed);
//		tf.position += Vector3.up * moveTo;
//		moveSpeed += Time.deltaTime*0.1f;
		rb.velocity = new Vector2 (rb.velocity.x,moveSpeed+curBoostSpeed);
		velocity = GetComponent<Rigidbody2D> ().velocity;
//		Quaternion vibRot = new Quaternion (tf.rotation.x, 0.5f+(Mathf.PingPong (Time.time * vibSpeed,1)), tf.rotation.z, tf.rotation.w);
//		tf.rotation = vibRot;
//		transform.rotation.x = Mathf.Sin (Time.time * vibSpeed);
		distTravelled.text = (tf.position.y-startPos.y).ToString();
		timeHasElapsed += Time.unscaledDeltaTime;
		timeElapsed.text = timeHasElapsed.ToString("F2");

	}
	void MovementControls(){
		if (Input.GetMouseButton (0) || arduino.digitalRead(pinLeft) == 1) {
			GoLeft();
			sideMove = true;
			pinLeftLast = 1;
		}
		if (Input.GetMouseButtonUp(0) || arduino.digitalRead(pinLeft) == 0 && pinLeftLast == 1 && !gridMovement && rb.velocity.x > 0){
			decel = true;
			sideMove = false;
			pinLeftLast = 0;
		}
		if (Input.GetMouseButton (1)|| arduino.digitalRead(pinRight) == 1) {
			GoRight ();
			sideMove = true;
			pinRightLast = 1;
		}
		if (Input.GetMouseButtonUp(1) || arduino.digitalRead(pinRight) == 0 && pinRightLast == 1  && !gridMovement && rb.velocity.x < 0){
			decel = true;
			Debug.Log ("decelerate");
			sideMove = false;
			pinRightLast = 0;
>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && moveSpeed < maxSpeed){
			moveSpeed ++;
		}else if (Input.GetAxis("Mouse ScrollWheel") < 0 && moveSpeed > minSpeed){
			moveSpeed --;
<<<<<<< HEAD
		}

=======
		}

		if (Input.GetKey (shieldButton) || arduino.digitalRead(pinBtn1) == 1) {
			shieldActive = true;
			pinBtn1Last = 1;
		}else if (Input.GetKeyUp (shieldButton) || arduino.digitalRead(pinBtn1) == 0 && pinBtn1Last == 1){
			shieldActive = false;
			pinBtn1Last = 0;
		}

		if (Input.GetKeyDown (boostButton) || arduino.digitalRead(pinBtn2) == 1 && curBoost > 0){
			boostActive = true;
		}

>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d

		if (move && gridMovement) {
			moveTime += Time.deltaTime*0.25f;
			rotTime = moveTime*2;
			if (tf.position.x != targetPos.x) {
				tf.position = Vector3.Lerp (tf.position, new Vector3 (targetPos.x, tf.position.y, tf.position.z), moveTime*laneSwitchSpeed);
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

<<<<<<< HEAD
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
=======
		if (shieldActive && curShield > 0) {
			Shield ();
			if (!shield.activeInHierarchy){
				shield.SetActive (true);
			}
		}
		if (!shieldActive && curShield < maxShield) {
			ShieldRegen ();
			if (shield.activeInHierarchy){
				shield.SetActive (false);
			}
		}

		if (boostActive && curBoost > 0){
			Boost ();
		}
		if (!boostActive && curBoost < maxBoost){
			BoostRegen();
>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d
		}
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
<<<<<<< HEAD
//			anim.clip = right;
//			anim.Play ();
=======
			//			anim.clip = right;
			//			anim.Play ();
>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d
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
<<<<<<< HEAD
	}

=======
	}

	void Shield () {
		curShield -= Time.deltaTime*shieldDepleteTime;
		float prcntg = curShield/maxShield;
		shieldBarScale.x = prcntg;
		shieldBar.rectTransform.localScale = shieldBarScale;
	}

	void ShieldRegen () {
		curShield += Time.deltaTime*shieldRegenTime;
		float prcntg = curShield/maxShield;
		shieldBarScale.x = prcntg;
		shieldBar.rectTransform.localScale = shieldBarScale;
	}

	void Boost () {
		if (curBoost > 0){
			curBoost -= Time.deltaTime*boostDepleteTime;
			float prcntg = curBoost/maxBoost;
			boostBarScale.x = prcntg;
			boostBar.rectTransform.localScale = boostBarScale;
		}else {
			StopBoost();
			boostActive = false;
		}
	}

	void StopBoost () {
		curBoost += Time.deltaTime*boostRegenTime;
		float prcntg = curBoost/maxBoost;
		boostBarScale.x = prcntg;
		boostBar.rectTransform.localScale = boostBarScale;
	}

	void BoostRegen () {

	}

	void ConfigurePins()
	{
		arduino.pinMode(pinUp, PinMode.INPUT);
		arduino.pinMode(pinDown, PinMode.INPUT);
		arduino.pinMode(pinLeft, PinMode.INPUT);
		arduino.pinMode(pinRight, PinMode.INPUT);
		arduino.pinMode(pinBtn1, PinMode.INPUT);
		arduino.pinMode(pinBtn2, PinMode.INPUT);

		//Only need to activate once for one pin, becuase all pins are on the same Port
		arduino.reportDigital((byte)(pinUp / 8), 1);

	}
>>>>>>> 5dd8b22bf7d9f2fd979bd617be42d2e396c1831d

}
