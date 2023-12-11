using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    // Array of box prefabs to spawn as enemies
    public GameObject[] box;

    // Powerup prefab to spawn
    public GameObject powerup;

    // Powerup prefab to spawn
    public GameObject Powerup1;

    // Z-axis position for enemy spawn
    private float zEnemySpawn = 12.0f;

    // Range for random X-axis position for spawning enemies
    private float xSpawnRange = 16.0f;

    // Range for random Z-axis position for spawning powerups
    private float zPowerupRange = 5.0f;

    // Fixed Y-axis position for spawning objects
    private float ySpawn = 0.0f;

    // Time interval for spawning powerups
    private float powerupSpawnTime = 5.0f;

    // Time interval for spawning enemies
    private float enemySpawnTime = 1.0f;

    // Delay before starting to spawn objects
    private float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start repeating the invocation of SpawnRandomEnemy with a delay and a specified time interval
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);

        // Start repeating the invocation of SpawnPowerup with a delay and a specified time interval
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);

        // Check if the current scene is Scene2, then start spawning Powerup1
        if (SceneManager.GetActiveScene().name == "Crazy2Game")
        {
            InvokeRepeating("SpawnPowerup1", startDelay, powerupSpawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update logic can be added here if needed
    }

    // Method to spawn a random enemy
    void SpawnRandomEnemy()
    {
        // Generate a random X-axis position within the specified range
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Generate a random index to select a box prefab from the array
        int randomIndex = Random.Range(0, box.Length);

        // Create a spawn position based on the random X-axis position, fixed Y-axis position, and Z-axis position for enemies
        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        // Instantiate a randomly selected box prefab at the generated spawn position
        Instantiate(box[randomIndex], spawnPos, box[randomIndex].gameObject.transform.rotation);
    }

    // Method to spawn a powerup
    void SpawnPowerup()
    {
        // Generate a random X-axis position within the specified range
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        // Generate a random Z-axis position within the specified range
        float randomZ = Random.Range(-zPowerupRange, zPowerupRange);

        // Create a spawn position based on the random X and Z-axis positions, and fixed Y-axis position for powerups
        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

        // Instantiate the powerup prefab at the generated spawn position
        Instantiate(powerup, spawnPos, powerup.gameObject.transform.rotation);

    }
    // Method to spawn a powerup
void SpawnPowerup1()
{
    // Generate a random X-axis position within the specified range
    float randomX = Random.Range(-xSpawnRange, xSpawnRange);

    // Generate a random Z-axis position within the specified range
    float randomZ = Random.Range(-zPowerupRange, zPowerupRange);

    // Create a spawn position based on the random X and Z-axis positions, and fixed Y-axis position for powerups
    Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

    // Instantiate the Powerup1 prefab at the generated spawn position
    Instantiate(Powerup1, spawnPos, Powerup1.transform.rotation);

    
}

}