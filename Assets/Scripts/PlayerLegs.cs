using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLegs : MonoBehaviour
{
    public GameObject player;
    PlayerMovement playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && playerControl.isJumping)
        { 
                playerControl.isJumping = false;
        }
        if (other.gameObject.CompareTag("Platform") && playerControl.isJumping)
        {
            playerControl.isJumping = false;
            player.transform.parent = other.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
    }
    
}
