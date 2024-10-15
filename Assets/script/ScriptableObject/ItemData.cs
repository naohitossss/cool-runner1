using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int value;
    public bool isStackable;
    public int maxStackSize;
    public void Use()
    {
        Debug.Log("Žg—p: " + itemName);
    }
}
