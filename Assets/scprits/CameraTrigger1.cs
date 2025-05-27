using UnityEngine;

public class CameraTrigger1 : MonoBehaviour
{
    public Transform cameraInsidePoint;
    public Transform cameraOutsidePoint;

    private Camera mainCamera;

    public static bool cameraLocked = false; // Глобальный флаг

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cameraInsidePoint != null && !cameraLocked)
        {
            SetCameraPosition(cameraInsidePoint);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cameraOutsidePoint != null && !cameraLocked)
        {
            SetCameraPosition(cameraOutsidePoint);
        }
    }

    void SetCameraPosition(Transform target)
    {
        mainCamera.transform.position = new Vector3(
            target.position.x,
            target.position.y,
            mainCamera.transform.position.z
        );
    }
}