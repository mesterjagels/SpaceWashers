using UnityEngine;
using System.Collections;

public class DirtSpawner : MonoBehaviour
{

    public GameObject[] dirt;
    public float interval = 0;
    public int distanceFromCam;
    private GameObject cam;
    public int dirtSpawnerWidth = 50;


    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Debug.Log("Planet spawned");
            Instantiate(dirt[Random.RandomRange(0, dirt.Length)], new Vector3(Random.Range(cam.transform.position.x - dirtSpawnerWidth, cam.transform.position.x + dirtSpawnerWidth), 
                cam.transform.position.y - distanceFromCam, 10), 
                Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}
