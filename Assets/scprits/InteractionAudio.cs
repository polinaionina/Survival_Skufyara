using UnityEngine;

public class InteractionAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public KeyCode interactionKey = KeyCode.F;

    private bool isPlayerInTrigger = false;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(interactionKey))
        {
            if (audioSource != null)
            {
                audioSource.Stop();   
                audioSource.Play();   
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
