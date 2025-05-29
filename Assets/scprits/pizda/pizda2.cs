using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pizda2 : MonoBehaviour
{
    [Tooltip("Name of the scene to load")]
    public string sceneName;

    [Tooltip("UI Button to trigger the scene loading")]
    public Button loadButton;

    void Start()
    {
        // Ensure that sceneName is not null or empty
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("SceneLoader: sceneName is not set! Please specify the name of the scene to load in the Inspector.");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        // Ensure that loadButton is assigned
        if (loadButton == null)
        {
            Debug.LogError("SceneLoader: loadButton is not assigned! Please assign the UI Button in the Inspector.");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        // Add a listener to the button's onClick event
        loadButton.onClick.AddListener(LoadScene);
    }

    // Method to load the specified scene
    public void LoadScene()
    {
        Debug.Log("LoadScene() was called!"); // <-- Add this line
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}