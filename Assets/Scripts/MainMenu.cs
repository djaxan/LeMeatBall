using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Attach this script to a GameObject in the scene with a Canvas setup

    // Start button logic
    public void StartButton()
    {
        // Load the next scene (Scene2 in this case)
        SceneManager.LoadScene("Scene1");
    }

    // Quit button logic
    public void QuitButton()
    {
        // Stop the application
        Application.Quit();

        // For editor testing (won't work in build)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
