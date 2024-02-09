using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float frequency = 2f; // Time between platform spawns
    public int totalPlatforms = 10; // Maximum number of platforms to spawn
    public float verticalOffset = 2f; // Vertical distance between platforms
    public float horizontalMinOffset = -5f; // Minimum horizontal offset
    public float horizontalMaxOffset = 5f; // Maximum horizontal offset
    public float minDistanceBetweenPlatforms = 1f; // Minimum distance between platforms

    private void Start()
    {
        StartCoroutine(SpawnPlatform());
    }

    IEnumerator SpawnPlatform()
    {
        for (int i = 0; i < totalPlatforms; i++)
        {
            // Calculate random horizontal offset within specified range
            float randomHorizontalOffset = Random.Range(horizontalMinOffset, horizontalMaxOffset);

            // Calculate the spawn position with both vertical and horizontal offsets
            Vector3 spawnPosition = new Vector3(transform.position.x + randomHorizontalOffset, transform.position.y + i * verticalOffset, transform.position.z);

            // Check if there is enough distance from existing platforms
            bool canSpawn = CheckPlatformSpacing(spawnPosition);

            // If there's not enough space, retry with a new position
            while (!canSpawn)
            {
                randomHorizontalOffset = Random.Range(horizontalMinOffset, horizontalMaxOffset);
                spawnPosition = new Vector3(transform.position.x + randomHorizontalOffset, transform.position.y + i * verticalOffset, transform.position.z);
                canSpawn = CheckPlatformSpacing(spawnPosition);
            }

            // Spawn platform at the calculated position
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Randomly flip the sprite on the x-axis
            if (Random.value > 0.5f)
            {
                newPlatform.transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            // Wait for the specified frequency
            yield return new WaitForSeconds(frequency);
        }
    }

    bool CheckPlatformSpacing(Vector3 spawnPosition)
    {
        // Check if there's enough distance from existing platforms
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, minDistanceBetweenPlatforms);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider belongs to a platform
            if (collider.gameObject.CompareTag("Ground"))
            {
                return false; // Not enough space, retry with a new position
            }
        }

        return true; // There is enough space
    }
}

