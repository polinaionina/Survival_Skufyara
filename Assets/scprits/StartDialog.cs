using UnityEngine;
using DialogueEditor;

public class SequentialDialogTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;
    [SerializeField] private GameObject nextTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ConversationManager.Instance != null)
        {
            ConversationManager.Instance.StartConversation(conversation);
            GetComponent<Collider2D>().enabled = false;

            ConversationManager.OnConversationEnded += EnableNextTrigger;
        }
    }

    private void EnableNextTrigger()
    {
        if (nextTrigger != null)
        {
            nextTrigger.GetComponent<Collider2D>().enabled = true;
        }

        ConversationManager.OnConversationEnded -= EnableNextTrigger;
    }
}