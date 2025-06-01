using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    [Tooltip("Массив объектов, которые должны быть выключены")]
    public GameObject[] dependentObjects;

    [Tooltip("Объект, который нужно активировать после выключения всех зависимых объектов")]
    public GameObject targetObject;

    private bool allDeactivated = false;

    void Start()
    {
        // Проверяем, что массив зависимых объектов не пустой
        if (dependentObjects == null || dependentObjects.Length == 0)
        {
            Debug.LogWarning("Массив dependentObjects пуст! Объект " + targetObject.name + " никогда не будет активирован.");
            return;
        }

        // Проверяем, что целевой объект указан
        if (targetObject == null)
        {
            Debug.LogError("Целевой объект не указан! Скрипт DependencyManager не будет работать.");
            return;
        }

        // Изначально выключаем целевой объект
        targetObject.SetActive(false);
    }

    void Update()
    {
        // Проверяем, все ли зависимые объекты выключены
        allDeactivated = true;
        foreach (GameObject dependentObject in dependentObjects)
        {
            if (dependentObject != null && dependentObject.activeSelf)
            {
                allDeactivated = false;
                break; // Если хотя бы один объект включен, выходим из цикла
            }
        }

        // Если все зависимые объекты выключены и целевой объект еще не активирован, активируем его
        if (allDeactivated && !targetObject.activeSelf)
        {
            targetObject.SetActive(true);
            Debug.Log("Объект " + targetObject.name + " активирован, т.к. все зависимые объекты выключены.");
        }
    }
}