using UnityEngine;
using System.Collections;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget;
    public Sprite newSprite;
    public float CameraPosX;
    public float CameraPosY;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    public bool NeededAction;
    private bool playerIsInside = false;
    private GameObject player;
    public bool isActive;
    public float teleportDelay = 0f; 

    private bool isTeleporting = false; 

    private MusicManager musicManager;

    public void SetActive() => isActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        //musicManager = FindObjectOfType<MusicManager>();
    }

    void Update()
    {   
        if (isActive)
        {
            if (playerIsInside && (!NeededAction || (NeededAction && Input.GetKeyDown(KeyCode.F))))
            {
                if (!isTeleporting)
                {
                    isTeleporting = true;
                    if (teleportDelay > 0f)
                        StartCoroutine(TeleportWithDelay());
                    else
                        Teleport();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (isActive)
        {
            if (other.CompareTag("Player"))
            {
                playerIsInside = true;
                player = other.gameObject;
                if (NeededAction && newSprite != null)
                    spriteRenderer.sprite = newSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            player = null;
            if (NeededAction)
                spriteRenderer.sprite = originalSprite;
            isTeleporting = false; 
        }
    }

    private IEnumerator TeleportWithDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        Teleport();
    }

    private void Teleport()
    {
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;

            Camera.main.transform.position = new Vector3(
                CameraPosX,
                CameraPosY,
                Camera.main.transform.position.z
            );

            if (musicManager != null)
            {
                musicManager.PlayGamePlayMusic();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        if (musicManager != null)
        {
            musicManager.PlayMainMenuMusic();
        }
    }
}