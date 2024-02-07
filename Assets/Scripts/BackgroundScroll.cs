using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float const_speed;
    public float speed; // control scroll speed from unity, for the camera itis -1, background is -0.8
    private Vector3 StartPos; // initial starting position

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position; // get initial starting pos of background
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(translation: Vector3.down * speed * Time.deltaTime); // move bg based on speed
    }

    public void Stop(){
        speed = 0;
    }

    public void Continue(){
        speed = const_speed;
    }
}
