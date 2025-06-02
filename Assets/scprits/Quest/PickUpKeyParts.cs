using UnityEngine;

public class PickUpKeyParts : MonoBehaviour
{
    [Header("Настройки предмета")]
    public InventoryItem itemToPickup;
    public Sprite newSprite; // Спрайт, который показывается, когда можно подобрать

    [Header("Настройки уведомлений (необязательно)")]
    [Tooltip("Включить показ уведомления при подборе предмета?")]
    public bool showNotificationOnPickup = false;
    [Tooltip("Ссылка на контроллер панели уведомлений")]
    public NotificationPanelController notificationPanelController;
    [Tooltip("Изображение для уведомления")]
    public Sprite notificationImage;

    private bool canPickup = false;
    // private bool HasTwoParts = false; // Эта переменная не использовалась, возможно, она нужна для другой логики
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool hasPickedUp = false; // Флаг, что предмет уже был поднят

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
            enabled = false; // Отключаем скрипт, если нет SpriteRenderer
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
                    hasPickedUp = true; // Устанавливаем флаг, что предмет поднят
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = originalSprite; // Возвращаем оригинальный спрайт или можно скрыть объект
                    }
                    // gameObject.SetActive(false); // Можно деактивировать объект после подбора, если он не уничтожается

                    // Показываем уведомление, если включено и настроено
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
                    // Если предмет не должен уничтожаться, но и не должен быть подбираем повторно,
                    // то hasPickedUp флаг предотвратит это.
                    // Если объект должен уничтожаться, то можно добавить Destroy(gameObject);
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
        if (other.CompareTag("Player") && !hasPickedUp) // Проверяем, что предмет еще не поднят
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
            if (spriteRenderer != null && !hasPickedUp) // Возвращаем оригинальный спрайт, только если предмет не поднят
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}