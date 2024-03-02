using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int numberOfEggs;
    public int numberOfGolds;
    public bool isLocked;
    private int maxLocalScore=5;
    public List<int> highestScore = new List<int>(){0,0,0,0,0};  //the first one is the highest

    public int GetHighestScore(int index)
    {
        Debug.Log(index);
        return highestScore[index];
    }

    public void SetHighestScore(int index, int highest)
    {
        // Insert the new highest score at the specified position
        highestScore.Insert(index, highest);
        
        // If there are more than 5 scores in the list, remove the last one
        if (highestScore.Count > maxLocalScore)
        {
            highestScore.RemoveAt(5); // Removes the last element (index 5)
        }
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
    private int goldenEgg;
    public string currentGameMode;
    public string playerName;
    private int maxLocalScore=5;

     void Awake(){      // I use awake here instead of Start because I need the highest score to be initialized for the Rankview to be shown
        _instance = this;

        DontDestroyOnLoad(gameObject); // Keep the EventController object persistent across scenes
        // Load the JSON string from PlayerPrefs
        savedJson = PlayerPrefs.GetString("playerData");
        loadedData = JsonUtility.FromJson<PlayerData>(savedJson) ?? new PlayerData();
        string updatedJson = JsonUtility.ToJson(loadedData);
        Debug.Log("Updated and Saved Player Data - Eggs: " + loadedData.GetEggs() +
              ", Golds: " + loadedData.GetGold() +
              ", Unlocked: " + loadedData.IsLocked() +
              ", Highest Score: " + loadedData.GetHighestScore(0));
        for (int i = maxLocalScore-1; i>=0; i--){
             Debug.Log("Highest: " + loadedData.GetHighestScore(i));
        }
    }

    void Start(){
        goldenEgg = loadedData.GetEggs();
        //Debug.Log(goldenEgg);

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
        int index = -1;
        for (int i = maxLocalScore-1; i>=0; i--){
            int previousHighest = loadedData.GetHighestScore(i);
            // Compare the current score with the previous high score
            if (current_height > previousHighest)
            {
                index = i;
            }
            else{
                break;
            }
        }
        // Update PlayerPrefs with the new high score
        if (index>=0){
            Debug.Log(index);
            Debug.Log(current_height);
            loadedData.SetHighestScore(index,current_height);
            // if (index == 0){
            //     RankingView.Instance.setScores(current_height);
            // }
            UpdateData();
        }
    }

    public void UpdateGoldenEgg(int egg){
        goldenEgg += egg;
        //Debug.Log(goldenEgg);
        loadedData.SetEggs(goldenEgg);
        UpdateData();
    }

    public void setZero(){      //reset the PlayerPrefs info
        
    }

    public int getHighestScore(int index){
        //return  highestScore;
        return loadedData.GetHighestScore(index); 
    }

    public void setPlayerName(string name){
        playerName = name;
    }
}
