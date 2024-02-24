using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float gapY;      // the distance that the camera is higher than player
    public float smoothTime = 0.3f;  // Smoothing time for camera movement
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly move the camera to the target position
        Vector3 targetPosition = new Vector3(transform.position.x, player.transform.position.y + gapY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
