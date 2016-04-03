using UnityEngine;
using System.Collections;

public class FlipCharacter : MonoBehaviour {

	public Sprite front;
	public int frontOrder;
	public Sprite back;
	public int backOrder;
	
	private int order;

	// Use this for initialization
	void Start () {

		order = gameObject.GetComponent<SpriteRenderer> ().sortingOrder;
		SetFront ();

	}

	public void SetFront(){
		front = gameObject.GetComponent<SpriteRenderer>().sprite = front;
		order = frontOrder;
	}

	public void SetBack(){
		back = gameObject.GetComponent<SpriteRenderer>().sprite = back;
		order = backOrder;

	}


	// Update is called once per frame
	void Update () {


	
		if (Input.GetKey ("f")) {

			SetBack ();

		}
		
		if (Input.GetKeyUp ("f")) {

			SetFront ();

		}

	}
}
