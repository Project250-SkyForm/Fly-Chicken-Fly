using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject bonOn;
    public GameObject boffOff;
    public GameObject bonOff;
    public GameObject boffOn;
    public GameObject BGmusic;

    //Singleton pattern
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }


    void Awake(){
        _instance = this;
    }

    void Start(){
        if (PlayerPrefs.HasKey("BGMute")){
            if (PlayerPrefs.GetInt("BGMute")==0){
                SetUINotActive(BGmusic);
                UIController.Instance.SetUIActive(bonOff);
                UIController.Instance.SetUIActive(boffOn);
            }else{
                UIController.Instance.SetUIActive(bonOn);
                UIController.Instance.SetUIActive(boffOff);
            }
        }else{
            PlayerPrefs.SetInt("BGMute",1);
            UIController.Instance.SetUIActive(bonOn);
            UIController.Instance.SetUIActive(boffOff);
        }
    }

    public void SetUIActive(GameObject ui)
    {   
        ui.SetActive(true);
    }

    public void SetUINotActive(GameObject ui)
    {   
        ui.SetActive(false);
    }

    public void MuteBG()
    {
        PlayerPrefs.SetInt("BGMute",0);
        SetUINotActive(BGmusic); // Mute all audio
    }

    // Function to explicitly unmute the audio
    public void UnmuteBG()
    {
        PlayerPrefs.SetInt("BGMute",1);
        SetUIActive(BGmusic); // Unmute all audio
    }

}
