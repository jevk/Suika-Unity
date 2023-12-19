using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class Score
{
    [SerializeField]
    public int score;

    [SerializeField]
    public string date;
}
[Serializable]
internal class HighscoreData
{
    public List<Score> scores;
}

public class Highscore : MonoBehaviour
{
    internal HighscoreData highscores;
    private string SavePath;

    private void Start()
    {
        SavePath = Application.persistentDataPath + "/games.json";
        // If the file exists load it, otherwise create it with three 0 scores
        if (System.IO.File.Exists(SavePath))
        {
            string json = System.IO.File.ReadAllText(SavePath);
            highscores = JsonUtility.FromJson<HighscoreData>(json);
        }
        else
        {
            highscores = JsonUtility.FromJson<HighscoreData>("{\"scores\":[{\"score\":3082,\"date\":\"2023-12-19 20.38.50\"},{\"score\":1480,\"date\":\"2021-02-14 01:43:25\"},{\"score\":1464,\"date\":\"2023-02-08 14:23:05\"},{\"score\":1454,\"date\":\"2021-08-28 17:01:20\"},{\"score\":1436,\"date\":\"2021-11-05 06:03:10\"},{\"score\":1430,\"date\":\"2023-03-17 02:17:50\"},{\"score\":1412,\"date\":\"2022-11-07 17:47:35\"},{\"score\":1406,\"date\":\"2022-07-12 09:55:55\"},{\"score\":1404,\"date\":\"2022-03-17 13:29:40\"},{\"score\":1402,\"date\":\"2022-09-21 06:37:55\"},{\"score\":1400,\"date\":\"2023-06-05 18:23:05\"},{\"score\":102,\"date\":\"2023-12-18 08:24:30\"},{\"score\":96,\"date\":\"2023-09-01 05:15:55\"},{\"score\":94,\"date\":\"2022-08-04 16:33:10\"},{\"score\":92,\"date\":\"2021-12-22 14:55:55\"},{\"score\":90,\"date\":\"2021-10-05 03:23:10\"},{\"score\":88,\"date\":\"2023-06-23 18:40:25\"},{\"score\":86,\"date\":\"2020-07-02 10:31:40\"},{\"score\":84,\"date\":\"2020-03-17 22:18:55\"},{\"score\":82,\"date\":\"2021-05-31 13:26:10\"},{\"score\":80,\"date\":\"2021-01-13 04:33:25\"},{\"score\":52,\"date\":\"2023-12-19 20.26.55\"},{\"score\":0,\"date\":\"2023-12-19 21.00.12\"}]}");
        }

        UpdateScores();
        SaveJSON(highscores);
    }

    private void SaveJSON(HighscoreData highscores)
    {
        // Remove the scores that have the same score
        highscores.scores = highscores.scores.GroupBy(x => x.score).Select(x => x.First()).ToList();
        // Remove the scores that have the same date
        highscores.scores = highscores.scores.GroupBy(x => x.date).Select(x => x.First()).ToList();
        // Sort the score list in reverse order by score
        highscores.scores = highscores.scores.OrderByDescending(o => o.score).ToList();
        // Save to the file as formatted JSON
        string json = JsonUtility.ToJson(highscores, true);
        System.IO.File.WriteAllText(SavePath, json);
    }

    public void SaveScore()
    {
        // Find "Canvas" object
        GameObject score = GameObject.Find("score");
        if (score != null)
        {
            // Get the "TextMeshProUGUI" component from the "Canvas" object
            TextMeshProUGUI scoreText = score.GetComponent<TextMeshProUGUI>();
            // Get the score from the "TextMeshProUGUI" component and convert it to an integer
            int scoreInt = int.Parse(scoreText.text);

            // Run the "SaveJSON" function from the "Highscore" script
            Score highscore = new()
            {
                score = scoreInt,
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            highscores.scores.Add(highscore);
        }
        // Save the JSON file
        SaveJSON(highscores);
    }

    public void UpdateScores()
    {
        string[] elementNames = { "NumberOne", "NumberTwo", "NumberThree", "NumberOneFinal", "NumberTwoFinal", "NumberThreeFinal" };

        foreach (string elementName in elementNames)
        {
            GameObject scoreObject = GameObject.Find(elementName);
            if (scoreObject != null)
            {
                scoreObject.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI text = scoreObject.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    int index = Array.IndexOf(elementNames, elementName);
                    if (index < highscores.scores.Count)
                    {
                        text.text = highscores.scores[index].score.ToString();
                    }
                    else
                    {
                        text.text = "0";
                    }
                }
            }
        }
    }
}
