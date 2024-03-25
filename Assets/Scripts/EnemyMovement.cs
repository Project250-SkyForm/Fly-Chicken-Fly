using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private Rigidbody2D body;
    public float catchUpSpeed = 3f; // Speed at which the enemy catches up to the player
    public float maxDistance = 10f; // Maximum distance below the player
    public float catchUpDelay = 5f; // Time before enemy starts catching up when player is idle on a platform
    public float detectionRadius = 10f;
    private float lastPlayerYPosition;
    private float lastPlayerXPosition;
    private float catchUpTimer;
    public List<Sprite> animationFrames;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lastPlayerYPosition = player.position.y;
        lastPlayerXPosition = player.position.x;
        catchUpTimer = catchUpDelay;
    }

void Update()
{
    float playerYPos = player.position.y;
    float playerXPos = player.position.x;
    float enemyYPos = transform.position.y;
    float enemyXPos = transform.position.x;

    float distanceToPlayer = Vector2.Distance(player.position, transform.position);
    
    // Adjust the enemy's vertical position to always be below the player but within the maxDistance
    if (playerYPos - enemyYPos > maxDistance)
    {
        transform.position = new Vector2(transform.position.x, playerYPos - maxDistance);
    }

    // Calculate the horizontal movement towards the player's X position
    float moveTowardsPlayerX = playerXPos - enemyXPos;

    if (playerYPos < enemyYPos) {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        body.velocity = directionToPlayer * catchUpSpeed * 2f;
    }

    // If the player is on the same level, below the enemy, or within the detection radius, target the player directly
    if (distanceToPlayer <= detectionRadius)
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        body.velocity = directionToPlayer * catchUpSpeed * 0.5f;
    }
    else
    {
        // If the player hasn't moved up, start the catch-up timer
        if (playerYPos <= lastPlayerYPosition)
        {
            catchUpTimer -= Time.deltaTime;
        }

        if (catchUpTimer <= 0 || playerYPos > lastPlayerYPosition)
        {
            // Reset catch-up timer if the player has moved up
            catchUpTimer = catchUpDelay;

            // Move towards the player's X position at a proportion of the catchUpSpeed
            float xVelocity = moveTowardsPlayerX > 0 ? catchUpSpeed : -catchUpSpeed;
            
            // Start moving the enemy up at catchUpSpeed, adjusting for exciting chase if close to the screen edge
            float yVelocity = catchUpSpeed;
            if (enemyYPos > Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5)
            {
                yVelocity *= 1.5f; // Increase speed for excitement
            }

            body.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    // Update lastPlayerYPosition for the next frame
    lastPlayerYPosition = playerYPos;

    AnimateEnemy(10f);
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("End Game");
            if(DataManager.Instance.currentGameMode == "death"){
                EventController.Instance.EndGame();
                Destroy(gameObject);
            }
            
        }
    }

    private void AnimateEnemy(float currentFrameRate)
    {
        if (animationFrames.Count > 0)
        {
            int index = (int)(Time.time * currentFrameRate) % animationFrames.Count;
            GetComponent<SpriteRenderer>().sprite = animationFrames[index];
        }
    }
}
