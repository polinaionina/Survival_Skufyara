using UnityEngine;

public class DisableTriggerOnEnter : MonoBehaviour
{
    private Collider2D triggerCollider;
    private bool playerEntered = false; // Флаг, чтобы убедиться, что отключаем только один раз

    void Awake()
    {
        // Получаем ссылку на Collider2D компонент этого же объекта
        triggerCollider = GetComponent<Collider2D>();

        // Проверяем, что это действительно триггер
        if (triggerCollider == null || !triggerCollider.isTrigger)
        {
            Debug.LogWarning("Объект " + gameObject.name + " имеет скрипт DisableTriggerOnEnter, но не является триггером или не имеет Collider2D.", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что вошел именно игрок (предполагаем, что у игрока есть тег "Player")
        // Если у вашего игрока другой тег, измените "Player" на нужный.
        if (other.CompareTag("Player") && !playerEntered)
        {
            // Отключаем компонент Collider2D
            if (triggerCollider != null)
            {
                triggerCollider.enabled = false;
                playerEntered = true; // Устанавливаем флаг, что игрок вошел
                Debug.Log("Триггер на объекте " + gameObject.name + " отключен, потому что игрок вошел.");

                // Здесь вы можете также вызвать метод начала диалога
                // Например, если у вас есть скрипт DialogueManager с методом StartDialogue()
                // DialogueManager.instance.StartDialogue();
            }
        }
    }

    // Если вам нужно включить триггер снова (например, после завершения диалога),
    // вы можете вызвать этот публичный метод из другого скрипта.
    public void EnableTrigger()
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
            playerEntered = false; // Сбрасываем флаг
            Debug.Log("Триггер на объекте " + gameObject.name + " снова включен.");
        }
    }
}