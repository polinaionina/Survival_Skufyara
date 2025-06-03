using UnityEngine;

public class QuestTrigger_First : MonoBehaviour
{
    public QuestPanelController questUI;
    public Sprite newSprite;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (questUI != null)
            {
                if (!questUI.gameObject.activeSelf)
                {
                    questUI.ShowQuest();
                }
                else
                {
                    questUI.HideQuest();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (newSprite != null)
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
            spriteRenderer.sprite = originalSprite;
        }
    }
}