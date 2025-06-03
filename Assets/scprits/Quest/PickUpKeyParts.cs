using UnityEngine;

public class PickUpKeyParts : MonoBehaviour
{
    public InventoryItem itemToPickup;
    public Sprite newSprite;

    public bool showNotificationOnPickup = false;
    public NotificationPanelController notificationPanelController;
    public Sprite notificationImage;

    private bool canPickup = false;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool hasPickedUp = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogError("PickUpKeyParts: На объекте отсутствует SpriteRenderer!", this);
            enabled = false;
        }
    }

    void Update()
    {
        if (canPickup && !hasPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryUI.Instance != null)
            {
                bool added = InventoryUI.Instance.AddItem(itemToPickup);
                Debug.Log("Попытка подобрать часть ключа: " + itemToPickup.itemName + ". Успешно: " + added);

                if (added)
                {
                    InventoryUI.Instance.updateKeyPartsCount();
                    hasPickedUp = true;
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = originalSprite;
                    }

                    if (showNotificationOnPickup)
                    {
                        if (notificationPanelController != null)
                        {
                            notificationPanelController.ShowNotifications(notificationImage);
                        }
                        else
                        {
                            Debug.LogWarning("PickUpKeyParts: NotificationPanelController не назначен, уведомление не будет показано.", this);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("PickUpKeyParts: InventoryUI.Instance не найден! Невозможно добавить предмет.", this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPickedUp)
        {
            canPickup = true;
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            if (spriteRenderer != null && !hasPickedUp)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}