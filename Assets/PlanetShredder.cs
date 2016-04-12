using UnityEngine;
using System.Collections;

public class PlanetShredder : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Planet"))
        {
            Debug.Log("Planet destroyed");
            Destroy(col.gameObject);
        }
    }
}
