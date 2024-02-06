using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            if (transform.position.y< other.transform.position.y){  //hit the platform
                EventController.Instance.AddLump(); //for testing
            }
            
            AudioController.Instance.PlayChickenJump();
        }
    }


}


