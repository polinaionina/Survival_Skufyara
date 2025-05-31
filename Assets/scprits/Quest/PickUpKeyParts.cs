using UnityEngine;

public class PickUpKeyParts : MonoBehaviour
{   
    public InventoryItem itemToPickup; 
    public Sprite newSprite;
    private bool canPickup = false;    
    private bool HasTwoParts = false;
    private Sprite originalSprite;
    private SpriteRenderer spriteRenderer;
    private bool HasPick;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {   
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            var added = InventoryUI.Instance.AddItem(itemToPickup);
            Debug.Log("Item pickup attempted. Success: " + added);
            InventoryUI.Instance.updateKeyPartsCount();
            spriteRenderer.sprite = originalSprite;
            HasPick = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !HasPick)
        {
            canPickup = true;
            spriteRenderer.sprite = newSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            spriteRenderer.sprite = originalSprite;
        }
    }
}
