using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
    public float jump;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        float move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(move * speed, body.velocity.y);

        if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (move > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Jump control
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            isJumping = true;
            EventController.Instance.StartCameraMoving();
            EventController.Instance.StartBackgroundMoving();
            AudioController.Instance.PlayChickenJump();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform"))
        {
            DataManager.Instance.UpdateHighestScore((int)transform.position.y);
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
        }
    }
}

