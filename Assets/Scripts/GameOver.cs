using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public float elapsedTime;
    public float duration;
    public GameObject gameOver;
    public GameObject score;
    public GameObject newScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime >= duration){
            UIController.Instance.SetUINotActive(gameOver);
            if (DataManager.Instance.getNewHighestScore){
                UIController.Instance.SetUIActive(newScore);
            }
            UIController.Instance.SetUIActive(score);
        }
    }
}
