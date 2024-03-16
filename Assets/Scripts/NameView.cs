using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NameView : MonoBehaviour
{
    Text text;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        string name = DataManager.Instance.GetPlayerName(index);
        text.text = name;
        Debug.Log(name);
    }

    
}
