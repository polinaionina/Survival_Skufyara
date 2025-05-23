using UnityEngine;

public class AddToInventoryOnDisable : MonoBehaviour
{
    public InventoryItem itemToAdd;

    private void OnDisable()
    {
        if (InventoryUI.Instance != null)
        {
            bool success = InventoryUI.Instance.AddItem(itemToAdd);
        }
    }
}
