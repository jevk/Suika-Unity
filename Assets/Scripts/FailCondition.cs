using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FailCondition : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fruit")
        {
            // Count every fruit in the scene and destroy them while adding to the score with the formula (index + 1) * 2
            // Destroy the fruits with a 0.1 second delay from each other
            GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
            foreach (GameObject fruit in fruits)
            {
                int fruitIndex = System.Array.FindIndex(this.fruitPrefabs, fruitPrefab => fruitPrefab.name == fruit.name);
                GameObject scoreText = GameObject.Find("score");
                if (scoreText != null)
                {
                    TextMeshProUGUI scoreTextMesh = scoreText.GetComponent<TextMeshProUGUI>();
                    if (scoreTextMesh != null)
                    {
                        scoreTextMesh.text = (int.Parse(scoreTextMesh.text) + (fruitIndex + 1) * 2).ToString();
                    }
                }
                Destroy(fruit, 0.1f);
            }


        }
    }
}
