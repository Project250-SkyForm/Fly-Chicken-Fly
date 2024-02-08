using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Singleton pattern
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }


    void Awake(){
        _instance = this;
        DontDestroyOnLoad(gameObject); // Keep the EventController object persistent across scenes
    }

    public void SetUIActive(GameObject ui)
    {   
        ui.SetActive(true);
    }

    public void SetUINotActive(GameObject ui)
    {   
        ui.SetActive(false);
    }
}
