using UnityEngine;
using System.Collections;

public class teleportFINAL : MonoBehaviour
{
    public Transform playerTeleportTarget;
    public Transform cameraTeleportTarget;
    public Sprite newSprite;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerIsInside = false;
    public Transform player;

    public bool isActive = false;

    public float delayAfterTeleport = 0.2f;

    public string playerTag = "Player";

    public void ActivateTeleport()
    {
        isActive = true;
        Debug.Log(gameObject.name + " teleport activated.");
    }

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

        if (FindObjectOfType<CameraTrigger1>() != null)
        {
             CameraTrigger1.cameraLocked = true;
             Debug.Log("CameraTrigger1.cameraLocked set to true");
        }
        else
        {
            Debug.LogWarning("CameraTrigger1 script not found in scene. Camera locking might not work as expected.");
        }

        playerTransform.position = playerTeleportTarget.position;
        Debug.Log(playerTransform.name + " teleported to " + playerTeleportTarget.position);
          if (Camera.main != null)
        {
            Vector3 newCameraPosition = new Vector3(
                cameraTeleportTarget.position.x,
                cameraTeleportTarget.position.y,
                Camera.main.transform.position.z
            );
            Camera.main.transform.position = newCameraPosition;
            Debug.Log("Main Camera teleported to " + newCameraPosition);
        }
        else
        {
            Debug.LogError("TeleportWithSimilarCameraLogic: Main Camera не найдена в сцене!", this);
        }

        if (delayAfterTeleport > 0)
        {
            yield return new WaitForSeconds(delayAfterTeleport);
            Debug.Log("Waited for " + delayAfterTeleport + " seconds.");
        }

        if (FindObjectOfType<CameraTrigger1>() != null)
        {
            CameraTrigger1.cameraLocked = false;
            Debug.Log("CameraTrigger1.cameraLocked set to false");
        }
        
        Debug.Log("TeleportSequence finished for " + playerTransform.name);
    }
}