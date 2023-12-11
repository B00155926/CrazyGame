using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCode : MonoBehaviour
{
    // The URL to open when the loadUrl() method is called
    public string URL = "https://github.com/B00155926/CrazyGame";

    // Method to switch to Scene1
    public void GoToScene1()
    {
        // Load the scene named "Crazy1Game"
        SceneManager.LoadScene("Crazy1Game");
    }

    // Method to switch to Scene2
    public void GoToScene2()
    {
        // Load the scene named "Crazy2Game"
        SceneManager.LoadScene("Crazy2Game");
    }

    // Method to open the specified URL
    public void loadUrl()
    {
        // Open the specified URL using the default application for handling URLs
        Application.OpenURL(URL);
    }
}
