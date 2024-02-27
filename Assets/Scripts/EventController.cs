using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController _instance;
    public static EventController Instance { get { return _instance; } }

    private int lump=0;
    private int egg=0;

    public int shrinkScore = 30;
    public float minFieldOfView = 2f;
    public float shrinkAmount = 0.005f;
    public float initialBoundary = 0;

    public float minXBoundary = -1;
    public float maxXBoundary = 1;
    public bool startShrinking = false;

    public PlayerMovement player;
    public GameObject lump1;
    public GameObject lump2;
    public GameObject lump3;
    public EnemyMovement enemy;
    public LumpView lumpView;
    public GoldenEggView eggView;
    public BackgroundScroll camera;
    public BackgroundScroll background;
    public Camera gameCamera;
    public BoundaryController leftBoundary;
    public BoundaryController rightBoundary;
    public float cameraGoDownDistance;

    void Awake(){
        _instance = this;
    }
    
    void Update(){
        if (initialBoundary.Equals(0)){
            UpdateXBoundary();
            initialBoundary = 1;
        }
        // Event when player touch the bottom of the camera, the camera should push the player up a jump
        if (player.transform.position.y < camera.transform.position.y - cameraGoDownDistance){
            Vector3 newPos = camera.transform.position;
            if (camera.transform.position.y - cameraGoDownDistance < 8.5f){
                newPos.y = 8.5f;
            }
            else{
                newPos.y -= 4; 
            }
            camera.transform.position = newPos; 
            // UpdateXBoundary();
        }
        /*
        not used speeding up camera
        // if the player is higher than the half height of camera
        else if (player.transform.position.y > camera.transform.position.y ){
            // Vector3 newPos = camera.transform.position;
            // newPos.y += 4;
            // camera.transform.position = newPos;
            camera.transform.Translate(translation: Vector3.down * -5 * Time.deltaTime);
        }
        */
        if (player.transform.position.y > shrinkScore){
            Debug.Log("Y Position: " + player.transform.position.y + " Shrink Score: " + shrinkScore);
            if (!startShrinking){
                startShrinking = true;
                StartShrinkingBoundaries();
            }
        }
    }

    private void UpdateXBoundary(){
        float backgroundWidth = background.spriteRenderer.bounds.size.x; // Get the actual width of the background sprite
        float halfWidth = backgroundWidth / 2f;
        
        // Calculate boundaries
        minXBoundary = background.transform.position.x - halfWidth;
        maxXBoundary = background.transform.position.x + halfWidth;
    }

    public void AddLump(){
        lump += 1;
        lumpView.UpdateLump(lump);
        if (lump == 1){
            lump1.SetActive(true);
        }
        if (lump == 2){
            lump2.SetActive(true);
        }
        if (lump == 3){
            lump3.SetActive(true);
        }
        if (DataManager.Instance.currentGameMode == "death" && lump >=3){
            Debug.Log("End Game");
            EndGame();
            Destroy(gameObject);
        }
        //LumpGenerator.Instance.GenerateImages();
    }

    public float GetMinXBoundary(){
        return minXBoundary;
    }

    public float GetMaxXBoundary(){
        return maxXBoundary;
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


    private void StartShrinkingBoundaries()
    {
        leftBoundary.StartScaling(true);
        rightBoundary.StartScaling(true);
        Debug.Log("Start Shrinking Boundaries ");
    }

    public void StopShrinkingBoundaries()
    {
        leftBoundary.StartScaling(false);
        rightBoundary.StartScaling(false);
        Debug.Log("Stop Shrinking Boundaries");
    }

    public void AddGoldenEgg(){
        egg+=1;
        eggView.UpdateGoldenEgg(egg);
        //DataManager.Instance.UpdateGoldenEgg(egg);
    }

    public void EndGame(){
        DataManager.Instance.UpdateGoldenEgg(egg);
        SceneController.Instance.ChangeScene("GameOverScene");
    }
}
