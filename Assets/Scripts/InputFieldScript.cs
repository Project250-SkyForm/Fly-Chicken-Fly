using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour
{
    public InputField inputField1;
    public InputField inputField2;
    public Text text1;  // text to in input field
    public Text text2;  //  text to be changed

    private string text;

    public void ReadStringInput(string s){
        text = s;
        
        Debug.Log(text1.text);
    }
}
