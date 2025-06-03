using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationController : MonoBehaviour
{
    public Sprite[] notificationSprites;
    public float fadeInDuration = 0.5f;
    public float displayDuration = 2.0f;
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
        canvasGroup.alpha = 0f;
    }

    public void ShowNotifications()
    {
        if (notificationSprites == null || notificationSprites.Length == 0)
        {
            Debug.LogWarning("No notification sprites assigned.");
            return;
        }

        gameObject.SetActive(true);
        StartCoroutine(ShowNotificationSequence());
    }

    private IEnumerator ShowNotificationSequence()
    {
        foreach (Sprite spriteToShow in notificationSprites)
        {
            notificationImage.sprite = spriteToShow;

            yield return StartCoroutine(FadeCanvasGroup(0f, 1f, fadeInDuration));

            yield return new WaitForSecondsRealtime(displayDuration);

            yield return StartCoroutine(FadeCanvasGroup(1f, 0f, fadeOutDuration));
        }

        gameObject.SetActive(false);
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        canvasGroup.alpha = startAlpha;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}