using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesManager : MonoBehaviour
{
    // TextMeshProUGUI for displaying the player's lives
    public TextMeshProUGUI livesText;

    static public int TotalLives;
    // variable for livesCount
    public static int livesCount;

    // Start is called before the first frame update
    void Start()
    {
        TotalLives = 3;
        livesCount = TotalLives;
        UpdateLivesText();
    }

    public void UpdateLivesText()
    {
        livesText.text = "Lives: " + livesCount.ToString();
        Debug.Log("updating text");
    }

     // Update is called once per frame
    void Update()
    {
       UpdateLivesText();
    }

    public void PlayerHit()
    {
        Debug.Log("PlayerHit is running OK");
        // Check if the game is already over
      
        // Deduct one life
        livesCount--;
        Debug.Log("livesCount--;");

        // Update the lives display
        UpdateLivesText();

        // Check if the game is over
        if (livesCount <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Game over logic
        Debug.Log("Game Over");
    }
}
