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
    }

    public void ChangeScene(string targetSceneName)
    { 
        if (targetSceneName == "VideoScene"){
            if(DataManager.Instance.playCutScene || DataManager.Instance.currentGameMode == "cutscene" ){
                SceneManager.LoadScene(targetSceneName);
            }
            else{
                SceneManager.LoadScene("PlayScene");
            }
        }else{
            SceneManager.LoadScene(targetSceneName);
        }
    }

    public void SetGameMode(string gameMode){
        DataManager.Instance.currentGameMode = gameMode;
    }

    public string CheckScene(){
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Get the name of the current scene
        string sceneName = currentScene.name;
        Debug.Log(sceneName);    
        return sceneName;
    }
}
