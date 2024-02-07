using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace, this is used to find out the text component

public class AltitudeView : MonoBehaviour
{
    private float meter;
    private Text text; // Now text is of type UnityEngine.UI.Text
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        meter = player.transform.position.y;
        
        // Get the Text component from the gameObject
        text = gameObject.GetComponent<Text>();

        // Check if the Text component is found
        if (text == null)
        {
            Debug.LogError("Text component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        meter = player.transform.position.y;
        text.text = "Altitude:" + meter;    
    }
}
