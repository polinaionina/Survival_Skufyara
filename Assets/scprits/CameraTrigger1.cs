using UnityEngine;

public class CameraTrigger1 : MonoBehaviour
{
    public Transform cameraInsidePoint;    // Позиция при входе в триггер
    public Transform cameraOutsidePoint;   // Позиция при выходе из триггера
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cameraInsidePoint != null)
        {
            SetCameraPosition(cameraInsidePoint);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cameraOutsidePoint != null)
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