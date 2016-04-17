using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    Vector2 offset;
    GameObject spaceship;
    public float parralax = 2f;
    int uvMoveSpeed = 20;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        mat = mr.materials[0];
        offset = mat.mainTextureOffset;
        spaceship = GameObject.FindGameObjectWithTag("Spaceship");
    }

    // Update is called once per frame
    void Update () {
        offset = spaceship.transform.position/ uvMoveSpeed / parralax;
        mat.mainTextureOffset = offset;
	}
}
