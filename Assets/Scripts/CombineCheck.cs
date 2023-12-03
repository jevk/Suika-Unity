using System.Collections;
using UnityEngine;

public class CombineCheck : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    private bool isMerging = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMerging && collision.gameObject.name == transform.gameObject.name)
        {
            isMerging = true;

            // Determine the master fruit based on their unique identifiers
            CombineCheck masterFruit = DetermineMasterFruit(collision.gameObject.GetComponent<CombineCheck>());

            if (masterFruit == this)
            {
                StartCoroutine(masterFruit.HandleCollision(collision));
            }
        }
    }

    private CombineCheck DetermineMasterFruit(CombineCheck otherCombineCheck)
    {
        // Compare unique criteria to determine the master fruit
        // In this example, we use the instance ID as a simple criterion
        return (GetInstanceID() > otherCombineCheck.GetInstanceID()) ? this : otherCombineCheck;
    }

    public IEnumerator HandleCollision(Collision2D collision)
    {
        int fruitIndex = System.Array.FindIndex(this.fruitPrefabs, fruit => fruit.name == transform.gameObject.name);

        if (fruitIndex < this.fruitPrefabs.Length - 1)
        {
            Destroy(collision.gameObject);
            GameObject fruit = Instantiate(this.fruitPrefabs[fruitIndex + 1], transform.position, Quaternion.identity);
            fruit.name = this.fruitPrefabs[fruitIndex + 1].name;

            // Make sure the rigidbody is not kinematic
            fruit.GetComponent<Rigidbody2D>().isKinematic = false;

            // Delay the destruction of the current fruit
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
            Destroy(gameObject);

            // Play the sound effect "pop.wav" in the "Audio Source" component in the "Spawner" gameobject
            GameObject.Find("Spawner").GetComponent<AudioSource>().Play();
        }

        isMerging = false;
    }
}
