using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Для UI

public class KillZone : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public Transform cameraRespawnPoint;
    public GameObject deathScreenImage; // UI изображение смерти

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Получаем компонент movee с объекта игрока
            movee playerMoveeScript = other.GetComponent<movee>();

            if (playerMoveeScript != null)
            {
                StartCoroutine(TeleportPlayer(other, playerMoveeScript));
            }
            else
            {
                Debug.LogError("KillZone: Скрипт 'movee' не найден на объекте игрока!");
            }
        }
    }

    IEnumerator TeleportPlayer(Collider2D playerCollider, movee playerMoveeScript)
    {
        // 1. Блокируем движение игрока используя его собственный скрипт
        playerMoveeScript.LockMovement(true);

        // Показываем изображение смерти
        if (deathScreenImage != null)
            deathScreenImage.SetActive(true);

        // Блокируем триггер камеры
        CameraTrigger1.cameraLocked = true;

        // Ждём 1.2 секунды перед телепортацией
        yield return new WaitForSeconds(1.2f);

        // Перемещаем игрока и камеру
        playerCollider.transform.position = playerRespawnPoint.position;
        Camera.main.transform.position = new Vector3(
            cameraRespawnPoint.position.x,
            cameraRespawnPoint.position.y,
            Camera.main.transform.position.z);

        // Скрываем изображение смерти
        if (deathScreenImage != null)
            deathScreenImage.SetActive(false);

        // Ждем конца кадра (или следующего кадра),
        // чтобы все физические события (как OnTriggerExit2D) успели обработаться,
        // пока cameraLocked все еще true.
        yield return null; // или yield return new WaitForEndOfFrame();

        // Включаем триггер камеры обратно
        CameraTrigger1.cameraLocked = false;

        // 2. Разблокируем движение игрока
        playerMoveeScript.LockMovement(false);
    }
}