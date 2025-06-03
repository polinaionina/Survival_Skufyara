using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorTaskSystem2D : MonoBehaviour
{
    public GameObject taskPanel;
    public Image taskImage;
    public TMP_InputField answerInput;
    public string correctAnswer = "123";
    public KeyCode interactionKey = KeyCode.F;

    private bool isPlayerNear = false;

    void Start()
    {
        taskPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            taskPanel.SetActive(true);
            answerInput.text = "";
            Time.timeScale = 0f;
        }
    }

    public void CheckAnswer()
    {
        if (answerInput.text == correctAnswer)
        {
            taskPanel.SetActive(false);
            Destroy(gameObject);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log("Неверно! Попробуй ещё раз.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            taskPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}