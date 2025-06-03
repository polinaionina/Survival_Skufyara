using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text feedbackText;
    public Button confirmButton;
    public string correctAnswer = "unity";

    public GameObject[] objectsToDisable;

    public GameObject objectToEnable;

    public movee playerMovement;

    public NotificationPanelController notificationPanelController;
    public Sprite notificationImage;

    private void Start()
    {
        gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(CheckAnswer);

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false);
        }
    }

    public void ShowQuest()
    {
        playerMovement.LockMovement(true);
        gameObject.SetActive(true);
        inputField.text = "";
        feedbackText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void HideQuest()
    {
        playerMovement.LockMovement(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void CheckAnswer()
    {
        string userInput = NormalizeInput(inputField.text);
        string normalizedCorrectAnswer = NormalizeInput(correctAnswer);
        
        bool isCorrect = userInput == normalizedCorrectAnswer;

        feedbackText.text = isCorrect ? "Правильно!" : "Неправильно!";
        feedbackText.gameObject.SetActive(true);

        if (isCorrect)
        {
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null) obj.SetActive(false);
            }

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            HideQuest();

            if (notificationPanelController != null)
            {
                notificationPanelController.ShowNotifications(notificationImage);
            }
        }
    }

    private string NormalizeInput(string input)
    {
        return input.Replace(",", "").Trim().ToLower();
    }
}