using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationController : MonoBehaviour
{
    [Tooltip("Массив спрайтов (картинок) для последовательного отображения")]
    public Sprite[] notificationSprites;
    [Tooltip("Время плавного появления уведомления (в секундах)")]
    public float fadeInDuration = 0.5f;
    [Tooltip("Время, которое уведомление будет видно на экране (в секундах)")]
    public float displayDuration = 2.0f;
    [Tooltip("Время плавного затухания уведомления (в секундах)")]
    public float fadeOutDuration = 0.5f;

    private Image notificationImage;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        notificationImage = GetComponent<Image>();
        if (notificationImage == null)
        {
            Debug.LogError("NotificationController requires an Image component.");
            enabled = false;
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f; // Изначально полностью прозрачный
    }

    public void ShowNotifications()
    {
        if (notificationSprites == null || notificationSprites.Length == 0)
        {
            Debug.LogWarning("No notification sprites assigned.");
            return;
        }

        gameObject.SetActive(true); // Активируем NotificationImage
        StartCoroutine(ShowNotificationSequence());
    }

    private IEnumerator ShowNotificationSequence()
    {
        foreach (Sprite spriteToShow in notificationSprites)
        {
            notificationImage.sprite = spriteToShow;

            // Fade In
            yield return StartCoroutine(FadeCanvasGroup(0f, 1f, fadeInDuration));

            // Display
            yield return new WaitForSecondsRealtime(displayDuration);

            // Fade Out
            yield return StartCoroutine(FadeCanvasGroup(1f, 0f, fadeOutDuration));
        }

        gameObject.SetActive(false); // Деактивируем NotificationImage после завершения
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