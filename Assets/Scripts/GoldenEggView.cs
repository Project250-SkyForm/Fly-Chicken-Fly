using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GoldenEggView : MonoBehaviour
{
    private Text text; // Now text is of type UnityEngine.UI.Text
    public int eggs;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        eggs = EventController.Instance.egg;
        text.text = "" + eggs;    
    }

    public void UpdateGoldenEgg(int egg)
    {   
        eggs=egg;
        text.text = "" + eggs;    
    }
}
