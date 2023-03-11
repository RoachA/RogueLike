using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entites
{
    public class EntityInventoryView : MonoBehaviour
    {
        [SerializeField] Dictionary<ItemTypes, ItemEntity> _inventoryItems;
        [SerializeField] private Dictionary<EntityEquipSlots, ItemEntity> _equippedItems;

        [SerializeField] private int _inventorySize;

        public void AddItemToInventory(ItemEntity item)
        {
            //_registeredItems.Add(item.name, item);
        }

        public Dictionary<EntityEquipSlots, ItemEntity> GetEquippedItems()
        {
            return _equippedItems;
        }

        public void EquipItem(EntityEquipSlots slot, ItemEntity item)
        {
            if (_equippedItems.TryGetValue(slot, out var equippedItem))
            {
                
            }
        }
    }
}
