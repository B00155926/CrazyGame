using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    // TextMeshProUGUI for displaying the player's score
    public TextMeshProUGUI scoreText;
    // variable for scoreCount
    public static int scoreCount;

    // Start is called before the first frame update
    void Start()
    {
      //initialise score  
      scoreCount = 0;  
    }

    // Update is called once per frame
    void Update()
    {
       scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}
