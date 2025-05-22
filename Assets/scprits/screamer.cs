using UnityEngine;

public class MonsterAudioTrigger : MonoBehaviour
{
    public AudioSource screamer; // аудиосорс для скримера
    public Transform Player;      // трансформ игрока
    public float activationDistance = 5f; // радиус активации звука

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        
        if (distance <= activationDistance)
        {
            if (!screamer.isPlaying)
                screamer.Play(); // Включаем звук, если он не играет
        }
        else
        {
            if (screamer.isPlaying)
                screamer.Stop(); // Останавливаем звук, если игрок далеко
        }
    }
}
