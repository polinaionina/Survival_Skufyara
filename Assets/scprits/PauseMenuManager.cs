using UnityEngine;
using UnityEngine.UI; // Необходимо для работы с компонентом Image
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Ссылки на UI элементы")]
    // Перетащите сюда ваш GameObject, который является корнем меню паузы 
    // (например, PauseMenuRootImage, у которого Image компонент изначально выключен)
    public GameObject pauseMenuRootObject; 

    [Header("Настройки сцен")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Ссылка на скрипт игрока")]
    public movee playerMoveScript;

    private bool isPaused = false;
    private Image pauseMenuImageComponent; // Для кэширования компонента Image

    void Start()
    {
        if (pauseMenuRootObject != null)
        {
            // Пытаемся получить компонент Image с корневого объекта меню паузы
            pauseMenuImageComponent = pauseMenuRootObject.GetComponent<Image>();

            if (pauseMenuImageComponent == null)
            {
                Debug.LogWarning("PauseMenuManager: Назначенный 'pauseMenuRootObject' не имеет компонента 'Image'. " +
                                 "Если это контейнер с несколькими Image, убедитесь, что нужные включаются/выключаются.");
            }
            
            // Изначально весь объект меню паузы деактивирован.
            // Ваш Image компонент на нем и так выключен в инспекторе.
            pauseMenuRootObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Корневой объект меню паузы (pauseMenuRootObject) не назначен в инспекторе для PauseMenuManager!");
        }

        // Поиск скрипта игрока, если не назначен
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
            // 1. Активируем сам GameObject меню паузы
            pauseMenuRootObject.SetActive(true);
            // 2. Включаем компонент Image, чтобы картинка стала видимой
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
            // 1. Выключаем компонент Image (возвращаем к вашему исходному состоянию в инспекторе)
            if (pauseMenuImageComponent != null)
            {
                pauseMenuImageComponent.enabled = false;
            }
            // 2. Деактивируем сам GameObject меню паузы
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