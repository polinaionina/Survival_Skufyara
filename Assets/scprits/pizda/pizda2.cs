using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pizda2 : MonoBehaviour
{
    public string sceneName;

    public Button loadButton;

    void Start()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("SceneLoader: sceneName is not set! Please specify the name of the scene to load in the Inspector.");
            enabled = false;
            return;
        }

        if (loadButton == null)
        {
            Debug.LogError("SceneLoader: loadButton is not assigned! Please assign the UI Button in the Inspector.");
            enabled = false;
            return;
        }

        loadButton.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        Debug.Log("LoadScene() was called!");
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}