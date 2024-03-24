using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;
    public string videoFileName;

    void Start()
    {
        PlayVideo();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    public void PlayVideo(){
        if (videoPlayer){
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            if (PlayerPrefs.GetInt("mute")==0){
                // Set the audio output mode to AudioSource
                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

                // Find the AudioSource component under the parent GameObject of the plane
                AudioSource audioSource = GetComponentInParent<AudioSource>();
            }
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (vp == videoPlayer)
        {
            // Load the next scene when the video ends
            if (DataManager.Instance.currentGameMode == "cutscene"){
                nextSceneName = "StartScene";
            }else{
                nextSceneName = "PlayScene";
            }
            SceneController.Instance.ChangeScene(nextSceneName);
        }
    }
}
