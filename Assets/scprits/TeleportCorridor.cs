using UnityEngine;

public class TeleportCorridor : MonoBehaviour
{
    public Vector3 cameraInsideCoordinates;
    public Vector3 cameraOutsideCoordinates;

    private Camera mainCamera;

    public static bool cameraLocked = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cameraLocked)
        {
            SetCameraPosition(cameraInsideCoordinates);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cameraLocked)
        {
            SetCameraPosition(cameraOutsideCoordinates);
        }
    }

    void SetCameraPosition(Vector3 targetPosition)
    {
        mainCamera.transform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            mainCamera.transform.position.z
        );
    }
}