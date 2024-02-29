using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAudioController : MonoBehaviour
{
    private void Awake()
    {
        // Prevent destruction of this GameObject when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
