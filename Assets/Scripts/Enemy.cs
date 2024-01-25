using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AnimatedEntity
{

    private float RangeX = 5, RangeY = 5;

    // Start is called before the first frame update
    void Start()
    {
        AnimationSetup();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationUpdate();
        // if (transform.position.x<0){
        //     transform.position += new Vector3(Random.Range(1*RangeX, RangeX), Random.Range(-1*RangeY, RangeY))*Time.deltaTime;
        // } if (transform.position.y<0){
        //     transform.position += new Vector3(Random.Range(-1*RangeX, RangeX), Random.Range(1*RangeY, RangeY))*Time.deltaTime;
        // } else{
        //     transform.position += new Vector3(Random.Range(-1*RangeX, RangeX), Random.Range(-1*RangeY, RangeY))*Time.deltaTime;
        // }
        transform.position += new Vector3(Random.Range(-1*RangeX, RangeX), Random.Range(-1*RangeY, RangeY))*Time.deltaTime;
    }
}
