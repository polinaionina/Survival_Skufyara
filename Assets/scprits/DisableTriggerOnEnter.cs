using UnityEngine;

public class DisableTriggerOnEnter : MonoBehaviour
{
    private Collider2D triggerCollider;
    private bool playerEntered = false;

    void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();

        if (triggerCollider == null || !triggerCollider.isTrigger)
        {
            Debug.LogWarning("Объект " + gameObject.name + " имеет скрипт DisableTriggerOnEnter, но не является триггером или не имеет Collider2D.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerEntered)
        {
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
                playerEntered = true;
                Debug.Log("Триггер на объекте " + gameObject.name + " отключен, потому что игрок вошел.");
            }
        }
    }

    public void EnableTrigger()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
            playerEntered = false;
            Debug.Log("Триггер на объекте " + gameObject.name + " снова включен.");
        }
    }
}