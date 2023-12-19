using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public bool isRunning = true;
    int fruitNumber = 0;
    bool fruitExists = false;
    public GameObject[] fruitPrefabs;
    string currentFruitName = "ReleasedFruit";

    // Cooldown timer
    float previousSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fruitExists = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the gameobject's x position according to the mouse's x position
        // limit the x position to the LWall and RWall's x positions
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < GameObject.Find("LBOUND").transform.position.x)
            mousePos.x = GameObject.Find("LBOUND").transform.position.x;
        if (mousePos.x > GameObject.Find("RBOUND").transform.position.x)
            mousePos.x = GameObject.Find("RBOUND").transform.position.x;
        if (isRunning) transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);

        // If the cooldown is over and fruit does not exist, spawn a random fruit prefab at the "Spawner" position from "fruitPrefabs" as "Fruit" (Cherry, Strawberry, Grapes, Dekopon, or Orange)
        // If a fruit exists already, move the fruit to the "Spawner" position

        if (Time.time - previousSpawnTime >= 1f && !fruitExists && isRunning)
        {
            // spawn a random fruit prefab at the "Spawner" position from "fruitPrefabs" as "Fruit" (Cherry, Strawberry, Grapes, Dekopon, or Orange
            int fruitIndex = Random.Range(0, fruitPrefabs.Length);
            if (fruitNumber < 5)
            {
                fruitIndex = Mathf.Min(fruitIndex, fruitNumber);
                if (fruitIndex >= fruitNumber) fruitNumber++;
            }
            currentFruitName = fruitPrefabs[fruitIndex].name;
            GameObject fruit = Instantiate(fruitPrefabs[fruitIndex], transform.position, Quaternion.identity);
            // disable the fruit's collider
            fruit.name = "Fruit";
            fruit.GetComponent<Rigidbody2D>().isKinematic = true;
            fruitExists = true;

            // Reset the cooldown timer
        }
        
        if (fruitExists)
        {
            // Move the fruit to the "Spawner" position
            GameObject fruit = GameObject.Find("Fruit");
            if (fruit != null)
            {
                fruit.transform.position = transform.position;
            }
        }

        // On mouse click, release the fruit and name it to the current fruit name
        if (Input.GetMouseButtonDown(0))
        {
            GameObject fruit = GameObject.Find("Fruit");
            if (fruit != null)
            {
                fruit.name = currentFruitName;
                fruit.GetComponent<Rigidbody2D>().isKinematic = false;
                fruit.layer = 6;
                fruitExists = false;
                previousSpawnTime = Time.time;
            }
        }
    }
}
