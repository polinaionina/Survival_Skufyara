using UnityEngine;

public class SpriteChangeOnProximity : MonoBehaviour
{
    [Tooltip("Радиус, в пределах которого будет происходить смена спрайта")]
    public float proximityRadius = 5f;

    [Tooltip("Спрайт по умолчанию")]
    public Sprite defaultSprite;

    [Tooltip("Спрайт, когда игрок находится рядом")]
    public Sprite proximitySprite;

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    void Start()
    {
        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на объекте " + gameObject.name);
            enabled = false; // Отключаем скрипт, чтобы избежать ошибок
            return;
        }

        // Устанавливаем спрайт по умолчанию
        if (defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
        else
        {
            Debug.LogWarning("Default Sprite не назначен для объекта " + gameObject.name);
        }

        // Ищем игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
            enabled = false; // Отключаем скрипт, чтобы избежать ошибок
            return;
        }
    }

    void Update()
    {
        // Вычисляем расстояние до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Проверяем, находится ли игрок в пределах радиуса
        if (distanceToPlayer <= proximityRadius)
        {
            // Если спрайт для приближения назначен, меняем спрайт
            if (proximitySprite != null)
            {
                spriteRenderer.sprite = proximitySprite;
            }
            else
            {
                Debug.LogWarning("Proximity Sprite не назначен для объекта " + gameObject.name);
            }
        }
        else
        {
            // Если игрок вышел из радиуса, возвращаем спрайт по умолчанию
            if (defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }

    // (Опционально) Рисуем Gizmo в редакторе для визуализации радиуса
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
    }
}