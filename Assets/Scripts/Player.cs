using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AnimatedEntity
{   
    //public float Speed=5;
    private float Gravity = -9.81f;
    private float Speed = 0;
    private bool falling = true;
    private float horizontal_speed=10.0f; //horizontal_speed_left = 10.0f, horizontal_speed_right = 10.0f;
    private AudioSource audioSource;
    private Platform current_platform;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> interruptAnimationCycle;

    void Start()
    {
        AnimationSetup();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationUpdate(); 

        // Going up
        if (Input.GetKey(KeyCode.W) && falling==false){
            Speed = 8;
            falling = true;
            current_platform = null;     // to avoid null bug
        }

        // Going left
        if(Input.GetKey(KeyCode.A)){
            // horizontal_speed_right = 10.0f;
            transform.position+= Vector3.left*Time.deltaTime*horizontal_speed;
            if (current_platform!=null){
                if (transform.position.x < current_platform.getLeft()){ //detect whether player go beyond the left edge of the platform
                    falling = true;
                }
            }
        }

        // Going down
        if(Input.GetKey(KeyCode.D)){
            // horizontal_speed_left = 10.0f;
            transform.position+= Vector3.right*Time.deltaTime*horizontal_speed;
            if (current_platform!=null){
                if (transform.position.x > current_platform.getRight()){ //detect whether player go beyond the right edge of the platform
                    falling = true;
                }
            }
        }


        if (falling){
            Speed += Gravity*Time.deltaTime;
            transform.position+=Vector3.up*Time.deltaTime*Speed;
        }
        
    }

    void OnTriggerEnter(Collider other){
        Platform platform = other.gameObject.GetComponent<Platform>();

        if(platform!=null){
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Interrupt (interruptAnimationCycle);
            //  //we don't want the player move to left when encountering a platform on left 
            //         if (CalculateEdges("left") >= platform.getRight()){
            //             horizontal_speed_left = 0;
            //         }
                
            //   //we don't want the player move to right when encountering a platform on left
            //         if (CalculateEdges("right")>= platform.getLeft()){
            //             horizontal_speed_right = 0;
            //         }
            
            if (transform.position.y >= platform.getTop()){
                Speed = 0;
                falling = false;
                current_platform = platform;    //record the information of the collider maybe
            }
            else{
                Speed = 0;
            }
        }
    }

    public float CalculateEdges(string direction)     // some of the codes are from chatgpt,
    //return one of the edges of the sprite
    {
        // Ensure the object has a SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Get the left and right edges of the platform
            float leftEdge = transform.position.x - spriteRenderer.bounds.size.x / 2f;
            float rightEdge = transform.position.x + spriteRenderer.bounds.size.x / 2f;
            float topEdge = transform.position.y + spriteRenderer.bounds.size.y / 2f;
            float downEdge = transform.position.y - spriteRenderer.bounds.size.y / 2f;
            if (direction=="left"){
                return leftEdge;
            }
            else if (direction == "right"){
                return rightEdge;
            }
            else if (direction == "top"){
                return topEdge;
            }
            else if (direction == "down"){
                Debug.Log("Down"+ downEdge);
                return downEdge;
            }
            else{   // should not happen, just in case debug warning
                return 0;
            }
        }
        else{   // should not happen, just in case debug warning
                return 0;
            }
    }
}
