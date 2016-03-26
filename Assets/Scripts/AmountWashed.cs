using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AmountWashed : MonoBehaviour {

	public GameObject[] dirt;
	public int dirtCount;
	public int totalPixels, pixelsWashedCount;
	public float totalPrcntWashed;
	public RawImage barImg;
	public Text barText, scoreText;
	public GameObject [] dirtText;

	RectTransform barRect;
	Vector3 barScale;
	// Use this for initialization
	void Start () {
		dirt = GameObject.FindGameObjectsWithTag ("Dirt");
		dirtCount = dirt.Length;
		barRect = barImg.rectTransform;
		barScale = barImg.rectTransform.localScale;
		Invoke ("GetPixels", 0.5f);
	}
	
	// Update is called once per frame
//	void Update () {
//		for (int i = 0; i < dirtCount; i++) {
//			
//		}
//	}

	public void GetPixels () {
		for (int i = 0; i < dirtCount; i++) 
		{
			totalPixels += dirt [i].GetComponent<DirtController> ().totalPixels;
			pixelsWashedCount += dirt [i].GetComponent<DirtController> ().pixelsWashedCount;
			if (dirt [i].GetComponent<DirtController> ().pixelsWashedCount == dirt [i].GetComponent<DirtController> ().totalPixels) 
			{
				Debug.Log (dirt [i].gameObject.name + " " + dirt [i].GetComponent<DirtController> ().pixelsWashedCount + "/" + dirt [i].GetComponent<DirtController> ().totalPixels);
				if (!dirtText [i].activeInHierarchy && dirt[i].GetComponent<DirtController>().totalPixels != 0) 
				{
					dirtText [i].SetActive (true);
				}
			}
		}

		totalPrcntWashed = (((float)pixelsWashedCount) / ((float)totalPixels));
		barScale.x = totalPrcntWashed;
		barRect.localScale = barScale;
//		barImg.rectTransform.localScale = barRect.localScale;
		barText.text = (totalPrcntWashed*100).ToString();
		scoreText.text = pixelsWashedCount.ToString();
	}

	public void UpdateDirts () {
		dirt = GameObject.FindGameObjectsWithTag ("Dirt");
	}
}
