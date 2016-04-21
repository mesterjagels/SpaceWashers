using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public float curPrcnt;
	public float minPrcntCleaned;
	public List <Vector3> randomPos;

    //  Score Variables
    public int numberOfPlayers;
    public int[] playerScore; 
    public int totalScore;

//	public int brushSize;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		duplicate = Instantiate (original) as Texture2D;
		alreadyWashed = duplicate.GetPixels32 ();
		totalPixels = alreadyWashed.Length;

        //  Score related
        playerScore = new int[numberOfPlayers];

		CalculateStuff (0);
	}

	void OnEnable () {
		GameObject.FindGameObjectWithTag("GameController").GetComponent<AmountWashed>().UpdateDirts();
	}

	
	public void Clean (int brushSize, Vector2 pixelUV, int playerNumber) {

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
		CalculateStuff (playerNumber);

       
	}

	public void CalculateStuff (int playerNumber) 
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

		curPrcnt = ((float)pixelsWashedCount / (float)totalPixels)*100;

		if (curPrcnt > minPrcntCleaned) {
            
            Cleaned(playerNumber);
		}

	}

	void Cleaned (int playerNumber) {
        //Increase player score, spawn scrolling score at the position of the destroyed object
        //Write what to to with the int PlayerNumber, by sending the score to socreboard
        switch (playerNumber)
        {
            case 0:
                GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player0Score += 100;
                Debug.Log("Player 0 has " + GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player0Score + " points");
                break;
            case 1:
                GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player1Score += 100;
                Debug.Log(GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player1Score);

                break;
            case 2:
                GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player2Score += 100;
                Debug.Log(GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player2Score);

                break;
            case 3:
                GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player3Score += 100;
                Debug.Log(GameObject.FindGameObjectWithTag("Gamecontroller").GetComponent<ScoreCalculator>().player3Score);
                break;
        }

        //Something happens when the dirt is cleaned
        //TODO: put scoring for the different players here 
        Destroy (this.gameObject, 0.5f);
        ;
      
    }
}
