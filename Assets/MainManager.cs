using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//
// Persists the player name between scenes
// Saves and Loands the high score and high score name
// Updates the score and checks for a new high score
//

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;     // static instance available to all classes

    public string playerName;       // current player name
    public int highScore;           // high score
    public string highScoreName;    // high score name
    public bool isHighScore;        // whether player has just beaten high score

    private int count = 0;          // score


    private void Awake()
    {

        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //
    // Handle saving and loading high score data
    //

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }

    // Increase score by 1 point. Check if this is a new high score
    public void AddToScore()
    {
        count += 1;
        UpdateScore();
        GameObject.Find("Box").GetComponent<MoveBox>().CalculateTarget();
        GameObject.Find("Cannon").GetComponent<CannonController>().NextLevel(count);
        if (count > highScore)
        {
            highScore = count;
            highScoreName = playerName;
            SaveScore();
            isHighScore = true;
            GameObject.Find("Highscore Text").GetComponent<Text>().text = "NEW HIGH SCORE!";
        }
    }

    // Update score
    public void UpdateScore()
    {
        GameObject.Find("Count").GetComponent<Text>().text = playerName + ": " + count;
    }
}
