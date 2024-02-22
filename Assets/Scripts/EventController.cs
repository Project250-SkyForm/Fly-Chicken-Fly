using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController _instance;
    public static EventController Instance { get { return _instance; } }

    private int lump=0;

    public int shrinkScore = 30;
    public float minFieldOfView = 2f;
    public float shrinkAmount = 0.005f;

    public float minXBoundary = -1;
    public float maxXBoundary = 1;

    public PlayerMovement player;
    public LumpView lumpView;
    public BackgroundScroll camera;
    public BackgroundScroll background;
    public Camera gameCamera;
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
            UpdateXBoundary();
            StopCameraMoving();
            StopBackgroundMoving();
            AddLump();
            AudioController.Instance.PlayChickenHit();
        }
        if (player.transform.position.y > shrinkScore){
            Debug.Log("Y Position: " + player.transform.position.y + " Shrink Score: " + shrinkScore);
            ShrinkView();
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
        if (DataManager.Instance.currentGameMode == "death" && lump >=3){
            Debug.Log("End Game");
            SceneController.Instance.ChangeScene("GameOverScene");
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

    private void ShrinkView(){
        StartCoroutine(ShrinkViewOverTime());
    }

    // Coroutine for smooth shrinking
    private IEnumerator ShrinkViewOverTime(){
        float targetScaleX = background.transform.localScale.x * shrinkAmount;
        float minScaleX = minFieldOfView / 5.0f;

        if (targetScaleX > minScaleX){
            while (background.transform.localScale.x > targetScaleX && targetScaleX > minScaleX){
                Vector3 scale = background.transform.localScale;
                scale.x = Mathf.MoveTowards(scale.x, targetScaleX, Time.deltaTime * shrinkAmount); // Adjust Time.deltaTime * shrinkAmount to control the speed
                background.transform.localScale = scale;

                float backgroundWidth = background.spriteRenderer.bounds.size.x; // Get the actual width of the background sprite
                float halfWidth = backgroundWidth / 2f;
                
                // Calculate boundaries
                minXBoundary = background.transform.position.x - halfWidth;
                maxXBoundary = background.transform.position.x + halfWidth;

                yield return null;
            }
        }
    }
}
