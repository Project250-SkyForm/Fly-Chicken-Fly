using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class PlayerData
{
    public int numberOfEggs;
    public int numberOfGolds;
    public bool isLocked;
    public int highestScore;

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void SetHighestScore(int highest)
    {
        highestScore = highest;
    }

    public int GetEggs()
    {
        return numberOfEggs;
    }

    public void SetEggs(int eggs)
    {
        numberOfEggs = eggs;
    }

    public int GetGold()
    {
        return numberOfGolds;
    }

    public void SetGold(int gold)
    {
        numberOfGolds = gold;
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void SetLock(bool isLock)
    {
        isLocked = isLock;
    }
}


public class DataManager : MonoBehaviour
{
     //Singleton pattern
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    private string savedJson;
    private PlayerData loadedData;
    public string currentGameMode;

    // Retrieve the previous high score from PlayerPrefs
    //private int highestScore;
    
     void Awake(){      // I use awake here instead of Start because I need the highest score to be initialized for the Rankview to be shown
        _instance = this;

        DontDestroyOnLoad(gameObject); // Keep the EventController object persistent across scenes
        // Load the JSON string from PlayerPrefs
        savedJson = PlayerPrefs.GetString("playerData");
        loadedData = JsonUtility.FromJson<PlayerData>(savedJson) ?? new PlayerData();
        // loadedData.SetHighestScore(0);
        // UpdateData();
        string updatedJson = JsonUtility.ToJson(loadedData);
        Debug.Log("Updated and Saved Player Data - Eggs: " + loadedData.GetEggs() +
              ", Golds: " + loadedData.GetGold() +
              ", Unlocked: " + loadedData.IsLocked() +
              ", Highest Score: " + loadedData.GetHighestScore());
    }

    void Start(){

    }


    void UpdateData(){
        // Convert the modified data back to a JSON string
        string updatedJson = JsonUtility.ToJson(loadedData);

        // Save the updated JSON string to PlayerPrefs
        PlayerPrefs.SetString("playerData", updatedJson);
        PlayerPrefs.Save();
    }

    public void UpdateHighestScore(int current_height)
    {
        int previousHighest = loadedData.GetHighestScore();

        // Compare the current score with the previous high score
        if (current_height > previousHighest)
        {
            // Update PlayerPrefs with the new high score
            loadedData.SetHighestScore(current_height);
            RankingView.Instance.setScores(current_height);
            UpdateData();
        }
    }

    public void setZero(){      //reset the PlayerPrefs info
        
    }

    public int getHighestScore(){
        //return  highestScore;
        return loadedData.GetHighestScore();
    }
}
