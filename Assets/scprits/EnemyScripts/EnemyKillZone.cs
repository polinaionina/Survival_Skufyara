using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public Transform cameraRespawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = playerRespawnPoint.position;
            Camera.main.transform.position = new Vector3(
                cameraRespawnPoint.position.x,
                cameraRespawnPoint.position.y,
                Camera.main.transform.position.z);
        }
    }
}
