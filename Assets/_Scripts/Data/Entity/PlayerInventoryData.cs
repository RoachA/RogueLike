using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

namespace Game.Data
{
    public class PlayerInventoryData<T> : MonoBehaviour where T : ScriptableItemData
    {
        public List<ItemData> InventoryItems;
        public Dictionary<EntityEquipSlots, ItemData> _equippedItems;
    }
}
