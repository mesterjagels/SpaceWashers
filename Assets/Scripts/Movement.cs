using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class Movement : MonoBehaviour {



	public float moveSpeed, moveSpeedWBoots, bracingSlow;
	Transform tf;
	Vector3 movePos;
	float slow;

	LineRenderer line;

	public float normalDrag, magnetDrag;
	int playerNr;
	string lHorAxis, rHorAxis, lVerAxis, rVerAxis;

	GameObject playerHolder;
	SpriteRenderer thisSprite;
	List <GameObject> topDownSprites = new List<GameObject>();
	float magnetMass = 10;
	Rigidbody2D ragdollHandRb;

	Vector2 ragdollHandVel;
	public float magnetDelay;
	public float washHandSpeed;
	DistanceJoint2D [] distJs;
	public DistanceJoint2D magnetDistJ;
	[Header ("Movement without controllers")]
	public KeyCode left; 
	public KeyCode right, up, down, magnetBoots, detach;
	[Header ("Public fields for gameobjects needed")]
	public GameObject topDownChar;
	public GameObject washHand;
	public GameObject ragdollHand;
	public Transform cord;
	[Header ("Public only for debugging")]
	public bool moving;
	public float distToCord;
	public bool washing;
	public Vector3 curVelocity, curShipVel;
	public Vector2 velX, velZ;
	public bool magnet;
	public Vector3 posOnShip;
	public bool stoppingBoost;
	[HideInInspector]
	public List <GameObject> childrenNotThis = new List<GameObject>();
	[HideInInspector]
	public Rigidbody2D [] childRbs;
	[HideInInspector]
	public Rigidbody2D rb, spaceshipRb;
	[HideInInspector]
	public InputDevice inputDevice;
//	public GameObject [] childSprites;
//	GameObject spaceship;
	// Use this for initialization
	void Start () {
		tf = gameObject.transform;
		movePos = new Vector3 (tf.position.x, 2.5f, tf.position.z);
		rb = gameObject.GetComponent<Rigidbody2D> ();
		slow = 1;
		magnet = false;
//		spaceship = GameObject.FindGameObjectWithTag("Spaceship");
		spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Rigidbody2D>();
		rb.drag = normalDrag;
//		if (tf.parent.parent.name.Contains("Player1")) {
//			playerNr = 1;
//		}else if (tf.parent.parent.name.Contains("Player2")) {
//			playerNr = 2;
//		}else if(tf.parent.parent.name.Contains("Player3")) {
//			playerNr = 3;
//		}else if(tf.parent.parent.name.Contains("Player4")) {
//			playerNr = 4;
//		}else if(tf.parent.parent.name.Contains("Player0")) {
//			playerNr = 0;
//		}

		for (int i = 0; i < 10; i++) {
			if (tf.parent.parent.name.Contains("Player" + i.ToString())){
				playerNr = i;
			}
		}

		distJs = GetComponents<DistanceJoint2D>();
		foreach (DistanceJoint2D dj in distJs) {
			if (dj.anchor.y == (-0.1f)){
				magnetDistJ = dj;
			}
		}
//		if (playerNr == 1) {
//			lHorAxis = "Horizontal";
//			lVerAxis = "Vertical";
//			rHorAxis = "HorizontalR1";
//			rVerAxis = "VerticalR1";
//		} else if (playerNr == 2) {
//			lHorAxis = "HorizontalL2";
//			lVerAxis = "VerticalL2";
//			rHorAxis = "HorizontalR2";
//			rVerAxis = "VerticalR2";
//		} else if (playerNr == 3) {
//			lHorAxis = "HorizontalL2";
//			lVerAxis = "VerticalL2";
//			rHorAxis = "HorizontalR3";
//			rVerAxis = "VerticalR3";
//		}
		inputDevice = (InputManager.Devices.Count > playerNr) ? InputManager.Devices[playerNr] : null;
		Debug.Log ("player nr: " + playerNr);
//		topDownChar.SetActive (false);
		spaceshipRb = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Rigidbody2D>();
		thisSprite = GetComponent<SpriteRenderer>();
		playerHolder = transform.parent.parent.gameObject;
		childRbs = tf.parent.GetComponentsInChildren<Rigidbody2D>();
		ragdollHandRb = ragdollHand.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (left)
		    || inputDevice.Direction.X < 0
		    ) {
			if (!magnet) {
//				rb.AddForce (Vector2.left * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.left * moveSpeed * slow * Time.deltaTime * 0.2f);
				MoveRB (velX, velZ);
			} else {
//				movePos.x -= (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
				velX += (Vector2.left * moveSpeedWBoots * slow * Time.deltaTime * 0.2f);
				MoveRB (velX, velZ);
			}
		} else if (Input.GetKeyUp (left)
		           || inputDevice.Direction.X == 0
		           ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (right)
		    || inputDevice.Direction.X > 0
		    ) {
			if (!magnet) 
			{
				rb.AddForce (Vector2.right * moveSpeed * slow * Time.deltaTime);
				velX += (Vector2.right * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);
			} else 
			{
//				movePos.x += (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
				velX += (Vector2.right * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);
			}


		}else if (Input.GetKeyUp (right)
		          || inputDevice.Direction.X == 0
		          ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (up)
		    || inputDevice.Direction.Y > 0
		    ) {
			if (!magnet) 
			{
//				rb.AddForce (Vector3.forward * moveSpeed * slow * Time.deltaTime);
				velZ += (Vector2.up * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);;
			} else 
			{
				velZ += (Vector2.up * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);
//				movePos.z += (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
			}
		}else if (Input.GetKeyUp (up)
		          || inputDevice.Direction.Y == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (down)
		    || inputDevice.Direction.Y < 0
		    ) {

			if (!magnet) 
			{
//				rb.AddForce (Vector3.back * moveSpeed * slow * Time.deltaTime);
				velZ += (Vector2.down * moveSpeed * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);
			} else 
			{
				velZ += (Vector2.down * moveSpeedWBoots * slow * Time.deltaTime*0.2f);
				MoveRB (velX, velZ);
//				movePos.z -= (moveSpeedWBoots * slow * Time.deltaTime);
//				Move ();
			}
		}else if (Input.GetKeyUp (down)
		          || inputDevice.Direction.Y == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

//		if (magnet && rb.velocity != Vector2.zero) {
//			rb.velocity = Vector2.zero+spaceshipRb.velocity;
//		}

		if (Input.GetKeyDown (magnetBoots) 
		    || inputDevice.RightBumper
		    )
		{
			Invoke ("Boots", magnetDelay);
		}

		if (Input.GetKey(KeyCode.Space)
		    || inputDevice.RightStickX != 0 || inputDevice.RightStickY != 0
		    ) {
			washing = true;
//			Debug.Log ("Rhor: " + Input.GetAxis(rHorAxis) + ", rVer: " + Input.GetAxis(rVerAxis));
		}else{
			washing = false;
		}

		if (washing 
//		    || Input.GetKey(KeyCode.L)
		    ){
			foreach (Transform go in tf.parent){
				go.GetComponent<FlipCharacter>().SetBack();
			}
			if(!magnet){
				washHand.GetComponent<PaintTest>().washHand = washHand.GetComponent<PaintTest>().washHandWithoutBoots;
			}
		}else {
			foreach (Transform go in tf.parent){
				go.GetComponent<FlipCharacter>().SetFront();
			}
		}

		if (Input.GetKey (detach)
		    || inputDevice.LeftBumper
		    ) {
			cord.gameObject.GetComponent<RopeStart> ().Detach (tf.position);
		}
//		line.SetPosition (0, tf.position);
//		line.SetPosition (1, cord.position);
		distToCord = Vector3.Distance (tf.position, cord.position);
		curShipVel = spaceshipRb.velocity;
		curVelocity = rb.velocity;
//		if (!moving && rb.velocity.x <= spaceshipRb.velocity.x){
//			rb.velocity = new Vector2 (spaceshipRb.velocity.x*Time.deltaTime, rb.velocity.y);
//		}
//
//		if (!moving && rb.velocity.y <= spaceshipRb.velocity.y){
//			rb.velocity = new Vector2 (rb.velocity.x, spaceshipRb.velocity.x*Time.deltaTime);
//		}

		//When controllers are connected
//		if (inputDevice.RightStickX > 0) {
//			ragdollHandVel.x = washHandSpeed;
//			ragdollHandRb.velocity = ragdollHandVel+rb.velocity;
//		}if (inputDevice.RightStickX < 0) {
//			ragdollHandVel.x = (-washHandSpeed);
//			ragdollHandRb.velocity = ragdollHandVel+rb.velocity;
//		}if (inputDevice.RightStickY > 0) {
//			ragdollHandVel.y = washHandSpeed;
//			ragdollHandRb.velocity = ragdollHandVel+rb.velocity;
//		}if (inputDevice.RightStickY < 0) {
//			ragdollHandVel.y = (-washHandSpeed);
//			ragdollHandRb.velocity = ragdollHandVel+rb.velocity;
//		}
//		if (inputDevice.RightStick == Vector2.zero){
//			ragdollHandVel = Vector2.zero;
//		}


//		if (ragdollHandVel != Vector2.zero && (velX != Vector2.zero || velZ != Vector2.zero)){
//			ragdollHandRb.velocity = ragdollHandRb.velocity+rb.velocity;
//		}
		//Debug.Log (Input.GetJoystickNames ());

		if ((spaceshipRb.velocity.x > 5 || spaceshipRb.velocity.x < 5) && spaceshipRb.velocity.x != 0 && !GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>().boostActive) {
			rb.velocity = new Vector2 ((spaceshipRb.velocity.x/2)+velX.x, rb.velocity.y);
		}

		if (magnet) {
			rb.velocity = spaceshipRb.velocity+new Vector2 (velX.x, velZ.y);
		}

		if (GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>().boostActive){
//			BoostAndMagnet ();			
		}

		if (!GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>().boostActive && !rb.simulated){
//			for (int i = 0; i < childrenNotThis.Count; i++) {
//				childrenNotThis[i].GetComponent<Rigidbody2D>().simulated = true;
//			}
//			rb.simulated = true;;
		}
		if (stoppingBoost && (rb.velocity.y-spaceshipRb.velocity.y) > 1) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y - Time.deltaTime*20);

		}
		if (stoppingBoost && (rb.velocity.y-spaceshipRb.velocity.y) < 1) {
			stoppingBoost = false;
		}
	}

	void BoostAndMagnet () {
		if (magnet) {
			if (rb.simulated) {
				for (int i = 0; i < childrenNotThis.Count; i++) {
					childrenNotThis[i].GetComponent<Rigidbody2D>().simulated = false;;
				}
				rb.simulated = false;
//				posOnShip = playerHolder.transform.localPosition;
			}
//			if (playerHolder.transform.localPosition != posOnShip){
//				playerHolder.transform.localPosition = posOnShip;
//			}
		}
	}

	void Move () {
		tf.position = Vector3.Lerp (tf.position, movePos, 1);
	}

	public void MoveRB (Vector2 x, Vector2 y) {
		rb.velocity = spaceshipRb.velocity + new Vector2 (x.x, y.y);
		moving = true;
	}

	void Boots () {
		if (magnet) 
		{
			foreach (Rigidbody2D rbc in childRbs){
				rbc.mass = 1;	
			}
			playerHolder.transform.parent = null;
//			slow = 1;
//			rb.velocity = Vector2.zero+spaceshipRb.velocity;
//			rb.angularVelocity = 0+spaceshipRb.angularVelocity;
//			rb.drag = magnetDrag;
			magnet = false;
			magnetDistJ.enabled = false;
			Flipback();
			washHand.GetComponent<PaintTest>().washHand = washHand.GetComponent<PaintTest>().washHandWithoutBoots;
//			velZ = Vector2.zero;
//			velX = Vector2.zero;
		} else 
		{
			foreach (Rigidbody2D rbc in childRbs){
				rbc.mass = magnetMass;	
			}
			playerHolder.transform.parent = spaceshipRb.gameObject.transform;
			Flip();
//			slow = bracingSlow;
//			rb.detectCollisions = true;
			magnet = true;
			washHand.GetComponent<PaintTest>().washHand = washHand.GetComponent<PaintTest>().washHandWithBoots;
			magnetDistJ.enabled = true;
//			velZ = Vector2.zero;
//			velX = Vector2.zero;
//			rb.drag = normalDrag;
		}
		movePos = tf.position;
	}

	void Flip () {
//			topDownChar.SetActive (true);
		for (int j = 0; j < topDownChar.transform.childCount; j++) {
			topDownChar.transform.GetChild(j).GetComponent<SpriteRenderer>().enabled = true;
		}
		thisSprite.enabled = false;
		for (int i = 0; i < childrenNotThis.Count; i++) {
			childrenNotThis[i].GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void Flipback () {
		thisSprite.enabled = true;
		for (int i = 0; i < childrenNotThis.Count; i++) {
			childrenNotThis[i].GetComponent<SpriteRenderer>().enabled = true;
		}
		for (int j = 0; j < topDownChar.transform.childCount; j++) {
			topDownChar.transform.GetChild(j).GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
