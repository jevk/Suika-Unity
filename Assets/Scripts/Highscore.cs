using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayedGame
{
    public int score;
}

public class Highscore : MonoBehaviour
{
    public PlayedGame games;
    public string SavePath;

    private void Start()
    {
        SavePath = Application.persistentDataPath + "/games.json";
        Debug.Log(SavePath);
    }

    private PlayedGame GetScore()
    {
        // Get score from "score" text object
        GameObject scoreText = GameObject.Find("score");
        if (scoreText != null)
        {
            TextMeshProUGUI scoreTextMesh = scoreText.GetComponent<TextMeshProUGUI>();
            if (scoreTextMesh != null)
            {
                return new PlayedGame { score = int.Parse(scoreTextMesh.text) };
            }
        }
        return null;
    }

    public void SaveJSON()
    {
        string saveData = JsonUtility.ToJson(GetScore());
        System.IO.File.WriteAllText(SavePath, saveData);
    }
}
