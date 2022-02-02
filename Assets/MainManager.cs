using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName;
    public int highScore;
    public string highScoreName;
    public bool isHighScore;

    private int count = 0;


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

    public void UpdateScore()
    {
        GameObject.Find("Count").GetComponent<Text>().text = playerName + ": " + count;
    }
}
