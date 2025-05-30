using UnityEngine;

public class EnemyWander : MonoBehaviour
{   
    public static EnemyWander Instanсe;
    public float speed = 2f;
    public float directionChangeInterval = 2f;

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
    private float timer = 2f;
    private SpriteRenderer spriteRenderer;

    private bool isActive = false;
    private bool aggressiveMode = false;

    void Start()
    {   
        Instanсe = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        if (isActive)
        {   
            timer += Time.deltaTime;
            if (timer >= directionChangeInterval)
            {
                ChooseNewDirection();
                timer = 0f;
            }
        }
    }
    
    public void SetActive() => isActive = true;

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void ChooseNewDirection()
    {
        var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        UpdateSprite();
    }

    void UpdateSprite()
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
            if (moveDirection.y > 0)
                newSprite = aggressiveMode ? spriteBackAxe : spriteBack;
            else
                newSprite = aggressiveMode ? spriteForwardAxe : spriteForward;
        }

        spriteRenderer.sprite = newSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal);
            UpdateSprite();
        }
    }

    public void SetAggressive(bool state)
    {
        aggressiveMode = state;
        UpdateSprite();
    }
}
