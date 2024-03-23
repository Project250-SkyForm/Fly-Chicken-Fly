using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSound : MonoBehaviour
{

    public string obstacleType;

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            if (obstacleType == "grass"){
                AudioController.Instance.PlayChickenGrassLand();
            }
            if (obstacleType == "cloud"){
                AudioController.Instance.PlayChickenCloudLand();
            }            
            if (obstacleType == "stars"){
                AudioController.Instance.PlayChickenStarsLand();
            }
        }
    }
}
