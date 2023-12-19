using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    // Scene to switch to
    public string Scene;

    public void GoToScene()
    {
        StartCoroutine(SleepFor(0.1f));
    }

    IEnumerator SleepFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Reset the time scale
        Time.timeScale = 1;
        // Unload the current scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        // Switch to the scene
        SceneManager.LoadScene(Scene);
    }

}
