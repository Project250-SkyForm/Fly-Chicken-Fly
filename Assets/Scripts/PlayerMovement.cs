using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public SpriteRenderer spriteRenderer;

    public float speed;
    public float jump;
    public bool isJumping;
    public bool ableToMove;
    public bool withPiggyback;
    public float piggybackJump;
    public float jumpConstant;

    // Animation variables
    public float walkingFrameRate = 0.1f; // Adjust this value to control walking animation speed
    public float jumpingFrameRate = 0.1f; // Adjust this value to control jumping animation speed
    public float idleFrameRate = 0.1f; // Adjust this value to control idle animation speed
    public List<Sprite> animationFrames; // List to hold the walking animation sprites
    public List<Sprite> jumpAnimationFrames; // Holds jumping sprites
    public List<Sprite> idleAnimationFrames; // Holds idle sprites
    
    private int currentFrame = 0;
    private float frameTimer = 0f;
    private bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (withPiggyback)
        {
            jump = piggybackJump;
        }
        else
        {
            jump = jumpConstant;
        }
        if (ableToMove)
        {
            float move = 0f;

            // Check for W, A, D keys
            if (Input.GetKey(KeyCode.W))
            {
                // Handle jump logic
                if (!isJumping)
                {
                    body.velocity = new Vector2(body.velocity.x, jump);
                    isJumping = true;

                    AudioController.Instance.PlayChickenJump();

                    if (jumpAnimationFrames.Count > 0 && spriteRenderer != null)
                    {
                        spriteRenderer.sprite = jumpAnimationFrames[0];
                    }

                    AnimatePlayer(jumpingFrameRate);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                // Handle left movement
                move = -1f;
                isIdle = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // Handle right movement
                move = 1f;
                isIdle = false;
            }

            else
            {
                isIdle = true;
            }

            if (isIdle)
            {
                AnimatePlayer(idleFrameRate); // Use idle frame rate when player is idle
            }

           
            Vector2 newVelocity = new Vector2(move * speed, body.velocity.y);

            // Adjust player movement based on the current boundaries
            Vector2 newPosition = transform.position + new Vector3(newVelocity.x, 0, 0) * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, GetMinXBoundary(), GetMaxXBoundary());

            // Apply the adjusted position directly or adjust velocity accordingly
            if (Time.deltaTime > 0)
            {
                body.velocity = new Vector2((newPosition.x - transform.position.x) / Time.deltaTime, body.velocity.y);
            }


            if (move < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (move > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            AnimatePlayer(walkingFrameRate);
        }
    }

    void AnimatePlayer(float currentFrameRate)
    {
        // Check if any movement key is pressed and the player is not idle
        bool isMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !isIdle;

        // Check if there are frames in the animation list and the spriteRenderer is not null
        if (animationFrames.Count > 0 && spriteRenderer != null)
        {
            // If the player is not jumping and not moving, play the idle animation
            if (!isJumping && !isMoving && idleAnimationFrames.Count > 0)
            {
                frameTimer += Time.deltaTime;

                if (frameTimer >= idleFrameRate)
                {
                    frameTimer = 0f;
                    currentFrame = (currentFrame + 1) % idleAnimationFrames.Count;
                    spriteRenderer.sprite = idleAnimationFrames[currentFrame];
                }
            }
            // If the player is jumping, cycle through the jump animation frames
            else if (isJumping && jumpAnimationFrames.Count > 0)
            {
                frameTimer += Time.deltaTime;

                if (frameTimer >= jumpingFrameRate)
                {
                    frameTimer = 0f;
                    currentFrame = (currentFrame + 1) % jumpAnimationFrames.Count;
                    spriteRenderer.sprite = jumpAnimationFrames[currentFrame];
                }
            }
            // If the player is moving, play the walking animation
            else if (isMoving && animationFrames.Count > 0)
            {
                frameTimer += Time.deltaTime;

                if (frameTimer >= currentFrameRate)
                {
                    frameTimer = 0f;
                    currentFrame = (currentFrame + 1) % animationFrames.Count;
                    spriteRenderer.sprite = animationFrames[currentFrame];
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform"))
        {
            isJumping = false;
        }
    }

    float GetMinXBoundary()
    {
        return EventController.Instance.GetMinXBoundary();
    }

    float GetMaxXBoundary()
    {
        return EventController.Instance.GetMaxXBoundary();
    }
}









