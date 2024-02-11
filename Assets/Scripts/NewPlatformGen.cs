using UnityEngine;
using System.Collections;

public class NewPlatformGen : MonoBehaviour
{
    public GameObject regularPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public int totalPlatforms = 10;
    public float minX = -5f;
    public float maxX = 5f;
    public float verticalDistanceBetweenPlatforms = 2f;
    public float timeBetweenPlatforms = 2f;
    public float startingYPosition = 0f;
    public int movingPlatformsFrequency = 3; // Number of regular platforms between moving platforms
    public float additionalVerticalDistanceForMoving = 2f; // Additional vertical distance for moving platforms

    void Start()
    {
        StartCoroutine(GeneratePlatformsRepeatedly());
    }

    IEnumerator GeneratePlatformsRepeatedly()
    {
        float currentY = startingYPosition;
        int regularPlatformCounter = 0;

        for (int i = 0; i < totalPlatforms; i++)
        {
            if (regularPlatformCounter == movingPlatformsFrequency)
            {
                GenerateMovingPlatform(currentY + additionalVerticalDistanceForMoving);
                regularPlatformCounter = 0; // Reset the counter after generating a moving platform
            }
            else
            {
                GenerateRegularPlatform(currentY);
                regularPlatformCounter++;
            }

            currentY += verticalDistanceBetweenPlatforms;
            yield return new WaitForSeconds(timeBetweenPlatforms);
        }
    }

    void GenerateRegularPlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        Instantiate(regularPlatformPrefab, spawnPosition, Quaternion.identity);
    }

    void GenerateMovingPlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        Instantiate(movingPlatformPrefab, spawnPosition, Quaternion.identity);
    }
}


