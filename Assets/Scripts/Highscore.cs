using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Game
{
    int score;
    string date;
}
public class PlayedGame
{
    public List<int> score;
    public List<int> randomShit;
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
        games.score = new List<int>();
        games.randomShit = new List<int>
        {
            1
        };
        LoadScore();
        UpdateScores();
        SaveJSON();
    }
    public void UpdateScores()
    {

        // Set the texts
        GameObject NumberOne = GameObject.Find("NumberOne");
        GameObject NumberTwo = GameObject.Find("NumberTwo");
        GameObject NumberThree = GameObject.Find("NumberThree");
        if (NumberOne != null && NumberTwo != null && NumberThree != null)
        {
            TextMeshProUGUI NumberOneText = NumberOne.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI NumberTwoText = NumberTwo.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI NumberThreeText = NumberThree.GetComponent<TextMeshProUGUI>();
            if (NumberOneText != null && NumberTwoText != null && NumberThreeText != null)
            {
                NumberOneText.text = games.score[0].ToString();
                NumberTwoText.text = games.score[1].ToString();
                NumberThreeText.text = games.score[2].ToString();
            }
        }

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
            System.IO.File.WriteAllText(SavePath, "{\"score\":[0,0,0]}");
        }
    }

    public void SaveJSON()
    {
        GetScore();
        // Sort the score list in reverse order
        games.score.Sort((a, b) => b.CompareTo(a));
        string saveData = JsonUtility.ToJson(games);
        System.IO.File.WriteAllText(SavePath, saveData);

        Debug.Log($"Saved score {saveData} to {SavePath}");
    }
}
