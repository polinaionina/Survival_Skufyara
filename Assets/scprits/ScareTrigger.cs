using UnityEngine;
using UnityEngine.UI;

public class ScareTrigger : MonoBehaviour
{
    [Header("Настройки скримера")]
    [SerializeField] private AudioClip scareSound; // Звук скримера
    [SerializeField] private GameObject scareImageObject; // Объект с Image для картинки
    [SerializeField] private float scareDuration = 1.5f; // Длительность скримера в секундах

    private AudioSource audioSource;
    private bool hasTriggered = false; // Флаг, чтобы триггер сработал только один раз

    private void Start()
    {
        // Настраиваем компонент AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = scareSound;

        // Выключаем изображение в начале
        if (scareImageObject != null)
        {
            scareImageObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что триггер срабатывает от игрока и ещё не активировался
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Помечаем как сработавший
            
            // Активируем изображение
            if (scareImageObject != null)
            {
                scareImageObject.SetActive(true);
            }

            // Проигрываем звук
            if (scareSound != null)
            {
                audioSource.Play();
            }

            // Запускаем таймер для выключения скримера
            Invoke("EndScare", scareDuration);
        }
    }

    private void EndScare()
    {
        // Выключаем изображение
        if (scareImageObject != null)
        {
            scareImageObject.SetActive(false);
        }

        // Можно отключить и сам объект триггера, если он больше не нужен
        // gameObject.SetActive(false);
    }
}