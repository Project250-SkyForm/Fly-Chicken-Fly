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

    void Awake(){
        _instance = this;
    }
    
    void Update(){
        if (player.transform.position.y < camera.transform.position.y - 10){
            StopCameraMoving();
            // should set the player to a platform
        }
    }

    public void AddLump(){
        lump += 1;
        lumpView.UpdateLump(lump);
    }

    public void StopCameraMoving(){
        camera.Stop();
    }

    public void StartCameraMoving(){
        camera.Continue();
    }
}
