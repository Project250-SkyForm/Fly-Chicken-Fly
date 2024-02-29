using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPlatformGen : MonoBehaviour
{
    public GameObject regularPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public List<GameObject> spikePlatformPrefab;
    public List<GameObject> rottenEggPlatformPrefab;
    public int platformsOnScreenLimit = 5; // Adjust this to control the number of platforms on the screen
    public float minX = -7f;
    public float maxX = 6f;
    public float verticalDistanceBetweenPlatforms = 4.5f;
    public float timeBetweenPlatforms = 2f;
    public float startingYPosition = 5.77f;
    public int movingPlatformsFrequency = 3;
    public int spikePlatformFrequency = 5;
    public int rottenEggPlatformFrequency = 8;
    public float movingPlatformScale = 6.8f;
    public float spikePlatformScale = 6.8f;
    public float rottenEggPlatformScale = 6.8f;

    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GeneratePlatformsRepeatedly());
    }

    IEnumerator GeneratePlatformsRepeatedly()
    {
        float currentY = startingYPosition;
        int regularPlatformCounter = 0;

        while (true)
        {
            if (regularPlatformCounter == movingPlatformsFrequency)
            {
                GenerateMovingPlatform(currentY + movingPlatformScale);
                regularPlatformCounter++;
            }
            else if (regularPlatformCounter == spikePlatformFrequency)
            {
                GenerateSpikePlatform(currentY + spikePlatformScale);
                regularPlatformCounter++;
            }
            else if (regularPlatformCounter == rottenEggPlatformFrequency)
            {
                GenerateRottenEggPlatform(currentY + rottenEggPlatformScale);
                regularPlatformCounter = 0;
            }
            else
            {
                GenerateRegularPlatform(currentY);
                regularPlatformCounter++;
            }

            currentY += verticalDistanceBetweenPlatforms;

            // Check if the number of platforms exceeds the limit
            if (platforms.Count > platformsOnScreenLimit)
            {
                DestroyOldestPlatform();
            }

            yield return new WaitForSeconds(timeBetweenPlatforms);
        }
    }

    void GenerateRegularPlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        GameObject platform = Instantiate(regularPlatformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platform);
    }

    void GenerateMovingPlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        GameObject platform = Instantiate(movingPlatformPrefab, spawnPosition, Quaternion.identity);
        platforms.Add(platform);
    }

    void GenerateSpikePlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        GameObject spikePlatform = GetRandomPlatform(spikePlatformPrefab);
        GameObject platform = Instantiate(spikePlatform, spawnPosition, Quaternion.identity);
        platforms.Add(platform);
    }

    void GenerateRottenEggPlatform(float yPosition)
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, yPosition);
        GameObject rottenEggPlatform = GetRandomPlatform(rottenEggPlatformPrefab);
        GameObject platform = Instantiate(rottenEggPlatform, spawnPosition, Quaternion.identity);
        platforms.Add(platform);
    }

    private GameObject GetRandomPlatform(List<GameObject> platformList)
    {
        // Generate a random index within the range of the list count
        int randomIndex = UnityEngine.Random.Range(0, platformList.Count);

        // Return the platform GameObject at the randomly generated index
        return platformList[randomIndex];
    }

    void DestroyOldestPlatform()
    {
        if (platforms.Count > 0)
        {
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
        }
    }
}

