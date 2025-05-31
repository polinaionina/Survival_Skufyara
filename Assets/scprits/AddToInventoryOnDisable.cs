using UnityEngine;

public class AddToInventoryOnDisable : MonoBehaviour
{
    public InventoryItem itemToAdd;

    private void OnDisable()
    {
        if (InventoryUI.Instance != null)
        {
            var success = InventoryUI.Instance.AddItem(itemToAdd);
        }
    }
}
