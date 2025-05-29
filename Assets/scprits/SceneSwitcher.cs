using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Название сцены, на которую нужно перейти
    public string sceneName;

    // Этот метод можно вызвать через UI Button (OnClick)
    public void ChangeScene()
    {
        // Проверяем, указано ли название сцены
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Загружаем указанную сцену
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not specified!");
        }
    }
}