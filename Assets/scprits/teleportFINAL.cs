using UnityEngine;
using System.Collections;

public class teleportFINAL : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform playerTeleportTarget; // Куда телепортировать игрока
    public Transform cameraTeleportTarget; // Куда переместить камеру
    public Sprite newSprite;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerIsInside = false;
    public Transform player;

    [Header("Activation Settings")]
    public bool isActive = false; // По умолчанию телепорт активен. Установите false, если он должен быть активирован позже.

    [Header("Timing Settings")]
    public float delayAfterTeleport = 0.2f; 

    [Header("Player Tag")]
    public string playerTag = "Player"; // Тег объекта игрока

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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {   
        if (isActive && playerIsInside && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TeleportSequence(player));
        }
    }
                

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player")){
            spriteRenderer.sprite = newSprite;
            playerIsInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = originalSprite;
            playerIsInside = false;
        }
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