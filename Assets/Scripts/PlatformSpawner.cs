using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public float spawnRate;
    private float timer = 0;
    public float height;
    public float xRange; // Maximum range for random x position

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPlatform();
            timer = 0;
        }
    }

    void spawnPlatform()
    {
        float lowest = transform.position.y - height;
        float highest = transform.position.y + height;
        float randomX = Random.Range(-xRange, xRange); // Random x position within the specified range

        Instantiate(platform, new Vector3(randomX, Random.Range(lowest, highest), 0), transform.rotation);
    }
}

