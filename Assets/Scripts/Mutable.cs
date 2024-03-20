using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutable : MonoBehaviour
{
    private bool isMuted = false;
    public GameObject muteButton;
    public GameObject unMuteButton;

    void Start(){
        if (AudioListener.volume == 0){
            UIController.Instance.SetUIActive(unMuteButton);
        }
    }
    // Function to toggle mute state
    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1; // Mute or unmute all audio
    }

    // Function to explicitly mute the audio
    public void Mute()
    {
        isMuted = true;
        AudioListener.volume = 0; // Mute all audio
    }

    // Function to explicitly unmute the audio
    public void Unmute()
    {
        isMuted = false;
        AudioListener.volume = 1; // Unmute all audio
    }
}
