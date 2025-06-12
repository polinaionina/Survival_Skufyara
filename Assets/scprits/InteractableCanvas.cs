using UnityEngine;

public class InteractableCanvas : MonoBehaviour
{
    public Canvas fadeCanvas;
    
    private KeyCode interactionKey = KeyCode.F;
    
    private bool isPlayerInTrigger = false;
    private bool isCanvasActive = false;
    public Sprite newSprite;    
    private SpriteRenderer rend;
    private Sprite originalSprite;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalSprite = rend.sprite;
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(interactionKey))
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {   
        isCanvasActive = !isCanvasActive;
        if (isCanvasActive) fadeCanvas.sortingOrder = 10;
        else fadeCanvas.sortingOrder = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            rend.sprite = newSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            rend.sprite = originalSprite;
        }
    }
}