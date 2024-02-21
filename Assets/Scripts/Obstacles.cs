using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public string type;
    public float fallingSpeed;
    public float horizontalSpeed;
    public int direction;
    public BackgroundScroll camera;
    public float disappear_distance;
    private SpriteRenderer movingPlatform;
    public float leftBound;
    public float rightBound;
    public float gapX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (transform.position.y<camera.transform.position.y-disappear_distance){
            Destroy(gameObject);
        }
        else if (movingPlatform!=null){
            Vector3 newPos = transform.position;
            // if (movingPlatform.transform.position.x < 0){
            //     newPos.x = movingPlatform.transform.position.x - gapX;
            // }
            newPos.x = movingPlatform.transform.position.x + gapX;
            transform.position = newPos;
        }
        else if (fallingSpeed != 0 || horizontalSpeed != 0){     //I have still objects for generation, so we don't want to chaneg any of them
            switch (type){
                case "polaCan":
                    fallingSpeed += 0.04f;
                    break;
                case "HandleUpKnife":
                    // // Check if the platform is beyond the right or left boundary
                    // if (transform.position.x >= rightBound || transform.position.x <= leftBound)
                    // {
                    //     // Change direction to move the platform back
                    //     direction *= -1;
                    // }   
                    break;
            }
        }
        
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime +
                            Vector3.right * direction * horizontalSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other){
        AudioSource audioSource = GetComponent<AudioSource>();
        
        switch (type)
        {
            case "HandleUpKnife":

                if (other.gameObject.CompareTag("Ground"))
                {
                    fallingSpeed = 0;
                    audioSource.Stop();
                }
                else if (other.gameObject.CompareTag("MovingPlatform") && movingPlatform==null)
                {
                    movingPlatform = other.gameObject.GetComponent<SpriteRenderer>();
                    // PlatformMovement movingInfo = other.gameObject.GetComponent<PlatformMovement>();
                    // horizontalSpeed = movingInfo.speed;
                    // leftBound = movingInfo.leftBound;
                    // rightBound = movingInfo.rightBound;
                    // direction = movingInfo.getDirection();
                    gapX = transform.position.x - movingPlatform.transform.position.x;
                    fallingSpeed = 0;
                    audioSource.Stop();
                }
                break;
            
            case "HandleDownKnife":
                // No action needed
                break;
            
            default:
                // None yet
                break;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            EventController.Instance.AddLump();
            Destroy(gameObject);
            AudioController.Instance.PlayChickenHit();
        }

    }

    void OnDestroy(){
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }
}
}
