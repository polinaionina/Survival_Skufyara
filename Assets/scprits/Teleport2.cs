using UnityEngine;
using System.Collections;

public class Teleport2 : MonoBehaviour
{
    public Transform teleportTarget;
    public Teleport2 pairedTeleporter;
    public Sprite newSprite;
    public float CameraPosX, CameraPosY;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    public bool NeededAction;
    private bool playerIsInside = false;
    private GameObject player;
    public bool isActive = true;
    public float teleportDelay = 0f;

    private bool isTeleporting = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (isActive && playerIsInside && (!NeededAction || Input.GetKeyDown(KeyCode.F)))
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            playerIsInside = true;
            player = other.gameObject;
            if (NeededAction && newSprite != null)
                spriteRenderer.sprite = newSprite;
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
        if (player != null && teleportTarget != null && pairedTeleporter != null)
        {
            pairedTeleporter.SetActive(false);
            
            player.transform.position = teleportTarget.position;
            
            Camera.main.transform.position = new Vector3(
                CameraPosX,
                CameraPosY,
                Camera.main.transform.position.z
            );
            
            StartCoroutine(EnablePairedTeleporter());
        }
    }

    private IEnumerator EnablePairedTeleporter()
    {
        yield return new WaitForSeconds(0.5f);
        pairedTeleporter.SetActive(true);
    }

    public void SetActive(bool state) => isActive = state;
}