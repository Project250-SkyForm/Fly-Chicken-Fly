using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public float minPosX = -5f; // Min x position
    public float maxPosX = 5f;  // Max x position
    public float movingSpeed = 2f; // Speed of movement along x-axis
    private bool isMoving = false;

    // Call this method to start moving
    public void MoveBoundary(bool toRight, float minPosX, float maxPosX)
    {
        this.minPosX = minPosX;
        this.maxPosX = maxPosX;
        if (!isMoving)
        {
            StartCoroutine(MoveBoundaryPosition(toRight));
        }
    }

    public void moveY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }

    private IEnumerator MoveBoundaryPosition(bool toRight)
    {
        isMoving = true;
        float targetPosX = toRight ? maxPosX : minPosX;
        while (Mathf.Abs(transform.position.x - targetPosX) > 0.01f)
        {
            Vector3 newPos = transform.position;
            newPos.x = Mathf.MoveTowards(newPos.x, targetPosX, movingSpeed * Time.deltaTime);
            transform.position = newPos;
            yield return null;
        }
        isMoving = false;
    }
}
