using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInventoryView : MonoBehaviour
{
    [SerializeField] Dictionary<int, ItemBase> _registeredItems;
    [SerializeField] private int _inventorySize;

    public void AddItemToInventory(ItemBase item)
    {
        //_registeredItems.Add(item.name, item);
    }
}
