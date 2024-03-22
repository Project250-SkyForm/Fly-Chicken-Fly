using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace, this is used to find out the text component

public class GlobalRankView : MonoBehaviour
{
    private int highestScore;
    public int rank;
    public Text rank1;
    public Text rank2;
    public Text rank3;
    public Text rank4;
    public Text rank5;
    public Text playerName1;
    public Text playerName2;
    public Text playerName3;
    public Text playerName4;
    public Text playerName5;

    // Start is called before the first frame update

    void Start()
    {
        setScores();
    }

    public void setScores(){
        // rank1.text = DataManager.Instance.getGlobalHighestScore(0) + " m";
        // rank2.text = DataManager.Instance.getGlobalHighestScore(1) + " m";
        // rank3.text = DataManager.Instance.getGlobalHighestScore(2) + " m";
        // rank4.text = DataManager.Instance.getGlobalHighestScore(3) + " m";
        // rank5.text = DataManager.Instance.getGlobalHighestScore(4) + " m";
        // playerName1.text = DataManager.Instance.getGlobalPlayerName(0);
        // playerName2.text = DataManager.Instance.getGlobalPlayerName(1);
        // playerName3.text = DataManager.Instance.getGlobalPlayerName(2);
        // playerName4.text = DataManager.Instance.getGlobalPlayerName(3);
        // playerName5.text = DataManager.Instance.getGlobalPlayerName(4);
    }
}
