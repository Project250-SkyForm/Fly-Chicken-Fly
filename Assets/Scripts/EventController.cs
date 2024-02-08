using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController _instance;
    public static EventController Instance { get { return _instance; } }

    private int lump=0;

    public PlayerMovement player;
    public LumpView lumpView;
    public BackgroundScroll camera;
    public BackgroundScroll background;
    public float cameraGoDownDistance;

    void Awake(){
        _instance = this;
    }
    
    void Update(){
        if (player.transform.position.y < camera.transform.position.y - cameraGoDownDistance){
            Vector3 newPos = camera.transform.position;
            if (camera.transform.position.y - cameraGoDownDistance < 8.5f){
                newPos.y = 8.5f;
                
            }
            else{
                newPos.y -= cameraGoDownDistance; 
            }
            camera.transform.position = newPos; 
            StopCameraMoving();
            StopBackgroundMoving();
            AddLump();
        }
    }

    public void AddLump(){
        lump += 1;
        lumpView.UpdateLump(lump);
        if (DataManager.Instance.currentGameMode == "death" && lump >=3){
            Debug.Log("End Game");
            SceneController.Instance.ChangeScene("GameOverScene");
        }
        LumpGenerator.Instance.GenerateImages();
    }

    public void StopCameraMoving(){
        camera.Stop();
    }

    public void StartCameraMoving(){
        camera.Continue();
    }

    public void StopBackgroundMoving(){
        background.Stop();
    }

    public void StartBackgroundMoving(){
        background.Continue();
    }
}
