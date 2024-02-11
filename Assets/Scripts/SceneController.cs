using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Singleton pattern
    private static SceneController _instance;
    public static SceneController Instance { get { return _instance; } }


    void Awake(){
        _instance = this;
        DontDestroyOnLoad(gameObject); // Keep the EventController object persistent across scenes
    }

    public void ChangeScene(string targetSceneName)
    {
        Debug.Log(targetSceneName);
        
        SceneManager.LoadScene(targetSceneName);
    }

    public void SetGameMode(string gameMode){
        DataManager.Instance.currentGameMode = gameMode;
    }
}
