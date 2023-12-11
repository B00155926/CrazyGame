using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RandomBoxes : MonoBehaviour
{
   // public TextMeshProUGUI scoreText;
    private ScoreManager scoreManager;

    // public TextMeshProUGUI livesText;
    private LivesManager livesManager;

    // TextMeshProUGUI for displaying the game over message
    public TextMeshProUGUI gameOverText;

    // Flag indicating whether the game is active
    public bool isGameActive;

    // Speed at which the object moves forward
    public float speed = 10.0f;

    // Z-axis position at which the object is destroyed
    private float zDestroy = -10.0f;

    // Rigidbody component of the object
    public Rigidbody objectRb;

    // Powerup prefab to spawn
    public GameObject powerup;

    // Powerup1 prefab to spawn in Scene2
    public GameObject powerup1;

    // Maximum number of Powerup1 objects to spawn
    public int maxPowerup1Count = 5;

    // Flag indicating whether Powerup1 objects can still be spawned
    private bool canSpawnPowerup1;

    // Start is called before the first frame update
    void Start()
    {
       
        // Get the Rigidbody component
        objectRb = GetComponent<Rigidbody>();

        // Set the game as active
        isGameActive = true;
       
        // Check the current scene
    Scene currentScene = SceneManager.GetActiveScene();

    // Set different speeds based on the scene
    if (currentScene.name == "Crazy2Game")
    {
        // Set a faster speed for Crazy2Game
        speed = 40.0f;
    }
    else
    {
        // Set the default speed for other scenes
        speed = 10.0f;
    }

        // Start the coroutine for spawning targets
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object forward based on speed
        objectRb.AddForce(Vector3.forward * -speed);

        // Check if the object's position is below the destroy threshold, then destroy it
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }

    }

    // Coroutine for spawning targets
IEnumerator SpawnTarget()
{
    while (isGameActive)
    {
        // Check the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Spawn Powerup1 only in Scene2
        if (currentScene.name == "Crazy2Game")
        {
            // Spawn Powerup1 only if the maximum count has not been reached
            if (GetPowerup1Count() < maxPowerup1Count)
            {
                SpawnPowerup1();
            }
            else
            {
                // Disable further spawning of Powerup1
                canSpawnPowerup1 = false;
            }
        }

        // Yield control back to the game loop
        yield return new WaitForSeconds(1.0f); 
    }
}

    private int GetPowerup1Count()
    {
        // Count the number of Powerup1 objects in the scene
        return GameObject.FindGameObjectsWithTag("Powerup1").Length;
    }

    
    // Public method to handle game over
    public void GameOver()
{
    // Display the game over text in the UI
    gameOverText.gameObject.SetActive(true);

    // Set the game as inactive
    isGameActive = false;
    
    ScoreManager.scoreCount  = 0;
    LivesManager.livesCount = 0;
    

    // Start the coroutine to load the main menu scene after a delay
    StartCoroutine(LoadMainMenuAfterDelay(2.0f));
}


    // Coroutine to load the main menu scene after a delay
    IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the main menu scene
        SceneManager.LoadScene("Crazy0Game"); 
    }

    // Method to spawn Powerup1
    void SpawnPowerup1()
    {
        // Generate a random X-axis position within the specified range
        float randomX = Random.Range(-10.0f, 10.0f);

        // Generate a random Z-axis position within the specified range
        float randomZ = Random.Range(0.0f, 10.0f);

        // Create a spawn position based on the random X and Z-axis positions, and fixed Y-axis position for powerups
        Vector3 spawnPos = new Vector3(randomX, 0.0f, randomZ);

        // Instantiate the Powerup1 prefab at the generated spawn position
        Instantiate(powerup1, spawnPos, powerup1.gameObject.transform.rotation);
    }
}