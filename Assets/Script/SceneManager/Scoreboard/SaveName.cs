using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    public InputField textBox;
    Player player;
    
    // Start is called before the first frame update
    public void clickSaveButton()
    {
        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log(PlayerPrefs.GetString("name") + ": "+PlayerPrefs.GetInt("score"));
        
        
    }
    
}
