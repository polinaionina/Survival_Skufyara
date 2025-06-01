using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Настройки предмета")]
    public InventoryItem itemToPickup;
    public bool NeededDestroy = true; // Уничтожать объект после подбора по умолчанию

    [Header("Настройки уведомлений (необязательно)")]
    [Tooltip("Включить показ уведомления при подборе предмета?")]
    public bool showNotificationOnPickup = false;
    [Tooltip("Ссылка на контроллер панели уведомлений")]
    public NotificationPanelController notificationPanelController;
    [Tooltip("Изображение для уведомления")]
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
                    // Показываем уведомление, если включено и настроено
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

                    // Уничтожаем объект, если это необходимо
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
            // Тут можно добавить визуальную подсказку для игрока, что предмет можно подобрать
            // Например, показать текст "Нажмите F для подбора"
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            // Тут можно скрыть визуальную подсказку
        }
    }
}