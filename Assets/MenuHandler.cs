using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
// Handles menu button presses
//

public class MenuHandler : MonoBehaviour
{
    // Start the game (from the menu)
    public void StartGame()
    {
        Text nF = GameObject.Find("Name Text").GetComponent<Text>();
        if (nF.text == "") return;
        MainManager.Instance.playerName = nF.text;
        LoadMain();
        
    }

    // Load the actual game
    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }
}
