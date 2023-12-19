using UnityEngine;

public class Exit : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // If ESC is pressed, quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject anyFruit = GameObject.FindGameObjectWithTag("Fruit");
            anyFruit.GetComponent<FailCondition>().Failure();
        }
    }
}
