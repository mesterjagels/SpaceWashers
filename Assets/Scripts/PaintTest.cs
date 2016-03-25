using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PaintTest : MonoBehaviour {

	public Texture2D tex;
	public GameObject spaceship;
	Renderer rend;
	Texture2D duplicate, dirty;
	public Color32 invisible;
	public int brushSize = 50;
	Color32 [] alreadyWashed;
	Color32 [] colors;
	int pixelsWashedCount;
	float pixelsPrcntWashed;
	int washCount;
	int totalPixels;

	public RawImage barImg;
	public Text barText;
	RectTransform barRect;
	Vector3 barScale;

	public KeyCode wash;
	private Movement mvmt;

	void Start () 
	{
		rend = spaceship.GetComponent<Renderer> ();
		duplicate = Instantiate (tex) as Texture2D;
		alreadyWashed = duplicate.GetPixels32 ();
		totalPixels = alreadyWashed.Length;
		barRect = barImg.rectTransform;
		barScale = barImg.rectTransform.localScale;
		mvmt = GetComponent<Movement>();
		spaceship = GameObject.FindGameObjectWithTag ("Spaceship");
	}
	
	// Update is called once per frame
	void Update () 
	{
//		Vector2 ray = transform.forward;
//		RaycastHit2D hit = Physics2D.Raycast (transform.position, ray);
//		if (hit.collider != null) {
////			Debug.Log (gameObject.transform.name + " hit " + hit.transform.gameObject.name);
//			if (hit.transform.tag == "Dirt"){
//				Vector2 localPos = new Vector2 (hit.transform.gameObject.transform.position.x-hit.point.x, hit.transform.gameObject.transform.position.y-hit.point.y);
////				Vector2 pixelUV = hit.point
////				Debug.Log ("localpos: " + hit.transform.gameObject.transform.position);
//				Debug.Log ("hitpos: " + hit.point);
////				Vector2 pixelUV = hit.textureCoord;
////				hit.transform.GetComponent<DirtController>().Clean (brushSize, pixelUV);
////				Debug.Log (gameObject.transform.name + " hit " + hit.transform.gameObject.name);
//			}
//		}

		RaycastHit hit;
		Vector3 ray = transform.forward;
		if (Physics.Raycast (transform.position, ray, out hit) && mvmt.washing)
		{			
			if (hit.transform.tag == "Dirt") 
			{
//				rend.material.mainTexture = duplicate;
				Vector2 pixelUV = hit.textureCoord;
//				pixelUV.x *= duplicate.width;
//				pixelUV.y *= duplicate.height;
//				pixelUV.x -= brushSize * 0.5f;
//				pixelUV.y -= brushSize * 0.5f;
//
//				colors = new Color32[brushSize * brushSize];
//				for (int i = 0; i < brushSize * brushSize; i++) {
//					if (colors [i].r != invisible.r && colors [i].g != invisible.g && colors [i].g != invisible.g && colors [i].b != invisible.b && colors [i].a != invisible.a) {
//						colors [i] = invisible;
//
//					}
//				}
//				if (hit.textureCoord.x < 1 && hit.textureCoord.x > 0 && hit.textureCoord.y < 1 && hit.textureCoord.y > 0) {
//					duplicate.SetPixels32 (Mathf.FloorToInt (pixelUV.x), Mathf.FloorToInt (pixelUV.y), brushSize, brushSize, colors);
//					duplicate.Apply ();
//					CalculateStuff ();
//				}
				Debug.Log ("pixeluv: " + pixelUV);
				hit.transform.GetComponent<DirtController>().Clean (brushSize, pixelUV);
			}

		}


	}

	void CalculateStuff () 
	{
		alreadyWashed = duplicate.GetPixels32 ();
		washCount = 0;
		for (int i = 0; i < alreadyWashed.Length; i++) 
		{
			if (alreadyWashed [i].a == invisible.a) 
			{				
				washCount++;
			}
		}
		if (washCount > pixelsWashedCount) {
			pixelsWashedCount = washCount;
			pixelsPrcntWashed = ((float)pixelsWashedCount) / ((float)totalPixels);
			barScale.x = pixelsPrcntWashed;
			barRect.localScale = barScale;
			barImg.rectTransform.localScale = barRect.localScale;
//			pixelsPrcntWashed *= 100;
			barText.text = (pixelsPrcntWashed*100).ToString();
		}
//		Debug.Log (pixelsWashedCount / totalPixels);


	}


}
