using UnityEngine;
using UnityEngine.UI;
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
		shieldBarRect = shieldBar.rectTransform;
		shieldBarScale = shieldBarRect.localScale;
		shieldBarScaleStart = shieldBarScale.x;
		curShield = maxShield;
		curBoost = maxBoost;
		shield.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
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
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && moveSpeed < maxSpeed){
			moveSpeed ++;
		}else if (Input.GetAxis("Mouse ScrollWheel") < 0 && moveSpeed > minSpeed){
			moveSpeed --;
		}

		if (Input.GetKey (shieldButton)){
			shieldActive = true;
		}else if (Input.GetKeyUp (shieldButton)){
			shieldActive = false;
		}

		if (Input.GetKeyDown (boostButton) && curBoost > 0){
			boostActive = true;
		}


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
		}

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


}
