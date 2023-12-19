using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FailCondition : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    private void CaptureScreenshot()
    {
        StartCoroutine(TakeScreenShot((result) =>
        {
            // Assign the texture to the "GameScreenshot" UI Image object
            GameObject gameScreenshot = GameObject.Find("GameScreenshot");

            if (gameScreenshot != null)
            {
                if (gameScreenshot.TryGetComponent<Image>(out var gameScreenshotImage))
                {
                    gameScreenshotImage.sprite = Sprite.Create(result, new Rect(0, 0, result.width, result.height), new Vector2(0, 0));
                }
            }
        }));
    }

    public void Failure()
    {
        SaveScore();
        CaptureScreenshot();

        // Stop the spawner
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null)
        {
            if (spawner.TryGetComponent<FruitSpawner>(out var fruitSpawner))
            {
                fruitSpawner.isRunning = false;
            }
        }

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
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        // Freeze the game
        foreach (GameObject fruit in fruits)
        {
            StartCoroutine(DeleteFruitWithDelay(fruit));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fruit")
        {
            Failure();
        }
    }

    IEnumerator TakeScreenShot(System.Action<Texture2D> callback)
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        callback(texture);
    }

    IEnumerator DeleteFruitWithDelay(GameObject fruit)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(fruit);
    }

    void SaveScore()
    {
        GameObject score = GameObject.Find("score");
        // Use GetComponentInParent to find the Highscore component, considering hierarchy
        Highscore highscoreComponent = score.GetComponentInParent<Highscore>();
        if (highscoreComponent != null)
        {
            highscoreComponent.SaveScore();
        }
    }
}
