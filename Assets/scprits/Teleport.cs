using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public float CameraPosX, CameraPosY;
    public KeyCode teleportKey = KeyCode.F;

    public Sprite newSprite;
    public bool changeSprite = true;
    
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
            
            if (changeSprite && newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            
            if (changeSprite && spriteRenderer != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }

    private void TeleportPlayer()
    {
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
            
            if (Camera.main != null)
            {
                Camera.main.transform.position = new Vector3(
                    CameraPosX,
                    CameraPosY,
                    Camera.main.transform.position.z
                );
            }
        }
    }
}