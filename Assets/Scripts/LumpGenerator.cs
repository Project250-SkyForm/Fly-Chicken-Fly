using UnityEngine;
using UnityEngine.UI;

public class LumpGenerator : MonoBehaviour
{
    public Canvas canvas; // Reference to the canvas where the images will be added
    public GameObject imagesToAdd; // Array of textures to add as images
    public float gap;
    private int numOfLumps=1;   //one because the gap*numOfLumps

    private static LumpGenerator _instance;
    public static LumpGenerator Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    // Method to generate images and add them to the canvas
    public void GenerateImages()
    {
        GameObject imageObject = Instantiate(imagesToAdd, canvas.transform);
         // Set the imageObject's position relative to the canvas
        Vector3 newPosition = canvas.transform.position;
        newPosition.y +=  gap*2; //distrance between image and text
        newPosition.y -= gap*numOfLumps ; // Adjust this value as needed
        imageObject.transform.position = newPosition;
        numOfLumps+=1;
    }
}
