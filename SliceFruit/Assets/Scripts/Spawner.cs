using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float velocityIntensity;

    public GameObject[] prefabs;
    public Transform[] spawnPoints;
    public float spawnTimer = 2;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTimer)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

            GameObject spawnedPrefab = Instantiate(randomPrefab, randomPoint.position, randomPoint.rotation);

            timer -= spawnTimer;

            Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();
            rb.velocity = randomPoint.forward * velocityIntensity;
        }
    }
}
