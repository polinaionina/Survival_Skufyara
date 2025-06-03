using UnityEngine;

public class SimpleImagePanelController : MonoBehaviour
{
    public GameObject objectToShow;
    public Sprite newTriggerSprite;
    public KeyCode interactionKey = KeyCode.F;

    public bool pauseGame = true;

    private SpriteRenderer triggerSpriteRenderer;
    private Sprite originalTriggerSprite;
    private bool playerInRange = false;

    private void Start()
    {
        triggerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (triggerSpriteRenderer != null)
        {
            originalTriggerSprite = triggerSpriteRenderer.sprite;
        }

        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactionKey))
        {
            ToggleObject();
        }
    }

    private void ToggleObject()
    {
        if (objectToShow == null) return;

        bool shouldShow = !objectToShow.activeSelf;
        objectToShow.SetActive(shouldShow);

        if (pauseGame)
        {
            Time.timeScale = shouldShow ? 0f : 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (triggerSpriteRenderer != null && newTriggerSprite != null)
            {
                triggerSpriteRenderer.sprite = newTriggerSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (triggerSpriteRenderer != null)
            {
                triggerSpriteRenderer.sprite = originalTriggerSprite;
            }

            if (objectToShow != null)
            {
                objectToShow.SetActive(false);
                if (pauseGame) Time.timeScale = 1f;
            }
        }
    }
}