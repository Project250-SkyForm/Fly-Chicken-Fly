﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class RandomNameGenerator : MonoBehaviour
{
    InputField inputText;
    string[] adjectives = { "Happy", "Silly", "Funky", "Bubbly", "Giggly", "Crazy", "Cheerful", "Wacky", "Whimsical", "Zesty", "Bloody", "Fried", "Humble", "Invincible" };
    string[] nouns = { "Banana", "Penguin", "Unicorn", "Pancake", "Jellybean", "Squid", "Lemon", "Noodle", "Rainbow", "Marshmallow", "Chicken","JoJo", "Dio", "Triangle"};

    void Start(){
        inputText = gameObject.GetComponent<InputField>();
        if (!PlayerPrefs.HasKey("playerName")){
            string playerName = "I’m just chicken";
            inputText.text = playerName;
            PlayerPrefs.SetString("playerName", playerName);
            Debug.Log("Generate New Name");
            DataManager.Instance.playerName = playerName;
        }
        else{
            string playerName = PlayerPrefs.GetString("playerName");
            inputText.text = playerName;
            Debug.Log("Maintain Old Name");
            DataManager.Instance.playerName = playerName;
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
        PlayerPrefs.SetString("playerName", playerName);
        inputText.text = playerName;
        DataManager.Instance.playerName = playerName;
    }

    public void ConfirmName(){
        string playerName = inputText.text;
        PlayerPrefs.SetString("playerName", playerName);
        DataManager.Instance.playerName = playerName;
    }

}
