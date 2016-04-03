using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	public KeyCode left, right, up, down, magnetBoots, detach;
	public float moveSpeed, moveSpeedWBoots, bracingSlow;
	Transform tf;
	Vector3 movePos;
	bool magnet;
	float slow;

	public Rigidbody2D rb, spaceshipRb;
	public Vector3 curVelocity, curShipVel;
	public Vector2 velX, velZ;
	public Transform cord;
	LineRenderer line;
	public float distToCord;
	public bool washing;
	public bool moving;
	public float normalDrag, magnetDrag;
	int playerNr;
	string lHorAxis, rHorAxis, lVerAxis, rVerAxis;
	public GameObject topDownChar;
	GameObject playerHolder;
	SpriteRenderer thisSprite;
	List <GameObject> topDownSprites = new List<GameObject>();
	public List <GameObject> childrenNotThis = new List<GameObject>();
	float magnetMass = 10;
	public Rigidbody2D [] childRbs;
	public GameObject washHand;
	public GameObject ragdollHand;
	Rigidbody2D ragdollHandRb;
	Vector2 ragdollHandVel;
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
		if (tf.parent.parent.name.Contains("Player1")) {
			playerNr = 1;
		}else if (tf.parent.parent.name.Contains("Player2")) {
			playerNr = 2;
		}else if(tf.parent.parent.name.Contains("Player3")) {
			playerNr = 3;
		}

		if (playerNr == 1) {
			lHorAxis = "Horizontal";
			lVerAxis = "Vertical";
			rHorAxis = "HorizontalR1";
			rVerAxis = "VerticalR1";
		} else if (playerNr == 2) {
			lHorAxis = "HorizontalL1";
			lVerAxis = "VerticalL1";
			rHorAxis = "HorizontalR2";
			rVerAxis = "VerticalR2";
		} else if (playerNr == 3) {
			lHorAxis = "HorizontalL2";
			lVerAxis = "VerticalL2";
			rHorAxis = "HorizontalR3";
			rVerAxis = "VerticalR3";
		}
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
		    || Input.GetAxis(lHorAxis) < 0
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
		           || Input.GetAxis(lHorAxis) == 0
		           ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (right)
		    || Input.GetAxis(lHorAxis) > 0
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
		          || Input.GetAxis(lHorAxis) == 0
		          ) {
			velX = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (up)
		    || Input.GetAxis(lVerAxis) > 0
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
		          || Input.GetAxis(lVerAxis) == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

		if (Input.GetKey (down)
		    || Input.GetAxis(lVerAxis) < 0
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
		          || Input.GetAxis(lVerAxis) == 0
		          ) {
			velZ = Vector2.zero;
			if (moving)
				moving = false;
		}

//		if (magnet && rb.velocity != Vector2.zero) {
//			rb.velocity = Vector2.zero+spaceshipRb.velocity;
//		}

		if (Input.GetKeyDown (magnetBoots) || Input.GetButtonDown ("ControllerL1"))
		{
			Invoke ("Boots", 1.5f);
		}

		if (Input.GetAxis (rHorAxis) != 0 || Input.GetAxis(rVerAxis) != 0  || Input.GetKey(KeyCode.Space)) {
			washing = true;

		}else{
			washing = false;
		}

		if (washing || Input.GetKey(KeyCode.L)){
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

		if (Input.GetKey (detach) || Input.GetButton ("ControllerR1")) {
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
		if (magnet){
			rb.velocity = spaceshipRb.velocity + new Vector2 (velX.x, velZ.y);
		}

		if (Input.GetAxis (rHorAxis) > 0) {
			ragdollHandVel.x = 5;
			ragdollHandRb.velocity = ragdollHandVel;
		}if (Input.GetAxis (rHorAxis) < 0) {
			ragdollHandVel.x = (-5);
			ragdollHandRb.velocity = ragdollHandVel;
		}if (Input.GetAxis (rVerAxis) < 0) {
			ragdollHandVel.y = 5;
			ragdollHandRb.velocity = ragdollHandVel;
		}if (Input.GetAxis (rVerAxis) > 0) {
			ragdollHandVel.y = (-5);
			ragdollHandRb.velocity = ragdollHandVel;
		}
		//Debug.Log (Input.GetJoystickNames ());

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
//			slow = 1;
//			rb.velocity = Vector2.zero+spaceshipRb.velocity;
//			rb.angularVelocity = 0+spaceshipRb.angularVelocity;
//			rb.drag = magnetDrag;
			magnet = false;
			Flipback();
			washHand.GetComponent<PaintTest>().washHand = washHand.GetComponent<PaintTest>().washHandWithoutBoots;
//			velZ = Vector2.zero;
//			velX = Vector2.zero;
		} else 
		{
			foreach (Rigidbody2D rbc in childRbs){
				rbc.mass = magnetMass;	
			}
			Flip();
//			slow = bracingSlow;
//			rb.detectCollisions = true;
			magnet = true;
			washHand.GetComponent<PaintTest>().washHand = washHand.GetComponent<PaintTest>().washHandWithBoots;
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
