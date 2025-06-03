using UnityEngine;
using UnityEngine.UI;

public class ScareTrigger : MonoBehaviour
{
    [Header("Настройки скримера")]
    [SerializeField] private AudioClip scareSound;
    [SerializeField] private GameObject scareImageObject;
    [SerializeField] private float scareDuration = 1.5f;

    private AudioSource audioSource;
    private bool hasTriggered = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = scareSound;

        if (scareImageObject != null)
        {
            scareImageObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            
            if (scareImageObject != null)
            {
                scareImageObject.SetActive(true);
            }

            if (scareSound != null)
            {
                audioSource.Play();
            }

            Invoke("EndScare", scareDuration);
        }
    }

    private void EndScare()
    {
        if (scareImageObject != null)
        {
            scareImageObject.SetActive(false);
        }
    }
}