using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public float minScaleX = 8f;
    public float maxScaleX = 18f;
    public float scalingSpeed = 0.5f;
    private bool isScaling = false;


    // Call this method to start scaling
    public void Rescale(bool expand, float minScaleX, float maxScaleX)
    {
        this.minScaleX = minScaleX;
        this.maxScaleX = maxScaleX;
        if (!isScaling)
        {
            // Debug.Log("Reshrinking maxScaleX: " + maxScaleX);
            StartCoroutine(RescaleBoundary(expand));
        }
    }


    private IEnumerator RescaleBoundary(bool expand)
    {
        isScaling = true;
        float targetScaleX = expand ? maxScaleX : minScaleX;
        while (Mathf.Abs(transform.localScale.x - targetScaleX) > 0.01f)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.MoveTowards(newScale.x, targetScaleX, scalingSpeed * Time.deltaTime);
            transform.localScale = newScale;
            yield return null;
        }
        // Debug.Log("RescaleBoundary finished");
        isScaling = false; 
    }
}