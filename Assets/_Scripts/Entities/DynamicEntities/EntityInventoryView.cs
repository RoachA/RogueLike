using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Entites
{
    public class EntityInventoryView : MonoBehaviour
    {
        [SerializeField] List<ItemEntity> _inventoryItems;
        [SerializeField] private Dictionary<EntityEquipSlots, ItemEntity> _equippedItems;

        [SerializeField] private int _inventorySize;

        public void AddItemToInventory(ItemEntity item)
        {
            _inventoryItems.Add(item);
        }

        public Dictionary<EntityEquipSlots, ItemEntity> GetEquippedItems()
        {
            return _equippedItems;
        }

        public void EquipItem(EntityEquipSlots slot, ItemEntity item)
        {
            if (_equippedItems.TryGetValue(slot, out ItemEntity equippedItem))
            {
                _inventoryItems.Add(equippedItem);
                //todo after this check if the character is encumbered. if so, don't let him walk etc.
            }

            _equippedItems.TryAdd(slot, item);
        }
    }
}
