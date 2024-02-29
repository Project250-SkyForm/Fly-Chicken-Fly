using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace, this is used to find out the text component

public class LocalRankView : MonoBehaviour
{
    private int highestScore;
    public int rank;
    private Text text;

    // Start is called before the first frame update

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        highestScore = DataManager.Instance.getHighestScore(rank-1);
        setScores(highestScore);
    }

    public void setScores(int highest){
        text.text =  highest + " m";
    }
}
