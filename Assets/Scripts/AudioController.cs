
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance {get {return _instance;}}
    
    // Initialize audio source and audio clips
    public AudioSource chicken, backgroundMusicSource;
    public AudioClip chickenJump, chickenGrassWalk, chickenCloudWalk, chickenLand, obstacleFall;
    public float hitVolume, jumpVolume, grassWalkVolume, cloudWalkVolume, landVolume, obstacleFallVolume, barkVolume, backgroundMusicVolume;
    public AudioClip[] chickenHits, ambientSounds, backgroundMusicTracks;

    void Awake(){
        _instance = this;
        playRandomSound();
        playBackgroundMusic();
        
    }

    // Use the following functions in other scripts to call
    // desired sound effect
    public void PlayChickenHit(){
        chicken.volume = hitVolume;
        int randomIndex = Random.Range(0, chickenHits.Length); 
        chicken.PlayOneShot(chickenHits[randomIndex]);
    }

    public void PlayChickenJump(){
        chicken.volume = jumpVolume;
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

    // Plays an ambient sound every 10 seconds 
    public void playRandomSound(){
        Invoke ("PlayAmbientSounds", 10);
    }

    public void PlayAmbientSounds(){
        chicken.volume = barkVolume;
        chicken.PlayOneShot(ambientSounds[Random.Range(0, ambientSounds.Length)]);
        playRandomSound();
    }

    public void PlayObstacleFall(AudioSource obstacleAudioSource){
        obstacleAudioSource.volume = obstacleFallVolume;
        obstacleAudioSource.clip = obstacleFall;
        obstacleAudioSource.Play();
    }

    public void playBackgroundMusic(){
    backgroundMusicSource.clip = backgroundMusicTracks[0]; // Assign the first background music track
    backgroundMusicSource.loop = false;
    backgroundMusicSource.volume = backgroundMusicVolume; 
    backgroundMusicSource.Play();

    // Start looping another track after the first one finishes
    StartCoroutine(LoopBackgroundMusic());
    }

    IEnumerator LoopBackgroundMusic()
    {
        // Wait until the first track finishes playing
        yield return new WaitForSeconds(backgroundMusicTracks[0].length);

        backgroundMusicSource.clip = backgroundMusicTracks[1]; 
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = backgroundMusicVolume;
        backgroundMusicSource.Play();
    }
}
