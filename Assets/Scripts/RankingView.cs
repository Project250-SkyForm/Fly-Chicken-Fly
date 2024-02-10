using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace, this is used to find out the text component

public class RankingView : MonoBehaviour
{
    private int highestScore;
    private Text text;

     //Singleton pattern
    private static RankingView _instance;
    public static RankingView Instance { get { return _instance; } }

    // Start is called before the first frame update

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        highestScore = DataManager.Instance.getHighestScore();
        setScores(highestScore);
    }

     void Awake(){
        _instance = this;
    }

    public void setScores(int highest){
        text.text = "Highest Scores: " + highest + " M";
    }
}
