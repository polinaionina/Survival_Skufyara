using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuRootObject;

    public string mainMenuSceneName = "MainMenu";

    public movee playerMoveScript;

    private bool isPaused = false;
    private Image pauseMenuImageComponent;

    void Start()
    {
        if (pauseMenuRootObject != null)
        {
            pauseMenuImageComponent = pauseMenuRootObject.GetComponent<Image>();

            if (pauseMenuImageComponent == null)
            {
                Debug.LogWarning("PauseMenuManager: Назначенный 'pauseMenuRootObject' не имеет компонента 'Image'. " +
                                 "Если это контейнер с несколькими Image, убедитесь, что нужные включаются/выключаются.");
            }
            
            pauseMenuRootObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Корневой объект меню паузы (pauseMenuRootObject) не назначен в инспекторе для PauseMenuManager!");
        }

        if (playerMoveScript == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerMoveScript = playerObject.GetComponent<movee>();
            }

            if (playerMoveScript == null)
            {
                Debug.LogWarning("Скрипт 'movee' не назначен и не найден на объекте с тегом 'Player'. " +
                                 "Управление персонажем не будет блокироваться/разблокироваться.");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenuRootObject != null)
        {
            pauseMenuRootObject.SetActive(true);
            if (pauseMenuImageComponent != null)
            {
                pauseMenuImageComponent.enabled = true;
            }
        }
        Time.timeScale = 0f;
        isPaused = true;

        if (playerMoveScript != null)
        {
            playerMoveScript.LockMovement(true);
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuRootObject != null)
        {
            if (pauseMenuImageComponent != null)
            {
                pauseMenuImageComponent.enabled = false;
            }
            pauseMenuRootObject.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;

        if (playerMoveScript != null)
        {
            playerMoveScript.LockMovement(false);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}