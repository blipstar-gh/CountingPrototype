using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    public void StartGame()
    {
        Text nF = GameObject.Find("Name Text").GetComponent<Text>();
        if (nF.text == "") return;
        MainManager.Instance.playerName = nF.text;
        LoadMain();
        
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }
}
