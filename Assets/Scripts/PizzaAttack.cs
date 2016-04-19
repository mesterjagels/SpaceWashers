using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PizzaAttack : MonoBehaviour {

    public GameObject pizzaObj, spaceship;
    public float pizzaSpeed;
    public int pizzaAmount;
    public int spawnDistance;
    List<GameObject> pizzaPool;
    Vector3 spaceshipPos;

	// Use this for initialization
	void Start () {
        spaceship = GameObject.FindGameObjectWithTag("Spaceship");
        
        CreatePizzaPool();
    }

    void Update()
    {
        spaceshipPos = spaceship.transform.position;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(new Vector3(spaceshipPos.x, spaceshipPos.y + (spaceship.GetComponent<Rigidbody2D>().velocity.y * 2), spaceshipPos.z), new Vector3(1, 1, 1));
    }

   
    void PizzaRain()
    {
        
    }

    void CreatePizzaPool()
    {
        pizzaPool = new List<GameObject>();
        for (int i = 0; i < pizzaAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pizzaObj);
            obj.SetActive(false);
            pizzaPool.Add(obj);
        }
    }
}
