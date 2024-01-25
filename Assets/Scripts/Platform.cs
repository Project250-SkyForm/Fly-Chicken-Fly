using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{   
    public string type;
    private const float camera_const = 8;
    private float leftEdge, rightEdge, topEdge, downEdge;
    private SpriteRenderer spriteRenderer;
    private float RangeX = 3, RangeY = 3;

    void Start(){
        leftEdge = CalculateEdges("left");
        rightEdge = CalculateEdges("right");
        topEdge = CalculateEdges("top");
        downEdge = CalculateEdges("down");

    }

    void Update(){
        if (type == "cloud" && transform.position.y < Camera.main.transform.position.y - camera_const){// we won't refresh the position of those ground level platform
            this.refresh();
        }
    }
    public void refresh(){
        
        transform.position += new Vector3(Random.Range(-1*RangeX, RangeX), Random.Range(-1*RangeX, 1)+Camera.main.transform.position.y);
         
     }

    public float CalculateEdges(string direction)     // some of the codes are from chatgpt,
    //return one of the edges of the sprite
    {
        // Ensure the object has a SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Get the left and right edges of the platform
            leftEdge = transform.position.x - spriteRenderer.bounds.size.x / 2f;
            rightEdge = transform.position.x + spriteRenderer.bounds.size.x / 2f;
            topEdge = transform.position.y + spriteRenderer.bounds.size.y / 2f;
            downEdge = transform.position.y - spriteRenderer.bounds.size.y / 2f;
        }
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
            return downEdge;
        }
        else{   // should not happen, just in case debug warning
            return 0;
        }
    }

    public float getLeft(){
        return leftEdge;
    }

    public float getRight(){
        return rightEdge;
    }

    public float getTop(){
        //Debug.Log("Top Edge: " + topEdge);
        return topEdge;
    }

    public float getDown(){
        return downEdge;
    }

    public string getType(){
        return type;
    }
}
