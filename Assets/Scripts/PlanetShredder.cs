using UnityEngine;
using System.Collections;

public class PlanetShredder : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Planet"))
        {
            Debug.Log("Planet destroyed");
            Destroy(col.gameObject);
        } else if (col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Spacetrash shredded");
            Destroy(col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Spacetrash shredded");
            Destroy(other.gameObject);
        }
    }
}
