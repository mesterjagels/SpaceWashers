using UnityEngine;
using System.Collections;

public class DirtController : MonoBehaviour {


	public Texture2D original;
	[HideInInspector]
	public Texture2D duplicate;
	Renderer rend;
	Color32 invisible = new Color32 (0,0,0,0);
	Color32[] colors;
	Color32 [] alreadyWashed;
	public int pixelsWashedCount;
//	float pixelsPrcntWashed;
	public int washCount;
	public int totalPixels;

//	public int brushSize;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		duplicate = Instantiate (original) as Texture2D;
		alreadyWashed = duplicate.GetPixels32 ();
		totalPixels = alreadyWashed.Length;
		CalculateStuff ();
	}

	void OnEnable () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<AmountWashed>().UpdateDirts();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Clean (int brushSize, Vector2 pixelUV) {
		rend.material.mainTexture = duplicate;
//		pixelUV = hit.textureCoord;
		pixelUV.x *= duplicate.width;
		pixelUV.y *= duplicate.height;
		pixelUV.x -= brushSize * 0.5f;
		pixelUV.y -= brushSize * 0.5f;

		colors = new Color32[brushSize * brushSize];
		for (int i = 0; i < brushSize * brushSize; i++) {
			if (colors [i].r != invisible.r && colors [i].g != invisible.g && colors [i].g != invisible.g && colors [i].b != invisible.b && colors [i].a != invisible.a) 
			{
				colors [i] = invisible;
			}
		}

		duplicate.SetPixels32 (Mathf.FloorToInt (pixelUV.x), Mathf.FloorToInt (pixelUV.y), brushSize, brushSize, colors);
//		Debug.Log (Mathf.FloorToInt (pixelUV.x) + " " + Mathf.FloorToInt (pixelUV.y) + " " + brushSize + " " + brushSize + " " + colors);
		duplicate.Apply ();
		CalculateStuff ();

	}

	public void CalculateStuff () 
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
		if (washCount > pixelsWashedCount) 
		{
			pixelsWashedCount = washCount;
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<AmountWashed> ().GetPixels ();
//			pixelsPrcntWashed = ((float)pixelsWashedCount) / ((float)totalPixels);
		}



	}
}
