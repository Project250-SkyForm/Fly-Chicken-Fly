using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreView : MonoBehaviour
{
    public AltitudeView altitudeView;
    public GoldenEggView goldenEggView;
    private Text text; 
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)altitudeView.score + goldenEggView.eggs*10;
        text.text = score+"";
    }
}
