using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NotificationPanelController : MonoBehaviour
{
    [Tooltip("UI Image для отображения изображения")]
    public Image notificationImage;
    [Tooltip("TextMeshPro Text для отображения текста")]
    public TMP_Text notificationText;
    [Tooltip("Массив текстов уведомлений")]
    public string[] notificationTexts;
    [Tooltip("Цвет текста")]
    public Color textColor = Color.white; // Цвет текста по умолчанию - белый
    [Tooltip("Время плавного появления уведомления (в секундах)")]
    public float fadeInDuration = 0.5f;
    [Tooltip("Время, которое уведомление будет видно на экране (в секундах)")]
    public float displayDuration = 2.0f;
    [Tooltip("Время плавного затухания уведомления (в секундах)")]
    public float fadeOutDuration = 0.5f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f; // Изначально полностью прозрачный
    }

    public void ShowNotifications(Sprite image)
    {
        if (notificationTexts == null || notificationTexts.Length == 0)
        {
            Debug.LogWarning("No notification texts assigned.");
            gameObject.SetActive(false); // Ensure panel is deactivated if no texts
            return;
        }
        StartCoroutine(ShowNotificationSequence(image));
    }

    private IEnumerator ShowNotificationSequence(Sprite image)
    {
        foreach (string text in notificationTexts)
        {
            notificationText.text = text;
            notificationText.color = textColor;
            notificationImage.sprite = image;

            gameObject.SetActive(true); // Активируем NotificationPanel
            // Fade In
            yield return StartCoroutine(FadeCanvasGroup(0f, 1f, fadeInDuration));

            // Display
            yield return new WaitForSecondsRealtime(displayDuration);

            // Fade Out
            yield return StartCoroutine(FadeCanvasGroup(1f, 0f, fadeOutDuration));
        }
        gameObject.SetActive(false); // Ensure panel is deactivated after all texts
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        canvasGroup.alpha = startAlpha; // Ensure starting alpha is correct

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha; // Ensure ending alpha is exact
    }
}    