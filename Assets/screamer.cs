using UnityEngine;

public class MonsterAudioTrigger : MonoBehaviour
{
    public AudioSource screamer; 
    public Transform Player;      
    public float activationDistance = 5f; 
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        
        if (distance <= activationDistance)
        {
            if (!screamer.isPlaying)
                screamer.Play(); 
        }
        else
        {
            if (screamer.isPlaying)
                screamer.Stop(); 
        }
    }
}
