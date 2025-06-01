using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public static EnemyWander Instanсe;
    public float speed = 2f;

    public Sprite spriteForward;
    public Sprite spriteBack;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public Sprite spriteForwardAxe;
    public Sprite spriteBackAxe;
    public Sprite spriteLeftAxe;
    public Sprite spriteRightAxe;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private bool isActive = false;
    private bool aggressiveMode = false;

    // Твои 4 угла маршрута
    private Vector2[] points = new Vector2[]
    {
        new Vector2(69.902f, -12.132f), // влево
        new Vector2(69.902f, -4.043f),  // вверх
        new Vector2(71.653f, -4.043f),  // вправо
        new Vector2(71.653f, -12.132f), // вниз (исходная точка)
    };

    private int currentTargetIndex = 0;

    private float reachThreshold = 0.05f; // насколько близко подойти к точке, чтобы считать ее достигнутой

    void Start()
    {
        Instanсe = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Начинаем движение к первой точке
        currentTargetIndex = 0;
    }

    void Update()
    {
        if (!isActive) return;

        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = points[currentTargetIndex];

        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Двигаемся к цели
        rb.linearVelocity = direction * speed;

        // Меняем спрайт в зависимости от направления движения
        UpdateSprite(direction);

        // Проверяем, достигли ли цель
        if (Vector2.Distance(currentPosition, targetPosition) < reachThreshold)
        {
            // Следующая точка (по циклу)
            currentTargetIndex = (currentTargetIndex + 1) % points.Length;
        }
    }

    public void SetActive() => isActive = true;

    void UpdateSprite(Vector2 moveDirection)
    {
        Sprite newSprite = null;

        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            newSprite = moveDirection.x > 0
                ? (aggressiveMode ? spriteRightAxe : spriteRight)
                : (aggressiveMode ? spriteLeftAxe : spriteLeft);
        }
        else
        {
            newSprite = moveDirection.y > 0
                ? (aggressiveMode ? spriteBackAxe : spriteBack)
                : (aggressiveMode ? spriteForwardAxe : spriteForward);
        }

        spriteRenderer.sprite = newSprite;
    }

    public void SetAggressive(bool state)
    {
        aggressiveMode = state;
        // При смене режима обновим спрайт согласно последнему направлению
        // Для этого возьмем вектор к текущей цели
        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = points[currentTargetIndex];
        Vector2 direction = (targetPosition - currentPosition).normalized;
        UpdateSprite(direction);
    }
}