using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillZone : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public Transform cameraRespawnPoint;
    public GameObject deathScreenImage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
        playerMoveeScript.LockMovement(true);

        if (deathScreenImage != null)
            deathScreenImage.SetActive(true);

        CameraTrigger1.cameraLocked = true;

        yield return new WaitForSeconds(1.2f);

        playerCollider.transform.position = playerRespawnPoint.position;
        Camera.main.transform.position = new Vector3(
            cameraRespawnPoint.position.x,
            cameraRespawnPoint.position.y,
            Camera.main.transform.position.z);

        if (deathScreenImage != null)
            deathScreenImage.SetActive(false);

        yield return null;

        CameraTrigger1.cameraLocked = false;

        playerMoveeScript.LockMovement(false);
    }
}