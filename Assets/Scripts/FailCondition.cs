using TMPro;
using UnityEngine;

public class FailCondition : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fruit")
        {
            // Capture a screenshot of the game and save it to a variable
            Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

            // Slide the "LosingScreen" object into view
            GameObject losingScreen = GameObject.Find("LosingScreen");
            GameObject bg = GameObject.Find("Background");
            if (losingScreen != null && bg != null)
            {
                Animator bgAnimator = bg.GetComponent<Animator>();
                Animator losingScreenAnimator = losingScreen.GetComponent<Animator>();
                if (losingScreenAnimator != null && bgAnimator != null)
                {
                    losingScreenAnimator.SetBool("Lost", true);
                    bgAnimator.SetBool("Lost", true);
                }
            }

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

            // Find "Canvas" object
            GameObject score = GameObject.Find("score");
            if (score != null)
            {
                // Run the "SaveJSON" function from the "Highscore" script
                Highscore highscore = score.GetComponent<Highscore>();
                if (highscore != null)
                {
                    highscore.SaveJSON();
                    Debug.Log("Saved score");

                    highscore.UpdateScores();
                }
            }

        }
    }
}
