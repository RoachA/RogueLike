using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

public class PlayerInventoryData : MonoBehaviour
{
    public List<ItemEntity> InventoryItems;
    public Dictionary<EntityEquipSlots, ItemEntity> _equippedItems;
}
