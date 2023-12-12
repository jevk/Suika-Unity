using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayedGame
{
    public List<int> score;
}

public class Highscore : MonoBehaviour
{
    public PlayedGame games;
    public string SavePath;

    private void Start()
    {
        SavePath = Application.persistentDataPath + "/games.json";
        Debug.Log(SavePath);
        games = new PlayedGame();
        LoadScore();
    }

    public void GetScore()
    {
        // Get score from "score" text object
        GameObject scoreText = GameObject.Find("score");
        if (scoreText != null)
        {
            TextMeshProUGUI scoreTextMesh = scoreText.GetComponent<TextMeshProUGUI>();
            if (scoreTextMesh != null)
            {
                games.score.Add(int.Parse(scoreTextMesh.text));
            }
        }
    }

    public void LoadScore()
    {
        if (System.IO.File.Exists(SavePath))
        {
            string json = System.IO.File.ReadAllText(SavePath);
            games = JsonUtility.FromJson<PlayedGame>(json);
        } else
        {
            // Make the file
            
        }
    }

    public void SaveJSON()
    {
        GetScore();
        games.score.Sort();
        string saveData = JsonUtility.ToJson(games);
        System.IO.File.WriteAllText(SavePath, saveData);

        Debug.Log($"Saved score {saveData} to {SavePath}");
    }
}
