
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance {get {return _instance;}}
    
    // Initialize audio source and audio clips
    public AudioSource chicken;
    public AudioClip chickenHit, chickenJump, chickenGrassWalk, chickenCloudWalk, chickenLand;
    public AudioClip[] chickenBarks;

    void Awake(){
        _instance = this;
    }

    // Use the following functions in other scripts to call
    // desired sound effect
    public void PlayChickenHit(){
        chicken.PlayOneShot(chickenHit);
    }

    public void PlayChickenJump(){
        chicken.PlayOneShot(chickenJump);
    }

    public void PlayChickenGrassWalk(){
        chicken.PlayOneShot(chickenGrassWalk);
    }

    public void PlayChickenCloudWalk(){
        chicken.PlayOneShot(chickenCloudWalk);
    }

    public void PlayChickenLand(){
        chicken.PlayOneShot(chickenLand);
    }

    // Plays a bark every 10 seconds 
    public void playRandomSound(){
        Invoke ("PlayChickenBarks", 10);
    }

    public void PlayChickenBarks(){
        chicken.PlayOneShot(chickenBarks[Random.Range(0, chickenBarks.Length)]);
        playRandomSound();
    }
}
