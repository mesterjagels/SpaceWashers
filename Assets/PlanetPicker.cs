using UnityEngine;
using System.Collections;

public class PlanetPicker : MonoBehaviour {

	public Sprite[] planetSkin;

	void Awake () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = planetSkin[Random.Range [0, planetSkin.Length - 1]];
	}
}
