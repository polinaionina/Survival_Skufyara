using UnityEngine;

public class EnableEnemyOnTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            EnemyWander.Instanсe.SetActive();
            Destroy(gameObject);
        }
    }
}
