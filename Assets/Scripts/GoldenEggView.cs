using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GoldenEggView : MonoBehaviour
{
    private Text text; // Now text is of type UnityEngine.UI.Text

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    public void UpdateGoldenEgg(int egg)
    {
        text.text = "" + egg;    
    }
}
