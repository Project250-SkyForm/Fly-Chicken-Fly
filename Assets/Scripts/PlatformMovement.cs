using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed = 2f;
    public float leftBound = -10f;  // Set the left boundary for the platform
    public float rightBound = 10f;  // Set the right boundary for the platform

    private int direction = 1;  // 1 for moving right, -1 for moving left

    void Update()
    {
        // Move the platform horizontally
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Check if the platform is beyond the right or left boundary
        if (transform.position.x >= rightBound || transform.position.x <= leftBound)
        {
            // Change direction to move the platform back
            direction *= -1;
        }
    }

    // Reset the platform's position to the opposite side when it goes beyond the boundaries
    void ResetPosition()
    {
        float newX = direction > 0 ? leftBound : rightBound;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    public int getDirection(){
        return direction;
    }
}
