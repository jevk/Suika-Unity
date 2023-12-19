using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public GameObject[] today;
    public GameObject[] month;
    public GameObject[] allTime;

    private string SavePath;

    void Start()
    {
        SavePath = Application.persistentDataPath + "/games.json";

        var scores = LoadJSON();

        foreach (var text in today)
        {
            // Get the scores that have the same date as today Score {score: 0, date: yyyy-mm-dd_HH:MM:SS}
            var todayScores = scores.scores.FindAll(x => x.date.Substring(0, 10) == System.DateTime.Now.ToString("yyyy-MM-dd"));
            // Get the index of the current text object
            int index = System.Array.IndexOf(today, text);
            // Assign the score to the text object
            text.GetComponent<TMPro.TextMeshProUGUI>().text = todayScores.Count > index ? todayScores[index].score.ToString() : "0";
        }

        foreach (var text in month)
        {
            // Get the scores that have the same date as today Score {score: 0, date: yyyy-mm-dd_HH:MM:SS}
            var monthScores = scores.scores.FindAll(x => x.date.Substring(0, 7) == System.DateTime.Now.ToString("yyyy-MM"));
            // Get the index of the current text object
            int index = System.Array.IndexOf(month, text);
            // Assign the score to the text object
            text.GetComponent<TMPro.TextMeshProUGUI>().text = monthScores.Count > index ? monthScores[index].score.ToString() : "0";
        }

        foreach (var text in allTime)
        {
            // Get the index of the current text object
            int index = System.Array.IndexOf(allTime, text);
            // Assign the score to the text object
            text.GetComponent<TMPro.TextMeshProUGUI>().text = scores.scores.Count > index ? scores.scores[index].score.ToString() : "0";
        }
    }

    HighscoreData LoadJSON()
    {
        if (System.IO.File.Exists(SavePath))
        {
            string json = System.IO.File.ReadAllText(SavePath);
            return JsonUtility.FromJson<HighscoreData>(json);
        }
        else
        {
            return new HighscoreData
            {
                scores = new List<Score>()
            };
        }
    }
}
