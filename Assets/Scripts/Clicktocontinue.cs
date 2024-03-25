using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicktocontinue : MonoBehaviour
{
    public GameObject newScore;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            UIController.Instance.SetUINotActive(newScore);
            audioSource.Play();
        }
    }
}
