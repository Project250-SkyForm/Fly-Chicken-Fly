using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace, this is used to find out the text component

public class LocalRankView : MonoBehaviour
{
    private int highestScore;
    private int highestMeter;
    private int goldenEgg;
    public int rank;
    public string type;
    private Text text;

    // Start is called before the first frame update

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        highestScore = DataManager.Instance.getHighestScore(rank-1);
        goldenEgg = DataManager.Instance.getGoldenEgg(rank-1);
        highestMeter = DataManager.Instance.getHighestMeter(rank-1);
        if (type=="goldenEgg"){
            setGoldenEgg(goldenEgg);
        }else if (type == "altitude"){
            setMeter(highestMeter);
        }
        else{
            setScores(highestScore);
        }
        
    }

    public void setScores(int highest){
        text.text =  highest + "";
    }

    public void setGoldenEgg(int eggs){
        text.text =  eggs+"";
    }

    public void setMeter(int meter){
        text.text =  meter + "";
    }

}
