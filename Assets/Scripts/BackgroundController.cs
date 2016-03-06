using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public GameObject [] bgs;
	public Vector3 [] pos;
	public float bgSpeed;
	int myLength;
	// Use this for initialization
	void Start () {
		bgs = GameObject.FindGameObjectsWithTag ("Background");
		myLength = bgs.Length;
		pos = new Vector3[myLength];
		for (int i = 0; i < bgs.Length; i++) {
			pos [i] = bgs [i].transform.localPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bgs.Length; i++) {
			if (!bgs [i].GetComponent<BGInstance> ().resetting) {
				bgs [i].transform.localPosition -= (Vector3.forward * Time.deltaTime * bgSpeed);
			}
//			if (bgs[i].transform.localPosition.z < (bgs[i].GetComponent<BGInstance>().startPos.z - ((i+1)*50))) {
//				bgs[i].GetComponent<BGInstance>().resetting = true;
//				bgs[i].GetComponent<BGInstance>().ResetPos ();
//
//			}
		}
	}
}
