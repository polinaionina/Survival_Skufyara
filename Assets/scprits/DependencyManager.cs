using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public GameObject[] dependentObjects;
    public GameObject targetObject;

    private bool allDeactivated = false;

    void Start()
    {
        if (dependentObjects == null || dependentObjects.Length == 0)
        {
            Debug.LogWarning("Массив dependentObjects пуст! Объект " + targetObject.name + " никогда не будет активирован.");
            return;
        }

        if (targetObject == null)
        {
            Debug.LogError("Целевой объект не указан! Скрипт DependencyManager не будет работать.");
            return;
        }

        targetObject.SetActive(false);
    }

    void Update()
    {
        allDeactivated = true;
        foreach (GameObject dependentObject in dependentObjects)
        {
            if (dependentObject != null && dependentObject.activeSelf)
            {
                allDeactivated = false;
                break;
            }
        }

        if (allDeactivated && !targetObject.activeSelf)
        {
            targetObject.SetActive(true);
            Debug.Log("Объект " + targetObject.name + " активирован, т.к. все зависимые объекты выключены.");
        }
    }
}