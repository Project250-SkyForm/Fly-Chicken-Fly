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
    float move = Input.GetAxis("Horizontal");
    Vector2 newVelocity = new Vector2(move * speed, body.velocity.y);

    // Adjust player movement based on the current boundaries
    Vector2 newPosition = transform.position + new Vector3(newVelocity.x, 0, 0) * Time.deltaTime;
    newPosition.x = Mathf.Clamp(newPosition.x, GetMinXBoundary(), GetMaxXBoundary());

    // Apply the adjusted position directly or adjust velocity accordingly
    body.velocity = new Vector2((newPosition.x - transform.position.x) / Time.deltaTime, body.velocity.y);

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
            ChickenJump();
            
        }
    }

    public void ChickenJump(){
        body.velocity = new Vector2(body.velocity.x, jump);
        isJumping = true;
        EventController.Instance.StartCameraMoving();
        EventController.Instance.StartBackgroundMoving();
         AudioController.Instance.PlayChickenJump();
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform"))
        {
            DataManager.Instance.UpdateHighestScore((int)transform.position.y);
            isJumping = false;
        }
    }

    float GetMinXBoundary(){
        return EventController.Instance.GetMinXBoundary();
    }

    float GetMaxXBoundary(){
        return EventController.Instance.GetMaxXBoundary();
    }
}

