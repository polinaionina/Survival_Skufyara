using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InventoryItem itemToPickup;
    public bool NeededDestroy = true;

    public bool showNotificationOnPickup = false;
    public NotificationPanelController notificationPanelController;
    public Sprite notificationImage;

    private bool canPickup = false;

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryUI.Instance != null)
            {
                bool added = InventoryUI.Instance.AddItem(itemToPickup);
                Debug.Log("Попытка подобрать предмет: " + itemToPickup.itemName + ". Успешно: " + added);

                if (added)
                {
                    if (showNotificationOnPickup)
                    {
                        if (notificationPanelController != null)
                        {
                            notificationPanelController.ShowNotifications(notificationImage);
                        }
                        else
                        {
                            Debug.LogWarning("PickupItem: NotificationPanelController не назначен, уведомление не будет показано.", this);
                        }
                    }

                    if (NeededDestroy)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                Debug.LogError("PickupItem: InventoryUI.Instance не найден! Невозможно добавить предмет.", this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }
}