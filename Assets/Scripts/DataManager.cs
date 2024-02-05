using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
     //Singleton pattern
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }
    
    public List<int> myIntList;
    private int max_num_score = 10;
    private int scoreList;

     // Retrieve the previous high score from PlayerPrefs
    private int highestScore;
    
     void Awake(){
        _instance = this;
        highestScore = PlayerPrefs.GetInt("highestScore", 0);
    }


    void Start()
    {
        // can be used for storing list

        // // Convert the list to a JSON string
        // string json = JsonUtility.ToJson(myIntList);        

        // // Save the JSON string to PlayerPrefs
        // PlayerPrefs.SetString("myIntList", json);
        // PlayerPrefs.Save();
        // // Retrieve the JSON string from PlayerPrefs
        // string savedJson = PlayerPrefs.GetString("myIntList");

        // // Convert the JSON string back to a list
        // List<int> loadedList = JsonUtility.FromJson<List<int>>(savedJson);

        // // Now 'loadedList' contains the values stored in 'myIntList'
        // Debug.Log("Loaded List: " + string.Join(", ", loadedList));
        // Debug.Log("Highest: " + string.Join(", ", highestScore));
    }

    public void UpdateHighestScore(int current_height)
    {
       
        int previousHighest = PlayerPrefs.GetInt("highestScore", 0);

        // Compare the current score with the previous high score
        if (current_height > previousHighest)
        {
            // Update PlayerPrefs with the new high score
            PlayerPrefs.SetInt("highestScore", current_height);
            PlayerPrefs.Save(); // Save the current_height to make changes permanent
            Debug.Log("Congratulations! New high score!");
            highestScore = current_height;
        }

        // Display rankings or any other relevant information
        Debug.Log("Previous High Score: " + previousHighest);
    }

    public void setZero(){      //reset the PlayerPrefs info
        PlayerPrefs.SetInt("highestScore",0);
        PlayerPrefs.Save();
        highestScore = 0;
    }

    public int getHighestScore(){
        return  highestScore;
    }
}
