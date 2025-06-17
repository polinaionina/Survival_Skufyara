using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverManager : MonoBehaviour
{
    [Header("Объект для смены картинки")]
    public Image targetImage;

    [Header("Кнопки для отслеживания")]
    public Button button1;
    public Button button2;

    [Header("Спрайты")]
    public Sprite defaultSprite;
    public Sprite button1HoverSprite;
    public Sprite button2HoverSprite;

    void Start()
    {
        if (targetImage != null)
        {
            targetImage.sprite = defaultSprite;
        }
        else
        {
            Debug.LogError("ОШИБКА: 'Target Image' не назначен в инспекторе!", this);
            return; // Прерываем выполнение, если главная картинка не задана
        }

        // Добавляем слушателей событий для каждой кнопки
        AddHoverEvents(button1, "Кнопка 1");
        AddHoverEvents(button2, "Кнопка 2");
    }

    private void AddHoverEvents(Button button, string buttonName)
    {
        if (button == null)
        {
            Debug.LogWarning($"ПРЕДУПРЕЖДЕНИЕ: '{buttonName}' не назначена в инспекторе!", this);
            return;
        }

        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // --- Событие для НАВЕДЕНИЯ курсора (PointerEnter) ---
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        // Добавляем вызов метода OnPointerEnter, передавая ему нужную кнопку
        pointerEnterEntry.callback.AddListener((eventData) => { OnPointerEnter(button); });
        trigger.triggers.Add(pointerEnterEntry);

        // --- Событие для УВОДА курсора (PointerExit) ---
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { OnPointerExit(); });
        trigger.triggers.Add(pointerExitEntry);

        Debug.Log($"События наведения и увода курсора успешно добавлены для '{buttonName}'", this);
    }

    public void OnPointerEnter(Button hoveredButton)
    {
        Debug.Log($"Курсор НАВЕДЕН на: {hoveredButton.name}");

        if (targetImage == null) return;

        if (hoveredButton == button1)
        {
            targetImage.sprite = button1HoverSprite;
        }
        else if (hoveredButton == button2)
        {
            targetImage.sprite = button2HoverSprite;
        }
    }

    public void OnPointerExit()
    {
        Debug.Log("Курсор УШЕЛ с кнопки");

        if (targetImage != null)
        {
            targetImage.sprite = defaultSprite;
        }
    }
}