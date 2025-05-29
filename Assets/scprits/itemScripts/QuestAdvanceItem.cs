using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/QuestAdvance")]
public class QuestAdvanceItem : InventoryItem
{
    public string targetObjectName; 

    public override void Use()
    {
        base.Use();

        GameObject obj = GameObject.Find(targetObjectName);
        if (obj != null)
        {
            var teleportScript = obj.GetComponent<TeleportOnTrigger>();
            if (teleportScript != null) teleportScript.SetActive(); 
            var faderScript = obj.GetComponent<FadeTrigger>();
            if (faderScript != null) faderScript.SetActive(); 
        }

        QuestManager.Instance?.TriggerNextQuest();
    }
}
