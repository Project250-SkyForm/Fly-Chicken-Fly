using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEggGenerator : MonoBehaviour
{
    public float generateRate; // Rate at which knives will be generated
    private float nextGenerateTime; // Time when the next knife should be generated
    public BackgroundScroll camera;
    public GameObject goldenEggPref;

    private float cameraWidth = 40.0f;  //should be update after the implementation of shrink and expand of camera

    void Start()
    {
        // Initialize nextGenerateTime to the current time plus the generateRate
        nextGenerateTime = Time.time + generateRate;
    }

    void Update()
    {
        // Check if it's time to generate a knife
        if (Time.time >= nextGenerateTime)
        {
            GenerateObjects();
            // Update nextGenerateTime for the next knife
            nextGenerateTime = Time.time + generateRate;
        }
    }

    void GenerateObjects()
    {

            // Generate Random Position
            Vector3 randomPosition = GetRandomPosition();

            // Check for Overlap
            if (!IsOverlapping(randomPosition))
            {
                // Instantiate Object
                GameObject goldenEgg = Instantiate(goldenEggPref, randomPosition, Quaternion.identity);
                GoldenEgg newEgg = goldenEgg.GetComponent<GoldenEgg>();
                newEgg.eternal = false;
            }

    }

    Vector3 GetRandomPosition()
    {
        Vector3 newPos = camera.transform.position;
        newPos.y += cameraWidth/2;
        float randomOffset = Random.Range(-10f, 10f); // Random offset
        newPos.x = camera.transform.position.x + randomOffset; // New position with random offset
        newPos.z = 0;
        return newPos;
    }

    bool IsOverlapping(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 10f); // Adjust radius according to your object size
        return colliders.Length > 0;
    }
}
