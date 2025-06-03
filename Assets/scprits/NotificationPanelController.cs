using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NotificationPanelController : MonoBehaviour
{
    public Image notificationImage;
    public TMP_Text notificationText;
    public string[] notificationTexts;
    public Color textColor = Color.white;
    public float fadeInDuration = 0.5f;
    public float displayDuration = 2.0f;
    public float fadeOutDuration = 0.5f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
    }

    public void ShowNotifications(Sprite image)
    {
        if (notificationTexts == null || notificationTexts.Length == 0)
        {
            Debug.LogWarning("No notification texts assigned.");
            gameObject.SetActive(false);
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

            gameObject.SetActive(true);
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