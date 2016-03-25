using UnityEngine;
using System.Collections;

public class EnableCleaner : MonoBehaviour {

	public KeyCode clean = KeyCode.F; 

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(clean)) {
			gameObject.GetComponent<PolygonCollider2D>().enabled = true;
		} else
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
