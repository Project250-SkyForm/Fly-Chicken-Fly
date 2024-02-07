using System.Collections;
using UnityEngine;

public class PlatformGenerator: MonoBehaviour
{
    public Transform platforms;
    public GameObject platform;
    public GameObject movingPlatform; // Prefab for the moving platform

    private Vector3 spawnPos;
    public float levelWidth = 2.2f;
    public float verticalSpacing = 1.0f;
    public float horizontalSpacing = 2.0f;
    public float timeBetweenPlatforms = 1.0f;
    public int totalPlatforms = 10;  // Total number of platforms
    public int numRegularPlatforms = 6;  // Number of regular platforms
    public int numMovingPlatforms = 1;  // Number of moving platforms after numRegularPlatforms

    private void Start()
    {
        StartCoroutine(GeneratePlatforms());
    }

    IEnumerator GeneratePlatforms()
    {
        float lastPlatformHeight = 0.0f;
        Camera mainCamera = Camera.main;

        for (int i = 0; i < totalPlatforms; i++)
        {
            bool isMovingPlatform = i >= numRegularPlatforms && i < numRegularPlatforms + numMovingPlatforms; // Check if it's time for a moving platform

            float newX;
            float newY;

            do
            {
                newX = Random.Range(-levelWidth, levelWidth);
                newY = lastPlatformHeight + verticalSpacing;

            } while (IsOverlapping(new Vector2(newX, newY)));

            spawnPos = new Vector3(newX, newY, 0f);
            float minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float clampedX = Mathf.Clamp(spawnPos.x, minX + levelWidth, maxX - levelWidth);
            spawnPos.x = clampedX;

            Quaternion rotation = Quaternion.identity;
            if (Random.Range(0f, 1f) > 0.5f)
            {
                rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            GameObject newPlatform = Instantiate(isMovingPlatform ? movingPlatform : platform, spawnPos, rotation);

            if (isMovingPlatform)
            {
                newPlatform.GetComponent<PlatformMovement>().speed = Random.Range(1f, 3f);
            }

            lastPlatformHeight = newY;

            yield return new WaitForSeconds(timeBetweenPlatforms);
        }
    }

    // this apparently doesnt work right now
    bool IsOverlapping(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, horizontalSpacing);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                return true; // Overlapping with an existing platform
            }
        }

        return false; // Not overlapping
    }
}

