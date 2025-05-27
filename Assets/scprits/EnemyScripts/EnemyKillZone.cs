using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public Transform cameraRespawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer(other));
        }
    }

    IEnumerator TeleportPlayer(Collider2D player)
    {
        // Отключаем триггер камеры
        CameraTrigger1.cameraLocked = true;

        // Перемещаем игрока и камеру
        player.transform.position = playerRespawnPoint.position;
        Camera.main.transform.position = new Vector3(
            cameraRespawnPoint.position.x,
            cameraRespawnPoint.position.y,
            Camera.main.transform.position.z);

        // Немного ждём (чтобы все OnTriggerExit отработали)
        yield return new WaitForSeconds(0.2f);

        // Включаем триггер камеры обратно
        CameraTrigger1.cameraLocked = false;
    }
}