using UnityEngine;

public class SpriteChangeOnProximity : MonoBehaviour
{
    public float proximityRadius = 5f;

    public Sprite defaultSprite;

    public Sprite proximitySprite;

    private SpriteRenderer spriteRenderer;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на объекте " + gameObject.name);
            enabled = false;
            return;
        }

        if (defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
        else
        {
            Debug.LogWarning("Default Sprite не назначен для объекта " + gameObject.name);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= proximityRadius)
        {
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
            if (defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
    }
}