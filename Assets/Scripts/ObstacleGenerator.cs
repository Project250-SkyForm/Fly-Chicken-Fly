﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleGenerator : MonoBehaviour
{
    public GameObject handleUpKnifePrefab; // Prefab of the knife object
    public GameObject handleDownKnifePrefab;
    public GameObject polaCanPrefab;
    public float generateRate; // Rate at which knives will be generated
    private float nextGenerateTime; // Time when the next knife should be generated
    public BackgroundScroll camera;
    private string[] obstaclesType = {"HandleUpKnife","HandleDownKnife","PolaCan"};
    private GameObject thisObstacle;

    private float cameraWidth = 40.0f;  //should be update after the implementation of shrink and expand of camera
    void Start()
    {
        // Initialize nextGenerateTime to the current time plus the generateRate
        nextGenerateTime = Time.time + generateRate;
        
    }

    void Update()
    {
        string type = GetRandomObstacleType();
        int direction = GetRandomDirection();
        switch(type){   // find that type of obstacle should be genrated
            case "HandleUpKnife":
                thisObstacle = handleUpKnifePrefab;
                break;
            case "HandleDownKnife":
                thisObstacle = handleDownKnifePrefab;
                break;
            case "PolaCan":
                thisObstacle = polaCanPrefab;
                break;
        }
        
        // Check if it's time to generate a knife
        if (Time.time >= nextGenerateTime)
        {
            Vector3 newPosition = camera.transform.position;
            newPosition.y = camera.transform.position.y + cameraWidth/2;
            switch (type){
                case "PolaCan":
                    if (direction == 1){
                        newPosition.x = camera.transform.position.x - cameraWidth/2;
                    }
                    else{
                        newPosition.x = camera.transform.position.x + cameraWidth/2;
                    }
                    break;

                default:
                    float randomOffset = Random.Range(-10f, 10f); // Random offset
                    newPosition.x = camera.transform.position.x + randomOffset; // New position with random offset
                    break;
            }
            transform.position = newPosition;
            // Instantiate a new knife object at the calculated position
            GameObject newObject = Instantiate(thisObstacle, newPosition, Quaternion.identity);
            // Play the obstacle falling sound once the obstacle is generated
            AudioController.Instance.PlayObstacleFall(newObject.GetComponent<AudioSource>());
            // change the newKnife z value to 0
            Vector3 newPos = newObject.transform.position;
            newPos.z = 0; 
            newObject.transform.position = newPos; 

            Obstacles newObstacle = newObject.GetComponent<Obstacles>();
            
            switch(type){
                case "PolaCan":
                    newObstacle.direction = direction;
                    newObstacle.horizontalSpeed = 10;
                    newObstacle.fallingSpeed =1;
                    break;
                default:
                    newObstacle.fallingSpeed =10;
                    break;
            }
            // Update nextGenerateTime for the next knife
            nextGenerateTime = Time.time + generateRate;
        }
    }

    private string GetRandomObstacleType()
    {
        // Generate a random index within the range of the array length
        int randomIndex = UnityEngine.Random.Range(0, obstaclesType.Length);

        // Return the string at the randomly generated index
        return obstaclesType[randomIndex];
    }

    private int GetRandomDirection(){
        int randomIndex = UnityEngine.Random.Range(0, 2) * 2 - 1; // only return 1 or -1
        return randomIndex;
    }

}
