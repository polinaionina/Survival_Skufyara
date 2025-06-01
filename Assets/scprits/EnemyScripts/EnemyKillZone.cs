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
            StartCoroutine(TeleportPlayer(other));
        }
    }

    IEnumerator TeleportPlayer(Collider2D player)
    {
        // Показываем изображение смерти
        if (deathScreenImage != null)
            deathScreenImage.SetActive(true);

        // Отключаем триггер камеры
        CameraTrigger1.cameraLocked = true;

        // Ждём 1 секунду перед телепортацией
        yield return new WaitForSeconds(1.2f);

        // Перемещаем игрока и камеру
        player.transform.position = playerRespawnPoint.position;
        Camera.main.transform.position = new Vector3(
            cameraRespawnPoint.position.x,
            cameraRespawnPoint.position.y,
            Camera.main.transform.position.z);

        // Скрываем изображение смерти
        if (deathScreenImage != null)
            deathScreenImage.SetActive(false);

        // ВАЖНО: Ждем конца кадра (или следующего кадра),
        // чтобы все физические события (как OnTriggerExit2D) успели обработаться,
        // пока cameraLocked все еще true.
        yield return null; // Можно также использовать yield return new WaitForEndOfFrame();

        // Включаем триггер камеры обратно
        CameraTrigger1.cameraLocked = false;
    }
}