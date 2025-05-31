using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Collect")]
public class CollectiblePart : InventoryItem
{
    private static int usedPartsCount = 0;
    private const int requiredCount = 4;

    public override void Use()
    {
        usedPartsCount++;
         Debug.Log("+1");
        if (usedPartsCount >= requiredCount) OnAllPartsUsed();
    }

    private void OnAllPartsUsed()
    {   
        Debug.Log("Все предметы собраны");
        GameObject obj = GameObject.Find("enddoor_0");
        var teleportScript = obj.GetComponent<teleportFINAL>();
        if (teleportScript != null) teleportScript.ActivateTeleport();
        QuestManager.Instance?.TriggerNextQuest();
    }

    public static void ResetUsedCount() =>
        usedPartsCount = 0;
    

    public static int GetUsedCount() =>
        usedPartsCount;
    
}
