using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private Rigidbody2D body;
    public float catchUpSpeed = 5f; // Speed at which the enemy catches up to the player
    public float maxDistance = 20f; // Maximum distance below the player
    public float catchUpDelay = 1f; // Time before enemy starts catching up when player is idle on a platform
    public float detectionRadius = 10f;
    private float lastPlayerYPosition;
    private float catchUpTimer;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lastPlayerYPosition = player.position.y;
        catchUpTimer = catchUpDelay;
    }

    void Update()
    {
        float playerYPos = player.position.y;
        float enemyYPos = transform.position.y;

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        // Adjust the enemy's position to always be below the player but within the maxDistance
        if (playerYPos - enemyYPos > maxDistance)
        {
            enemyYPos = playerYPos - maxDistance;
            transform.position = new Vector2(transform.position.x, enemyYPos);
        }

        // If the player is on the same level or below the enemy, target the player directly
        if (distanceToPlayer <= detectionRadius || playerYPos <= enemyYPos)
        {
            // Move directly towards the player
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            body.velocity = directionToPlayer * catchUpSpeed;
        }
        else
        {
            // Check if the player's Y position has increased
            if (playerYPos > lastPlayerYPosition)
            {
                // Reset the timer if the player moves up
                catchUpTimer = catchUpDelay;
            }
            else
            {
                // If the player hasn't moved up, start the catch-up timer
                catchUpTimer -= Time.deltaTime;
                if (catchUpTimer <= 0)
                {
                    // Start moving the enemy up at catchUpSpeed
                    body.velocity = new Vector2(body.velocity.x, catchUpSpeed);

                    // If the enemy is about to enter the screen, adjust the speed for an exciting chase
                    if (enemyYPos > Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5)
                    {
                        body.velocity = new Vector2(body.velocity.x, catchUpSpeed * 1.5f); // Increase speed for excitement
                    }
                }
            }

            // Continuously move up slowly if the player is above the enemy
            if (body.velocity.y < catchUpSpeed * 0.5f)
            {
                transform.position += Vector3.up * Time.deltaTime * (catchUpSpeed * 0.5f);
            }
        }

        // Update lastPlayerYPosition for the next frame
        lastPlayerYPosition = playerYPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("End Game");
            SceneController.Instance.ChangeScene("GameOverScene");
            Destroy(gameObject);
        }
    }
}
