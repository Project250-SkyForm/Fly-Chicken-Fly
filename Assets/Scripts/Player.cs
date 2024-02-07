using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AnimatedEntity
{   

    private const float Gravity = -9.81f;
    private const float camera_const = 5;
    private const float standing_const = 1.0f;
    private float Speed = 0;
    private bool falling = false;
    private bool able_move_left = true;
    private bool able_move_right = true;
    private float horizontal_speed=10.0f; //horizontal_speed_left = 10.0f, horizontal_speed_right = 10.0f;
    private AudioSource audioSource;
    private Platform current_platform;
    private SpriteRenderer spriteRenderer;

    public List<Sprite> interruptAnimationCycle;
    public BackgroundScroll background;
    public BackgroundScroll leftRect;
    public BackgroundScroll rightRect;

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
            if (current_platform != null){
                    //current_platform.refresh();
            }
            leftRect.Continue();
            rightRect.Continue();
            Speed = 10;
            falling = true;
            current_platform = null;     // to avoid null bug
        }

        // Going left
        if(Input.GetKey(KeyCode.A) && able_move_left){
            able_move_right = true;     // if we move left, which means we have space to move right now
            transform.position+= Vector3.left*Time.deltaTime*horizontal_speed;
            if (current_platform!=null){
                if (transform.position.x < current_platform.getLeft()){ //detect whether player go beyond the left edge of the platform
                    falling = true;
                }
            }
        }

        // Going down
        if(Input.GetKey(KeyCode.D) && able_move_right){
            able_move_left = true;
            transform.position+= Vector3.right*Time.deltaTime*horizontal_speed;
            if (current_platform!=null){
                if (transform.position.x > current_platform.getRight()){ //detect whether player go beyond the right edge of the platform
                    falling = true;
                }
            }
        }


        if (falling){   // if the player is not standing on the playform, it should fall down
            Speed += Gravity*Time.deltaTime;
            transform.position+=Vector3.up*Time.deltaTime*Speed;
        }

        Vector3 cameraPosition = Camera.main.transform.position;    //determine whether the camera is too high to see the player
        if (cameraPosition.y > transform.position.y + camera_const){    //stop the motions of both the background and camera and left & right rect
            Interrupt (interruptAnimationCycle);
            EventController.Instance.AddLump();
            Vector3 newPos = Camera.main.transform.position;
            newPos.y = transform.position.y + camera_const;
            Camera.main.transform.position = newPos;
            background.Stop();
            leftRect.Stop();
            rightRect.Stop();
        }
        
    }

    void OnTriggerEnter(Collider other){
        Platform platform = other.gameObject.GetComponent<Platform>();

        if(platform!=null){
            if (platform.getType()=="left_rect"){
                able_move_left = false;
                Debug.Log("Collide left rect");
            }
            else if (platform.getType()=="right_rect"){
                able_move_right = false;
                Debug.Log("Collide right rect");
            }
            else{
                if (audioSource != null){
                    audioSource.Play();
                }
                if (falling && transform.position.y>=platform.transform.position.y){    // depature on the platform
                    Speed = 0;
                    falling = false;
                    current_platform = platform;    //record the information of the collider maybe
                    DataManager.Instance.UpdateHighestScore((int)transform.position.y);
                    //Debug.Log("Depature on playform");
                }
                else if (Speed>=0){         //going up and hit the platform above
                    Speed = 0;   
                    current_platform = platform;
                    falling = true;
                    Interrupt (interruptAnimationCycle);
                    EventController.Instance.AddLump();
                    //Debug.Log("hit the platform");
                }

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
            float leftEdge =  - spriteRenderer.bounds.size.x / 2f;
            float rightEdge =  spriteRenderer.bounds.size.x / 2f;
            float topEdge =  spriteRenderer.bounds.size.y / 2f;
            float downEdge = - spriteRenderer.bounds.size.y / 2f;
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
                //Debug.Log("Down"+ downEdge);
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
