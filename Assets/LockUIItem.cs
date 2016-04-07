using UnityEngine;
using System.Collections;

public class LockUIItem : MonoBehaviour
{
    private Vector3 position;
    private Vector3 scale;
    // Use this for initialization
    void Awake()
    {
        position = this.gameObject.GetComponent<RectTransform>().position;
        scale = this.gameObject.GetComponent<RectTransform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (position != this.gameObject.GetComponent<RectTransform>().position)
        {
            this.gameObject.GetComponent<RectTransform>().position = position;
            this.gameObject.GetComponent<RectTransform>().localScale = scale;
        }
    }
}