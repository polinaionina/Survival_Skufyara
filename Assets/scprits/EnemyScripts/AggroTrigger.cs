using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    public EnemyWander enemyWander;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            enemyWander.SetAggressive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            enemyWander.SetAggressive(false);
    }
}
