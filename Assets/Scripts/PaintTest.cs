using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PaintTest : MonoBehaviour {

	public Texture2D tex, texDirty;
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
	// Use this for initialization
	void Start () 
	{
		rend = spaceship.GetComponent<Renderer> ();
		duplicate = Instantiate (tex) as Texture2D;
		alreadyWashed = duplicate.GetPixels32 ();
		totalPixels = alreadyWashed.Length;
		barRect = barImg.rectTransform;
		barScale = barImg.rectTransform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Only if we hit something, do we continue
		RaycastHit hit;
		Vector3 ray = (-transform.up);
		if (Physics.Raycast (transform.position, ray, out hit) && Input.GetKey(wash))
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
