using UnityEngine;
using System.Collections;

public class teleportFINAL : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform playerTeleportTarget; // Куда телепортировать игрока
    public Transform cameraTeleportTarget; // Куда переместить камеру

    [Header("Activation Settings")]
    public bool isActive = true; // По умолчанию телепорт активен. Установите false, если он должен быть активирован позже.

    [Header("Player Tag")]
    public string playerTag = "Player"; // Тег объекта игрока

    [Header("Timing Settings")]
    public float delayAfterTeleport = 0.2f; // Задержка перед разблокировкой камеры

    // Публичный метод для активации телепорта из другого скрипта
    public void ActivateTeleport()
    {
        isActive = true;
        Debug.Log(gameObject.name + " teleport activated.");
    }

    // Публичный метод для деактивации телепорта из другого скрипта
    public void DeactivateTeleport()
    {
        isActive = false;
        Debug.Log(gameObject.name + " teleport deactivated.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, активен ли телепорт и вошел ли игрок
        if (!isActive)
        {
            // Если телепорт не активен, ничего не делаем
            return;
        }

        if (!other.CompareTag(playerTag))
        {
            // Если это не игрок, ничего не делаем
            return;
        }

        // Проверяем, назначены ли цели
        if (playerTeleportTarget == null)
        {
            Debug.LogError("TeleportWithSimilarCameraLogic: Player Teleport Target не назначен на объекте " + gameObject.name, this);
            return;
        }
        if (cameraTeleportTarget == null)
        {
            Debug.LogError("TeleportWithSimilarCameraLogic: Camera Teleport Target не назначен на объекте " + gameObject.name, this);
            return;
        }
        
        // Если все проверки пройдены, запускаем корутину телепортации
        StartCoroutine(TeleportSequence(other.transform));
    }

    IEnumerator TeleportSequence(Transform playerTransform)
    {
        Debug.Log("TeleportSequence started for " + playerTransform.name);

        // 1. Сообщаем CameraTrigger1, чтобы он временно не управлял камерой
        // Убедитесь, что скрипт CameraTrigger1 существует и переменная cameraLocked доступна
        if (FindObjectOfType<CameraTrigger1>() != null) // Проверка, существует ли такой объект, чтобы избежать ошибки, если нет
        {
             CameraTrigger1.cameraLocked = true;
             Debug.Log("CameraTrigger1.cameraLocked set to true");
        }
        else
        {
            Debug.LogWarning("CameraTrigger1 script not found in scene. Camera locking might not work as expected.");
        }


        // 2. Перемещаем игрока
        playerTransform.position = playerTeleportTarget.position;
        Debug.Log(playerTransform.name + " teleported to " + playerTeleportTarget.position);

        // 3. Перемещаем основную камеру
        if (Camera.main != null)
        {
            Vector3 newCameraPosition = new Vector3(
                cameraTeleportTarget.position.x,
                cameraTeleportTarget.position.y,
                Camera.main.transform.position.z // Сохраняем Z-координату камеры
            );
            Camera.main.transform.position = newCameraPosition;
            Debug.Log("Main Camera teleported to " + newCameraPosition);
        }
        else
        {
            Debug.LogError("TeleportWithSimilarCameraLogic: Main Camera не найдена в сцене!", this);
        }

        // 4. Немного ждём (аналогично вашему KillZone)
        // Эта задержка может помочь избежать конфликтов, если другие скрипты
        // реагируют на изменение позиции игрока в том же кадре или на OnTriggerExit.
        if (delayAfterTeleport > 0)
        {
            yield return new WaitForSeconds(delayAfterTeleport);
            Debug.Log("Waited for " + delayAfterTeleport + " seconds.");
        }


        // 5. Возвращаем управление камерой CameraTrigger1
        if (FindObjectOfType<CameraTrigger1>() != null)
        {
            CameraTrigger1.cameraLocked = false;
            Debug.Log("CameraTrigger1.cameraLocked set to false");
        }
        
        Debug.Log("TeleportSequence finished for " + playerTransform.name);
    }
}