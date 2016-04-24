using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Uniduino;

public class SpaceshipController : MonoBehaviour {
	public Bounds spriteBounds;
	public Vector2 startPos, lPos, rPos;
	public Vector3 targetPos;
	public Quaternion targetRot, startRot;
	Transform tf;
	bool move;
	float curBoost, moveTime, rotTime, curShield;
	public float minSpeed, maxSpeed;
	public float speedPerAcceleration;
	public float moveSpeed, laneSwitchSpeed, sideThrustSpeed, sideDecelerationSpeed, sideThrustAccelerationSpeed;
	float moveTo;
	public Vector2 sideVel;
	public Vector2 velocity;
	Rigidbody2D rb;
	public bool gridMovement;
	public bool decel, shieldActive;
	float sideAccTimer;
	public bool sideMove, boostActivatable, shieldActivatable;
	public Text distTravelled, timeElapsed;
	float timeHasElapsed, shieldRegenTimer;
	public float maxShield, shieldRegenTime, shieldDepleteTime, minShieldNeeded;
	public float maxBoost, boostRegenTime, boostDepleteTime, boostSpeed, minBoostNeeded;
	public KeyCode shieldButton, boostButton;
	public RawImage shieldBar, boostBar, distBar;
	float shieldBarScaleStart, boostBarScaleStart, distBarScaleStart;
	RectTransform shieldBarRect, boostBarRect, distBarRect;
	Vector3 shieldBarScale, boostBarScale, distBarScale;
	public bool boostActive;
	public GameObject shield;
	float distPrcntg, prevMoveSpeed;
	public float maxXPos, curBoostSpeed;
	GameObject [] players;
	bool canAccel;
	public bool changingSpeed;
	float accelTimer;
	public int curSpeed, maxAccSpeed;
	public List <float> speeds = new List<float> ();

    private Arduino arduino;
    private int pinUp = 2;
    private int pinDown = 3;
    private int pinLeft = 4;
    private int pinRight = 5;
    private int pinBtn1 = 6;
    private int pinBtn2 = 7;
    private int pinBtn1Light = 8;
    private int pinBtn2Light = 9;

    //	public float vibSpeed;
    //	public float timeToMove;
    // Use this for initialization
    void Start () {
		tf = gameObject.transform;
		startPos = tf.position;
		startRot = tf.rotation;
		lPos = new Vector3 (startPos.x-10, startPos.y);
		rPos = new Vector3 (startPos.x+10, startPos.y);
		rb = GetComponent<Rigidbody2D> ();
		sideAccTimer = 0;
		shieldBarRect = shieldBar.rectTransform;
		shieldBarScale = shieldBarRect.localScale;
		shieldBarScaleStart = shieldBarScale.x;
		boostBarRect = shieldBar.rectTransform;
		boostBarScale = shieldBarRect.localScale;
		boostBarScaleStart = shieldBarScale.x;
		distBarRect = shieldBar.rectTransform;
		distBarScale = shieldBarRect.localScale;
		distBarScaleStart = shieldBarScale.x;
		curShield = maxShield;
		curBoost = maxBoost;
		decel = true;
		shield.SetActive (false);
		players = GameObject.FindGameObjectsWithTag("Player");        
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
		prevMoveSpeed = moveSpeed;
		moveSpeed = speeds [0];
		curSpeed = 0;
		maxAccSpeed = speeds.Count;
		changingSpeed = false;
		spriteBounds = GetComponent<SpriteRenderer>().sprite.bounds;
    }
	
	// Update is called once per frame
	void Update () {
        // Arduino needed to get a similar effect as ButtonUp
        int pinLeftLast = 0,
            pinRightLast = 0,
            pinUpLast = 0,
            pinDownLast = 0,
            pinBtn1Last = 0,
            pinBtn2Last = 0;


		if (Input.GetMouseButton(0) | arduino.digitalRead(pinLeft) == 1)
        {
            GoLeft();
            sideMove = true;
            pinLeftLast = 1;
        }
        if (Input.GetMouseButtonUp(0) | arduino.digitalRead(pinLeft) == 0 && !gridMovement && rb.velocity.x > 0 && pinLeftLast == 1)
        {
            decel = true;
            sideMove = false;
            pinLeftLast = 0;
        }
        if (Input.GetMouseButton(1) | arduino.digitalRead(pinRight) == 1)
        {
            GoRight();
            sideMove = true;
            pinRightLast = 1;
        }
        if (Input.GetMouseButtonUp(1) | arduino.digitalRead(pinRight) == 0 && !gridMovement && rb.velocity.x < 0 && pinRightLast == 1)
        {
            decel = true;
            Debug.Log("decelerate");
            sideMove = false;
            pinRightLast = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 | arduino.digitalRead(pinUp) == 1 && moveSpeed < maxSpeed){
			//moveSpeed += speedPerAcceleration;

			if (curSpeed < speeds.Count-1 && moveSpeed == speeds[curSpeed]){
				curSpeed += 1;
			}
		}else if (Input.GetAxis("Mouse ScrollWheel") < 0 | arduino.digitalRead(pinDown) == 1 && moveSpeed > minSpeed){
			//moveSpeed -= speedPerAcceleration;
			if (curSpeed+1 >= 0 && moveSpeed == speeds[curSpeed]){
				curSpeed -= 1;
			}
		}
		if (moveSpeed > maxSpeed){
			moveSpeed = maxSpeed;
		}
		if (moveSpeed < minSpeed){
			moveSpeed = minSpeed;
		}

        if ((Input.GetKey (shieldButton) | arduino.digitalRead (pinBtn1) == 1) && shieldActivatable && curShield > 0) {
			shieldActive = true;
			pinBtn1Last = 1;
		} else {
			shieldActive = false;
		}
		        
        if (curShield <= 0 && shieldActive)
        {
            shieldActive = false;
        }

        if (Input.GetKeyDown(boostButton) | arduino.digitalRead(pinBtn2) == 1 && boostActivatable)
        {
            boostActive = true;
        }
        if (boostActive && curBoost <= 0)
        {
            boostActive = false;
        }

//        if (move && gridMovement) {
//			moveTime += Time.deltaTime*0.25f;
//			rotTime = moveTime*2;
//			if (tf.position.x != targetPos.x) {
//				tf.position = Vector3.MoveTowards (tf.position, new Vector3 (targetPos.x, tf.position.y, tf.position.z), moveTime*laneSwitchSpeed);
//				//tf.position = Vector3.Lerp (tf.position, new Vector3 (targetPos.x, tf.position.y, tf.position.z), moveTime*laneSwitchSpeed);
//			}else {
//				moveTime = 0;
//				move = false;
//			}
//		}

		if (decel) {
			Decelerate();
		}
		if (sideMove){
			sideAccTimer += Time.deltaTime*sideThrustAccelerationSpeed;
		}else {
			sideAccTimer = 0;
		}

		if (shieldActive && curShield > 0) {
			Shield ();
			if (!shield.activeInHierarchy){
				shield.SetActive (true);
			}
		}
		if (curShield > minShieldNeeded) {
			shieldActivatable = true;
		} else {
			shieldActivatable = false;
		}
		if (!shieldActive && curShield < maxShield) {
			ShieldRegen ();
			if (shield.activeInHierarchy){
				shield.SetActive (false);
			}
		}
		if (curBoost > minBoostNeeded) {
			boostActivatable = true;
		} else {
			boostActivatable = false;
		}

		if (boostActive && curBoost > 0){
			Boost ();
		}
		if (boostActive && curBoost <= 0) {
			StopBoost ();
		}

		if (!boostActive && curBoost < maxBoost){
			BoostRegen();
		}

		if (moveSpeed != speeds [curSpeed]) {
			Accelerate (curSpeed);
		}

		moveTo = (Time.deltaTime * moveSpeed);
//		tf.position += Vector3.up * moveTo;
//		moveSpeed += Time.deltaTime*0.1f;
		rb.velocity = new Vector2 (rb.velocity.x,moveSpeed+curBoostSpeed);
		velocity = GetComponent<Rigidbody2D> ().velocity;
//		Quaternion vibRot = new Quaternion (tf.rotation.x, 0.5f+(Mathf.PingPong (Time.time * vibSpeed,1)), tf.rotation.z, tf.rotation.w);
//		tf.rotation = vibRot;
//		transform.rotation.x = Mathf.Sin (Time.time * vibSpeed);
		distTravelled.text = "Dist: " + (tf.position.y-startPos.y).ToString();
		timeHasElapsed += Time.unscaledDeltaTime;
		timeElapsed.text = timeHasElapsed.ToString("F2");
		distPrcntg = (tf.position.y - startPos.y) / GameObject.FindGameObjectWithTag ("ObstacleController").GetComponent<ObstacleSpawner> ().distToTravel;
		distBarScale.x = distPrcntg;
		distBar.rectTransform.localScale = distBarScale;
		/*
		if (tf.position.x < (-maxXPos)) {
			tf.position = new Vector3 ((maxXPos - 2), tf.position.y, tf.position.z);
		}

		if (tf.position.x > (maxXPos)) {
			tf.position = new Vector3 (((-maxXPos) + 2), tf.position.y, tf.position.z);
		}
		*/

		if (moveSpeed - prevMoveSpeed > 1) {
			prevMoveSpeed = moveSpeed;
			for (int i = 0; i < players.Length; i++) {
				if (players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity != rb.velocity){
					players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity = rb.velocity;		
				}
			}
		}
		if (prevMoveSpeed - moveSpeed > 1) {
			prevMoveSpeed = moveSpeed;
			for (int i = 0; i < players.Length; i++) {
				if (players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity != rb.velocity){
					players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity = rb.velocity;		
				}
			}
		}

		//if (moveSpeed != speeds [curSpeed]) {
			Accelerate (curSpeed);
		//}
		if (moveSpeed < speeds[curSpeed] && Mathf.Abs(speeds [curSpeed] - moveSpeed) <= 0.1f && !boostActive) {
			moveSpeed = speeds[curSpeed];
		}

		if (moveSpeed < speeds [curSpeed] && Mathf.Abs(moveSpeed - speeds [curSpeed]) <= 0.1f && !boostActive) {
			moveSpeed = speeds[curSpeed];
		}

		if (curSpeed >= speeds.Count - 1) {
			curSpeed = speeds.Count-1;
		}
		if (curSpeed <= 0) {
			curSpeed = 0;
		}

		if (!boostActive && curBoostSpeed > 0){
			curBoostSpeed -= Time.deltaTime*30;
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
			if (sideVel.x < (-sideThrustSpeed)){
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

				targetPos = new Vector3 (rPos.x, tf.position.y, tf.position.z);
			}else if (tf.position.x == lPos.x) {

				targetPos = new Vector3 (startPos.x, tf.position.y, tf.position.z);
			}

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
			for (int i = 0; i < players.Length; i++) {
				if (players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity != rb.velocity && !players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().magnet){
					players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().rb.velocity = rb.velocity;		
				}

			}
			if (curBoostSpeed < boostSpeed) {
				curBoostSpeed += Time.deltaTime * boostSpeed;
			} else {
				curBoostSpeed = boostSpeed;
			}
		}else {
			StopBoost();
			boostActive = false;
		}

	}

	void StopBoost () {
		curBoost += Time.deltaTime*boostRegenTime;
		float prcntg = curBoost/maxBoost;
		boostBarScale.x = prcntg;
//		curBoostSpeed = 0;
		boostBar.rectTransform.localScale = boostBarScale;
		for (int i = 0; i < players.Length; i++) {
			if (players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().magnet){
				players[i].GetComponent<PlayerHead>().head.GetComponent<Movement>().stoppingBoost = true;
				Debug.Log ("stopboost");
			}
		}
		if (curBoostSpeed > 0) {
			curBoostSpeed -= Time.deltaTime * (boostSpeed*0.5f);
		} else {
			curBoostSpeed = 0;
		}
	}

	void BoostRegen () {
		curBoost += Time.deltaTime*boostRegenTime;
		float prcntg = curBoost/maxBoost;
		boostBarScale.x = prcntg;
		boostBar.rectTransform.localScale = boostBarScale;
	}

    void ConfigurePins()
    {
        arduino.pinMode(pinUp, PinMode.INPUT);
        arduino.pinMode(pinDown, PinMode.INPUT);
        arduino.pinMode(pinLeft, PinMode.INPUT);
        arduino.pinMode(pinRight, PinMode.INPUT);
        arduino.pinMode(pinBtn1, PinMode.INPUT);
        arduino.pinMode(pinBtn2, PinMode.INPUT);

        arduino.pinMode(pinBtn1Light, PinMode.OUTPUT);

        //Only need to activate once for one pin, becuase all pins are on the same Port
        arduino.reportDigital((byte)(pinUp / 8), 1);
    }

	void Accelerate (int speedToGo) {
		if (speedToGo <= maxAccSpeed-1 && moveSpeed < speeds[speedToGo]) {
			if (moveSpeed <= speeds[speedToGo]) {
				moveSpeed += (Time.deltaTime*2);
			}
//			else if (moveSpeed-speeds[speedToGo] >= 0.25f){
//				moveSpeed = speeds[speedToGo];
//			}
			changingSpeed = true;
		}else if (speedToGo >= 0  && moveSpeed > speeds[speedToGo]) {
			if (moveSpeed >= speeds[speedToGo]) {
				moveSpeed -= (Time.deltaTime*2);
			}
//			else if (moveSpeed-speeds[speedToGo] >= 0.25f){
//				moveSpeed = speeds[speedToGo];
//			}
			changingSpeed = true;
		}else {
			changingSpeed = false;
		}
	}
}
