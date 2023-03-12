using System.Collections;
using System.Collections.Generic;
using Game.Data;
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
        
        public void InitInventory()
        {
        }

        public int GetItemsDv()
        {
            var dvSum = 0;
            
            foreach (var item in _equippedItems)
            {
                if (item.Value.GetType() == typeof(ItemWearableEntity))
                {
                    dvSum += item.Value.GetItemData<WearableItemData>().Stats.DV;
                }
            }
            
            return dvSum;
        }

        public ItemMeleeWeaponEntity[] GetEquippedWeapons()
        {
            var equippedWeapons = new ItemMeleeWeaponEntity[2];
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.rightHand, out ItemEntity equippedItem_r))
            {
                if (equippedItem_r.GetType() == typeof(ItemMeleeWeaponEntity))
                    equippedWeapons[0] = (ItemMeleeWeaponEntity) equippedItem_r;
            }
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.leftHand, out ItemEntity equippedItem_l))
            {
                if (equippedItem_l.GetType() == typeof(ItemMeleeWeaponEntity))
                    equippedWeapons[1] = (ItemMeleeWeaponEntity) equippedItem_l;
            }

            return equippedWeapons;
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
