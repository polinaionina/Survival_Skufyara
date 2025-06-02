using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Collect")]
public class CollectiblePart : InventoryItem
{
    private static int usedPartsCount = 0;
    private const int requiredCount = 4;
    public Sprite[] useEffectFrames;

    public override void Use()
    {
        usedPartsCount++;
        Debug.Log("+1");
        if (usedPartsCount >= requiredCount) OnAllPartsUsed();
        ItemUseEffectPlayer.Instance.PlayEffect(useEffectFrames, 1f);
    }

    private void OnAllPartsUsed()
    {   
        Debug.Log("Все предметы собраны");
        var obj = GameObject.Find("enddoor_0");
        var teleportScript = obj.GetComponent<teleportFINAL>();
        if (teleportScript != null) teleportScript.ActivateTeleport();
        QuestManager.Instance?.TriggerNextQuest();
    }

    public static void ResetUsedCount() =>
        usedPartsCount = 0;
    

    public static int GetUsedCount() =>
        usedPartsCount;
    
}
