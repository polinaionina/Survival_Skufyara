using UnityEngine;
using System.Collections;

public class DelayedActionExecutor : MonoBehaviour
{
    public void ExecuteDelayedTeleportAndQuest(float delay, Vector3 targetPosition, GameObject triggerToActivate)
    {
        StartCoroutine(TeleportAndQuestSequence(delay, targetPosition, triggerToActivate));
    }

    private IEnumerator TeleportAndQuestSequence(float delay, Vector3 targetPosition, GameObject triggerToActivate)
    {
        Debug.Log($"Начинается задержка {delay} сек перед телепортацией...");
        yield return new WaitForSeconds(delay);
        Debug.Log("Задержка окончена. Телепортация...");
        transform.position = targetPosition;
        Debug.Log("Телепортация завершена. Активация триггера и квеста...");

        if (triggerToActivate != null)
        {
            triggerToActivate.SetActive(true);
            Debug.Log($"Триггер '{triggerToActivate.name}' активирован.");
        }

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.TriggerNextQuest();
            Debug.Log("QuestManager.Instance.TriggerNextQuest() вызван.");
        }
        else
        {
            Debug.LogWarning("QuestManager.Instance не найден. Квест не продвинут.");
        }
    }
}