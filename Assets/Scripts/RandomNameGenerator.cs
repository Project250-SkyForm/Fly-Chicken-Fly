using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RandomNameGenerator : MonoBehaviour
{
    InputField inputText;
    string[] adjectives = { "Happy", "Silly", "Funky", "Bubbly", "Giggly", "Crazy", "Cheerful", "Wacky", "Whimsical", "Zesty", "Bloody", "Fried", "Humble", "Invincible" };
    string[] nouns = { "Banana", "Penguin", "Unicorn", "Pancake", "Jellybean", "Squid", "Lemon", "Noodle", "Rainbow", "Marshmallow", "Chicken","JoJo", "Dio", "Triangle"};

    void Start(){
        if (DataManager.Instance.playerName !=null){
            inputText = gameObject.GetComponent<InputField>();
            string playerName = GetRandomName();
            inputText.text = playerName;
        }
    }
    // Generate a random name
    public string GetRandomName()
    {
        string adjective = adjectives[UnityEngine.Random.Range(0, adjectives.Length)];
        string noun = nouns[UnityEngine.Random.Range(0, nouns.Length)];
        Debug.Log(adjective + " " + noun);
        return adjective + " " + noun;
    }

    public void SetRandomName(){
        string playerName = GetRandomName();
        inputText.text = playerName;
    }

}
