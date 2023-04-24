using System.Collections.Generic;
using Game.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    public class EntityInventoryView : MonoBehaviour
    {
        [SerializeField] List<IInventoryItem> _inventoryItems;
        [SerializeField] private Dictionary<EntityEquipSlots, IInventoryItem> _equippedItems;

        [SerializeField] private int _inventorySize;

        private void Start()
        {
            _equippedItems = new Dictionary<EntityEquipSlots, IInventoryItem>();
            _inventoryItems = new List<IInventoryItem>();
            AddItemForTest();
        }

        public void AddItemToInventory(Item item)
        {
            _inventoryItems.Add(item);
        }

        public Dictionary<EntityEquipSlots, IInventoryItem> GetEquippedItems()
        {
            return _equippedItems;
        }
        
        public void InitInventory(Item[] items)
        {
            foreach (var item in items)
            {
                AddItemToInventory(item);
            }
        }

        public int GetItemsDv()
        {
            var dvSum = 0;

            if (_equippedItems.Count == 0)
                return dvSum;
            
            foreach (var item in _equippedItems)
            {
                if (item.Value.GetType() == typeof(IInventoryItem))
                {
                    dvSum += item.Value.GetItemData<WearableItemData>().Stats.DV;
                }
            }
            
            return dvSum;
        }

        public ItemMeleeWeapon[] GetEquippedWeapons()
        {
            var equippedWeapons = new ItemMeleeWeapon[2];
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.RightHand, out IInventoryItem equippedItem_r))
            {
                if (equippedItem_r.GetType() == typeof(ItemMeleeWeapon))
                    equippedWeapons[0] = equippedItem_r as ItemMeleeWeapon;
            }
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.LeftHand, out IInventoryItem equippedItem_l))
            {
                if (equippedItem_l.GetType() == typeof(ItemMeleeWeapon))
                    equippedWeapons[1] = equippedItem_l as ItemMeleeWeapon;
            }

            return equippedWeapons;
        }
        
        public void EquipItem(EntityEquipSlots slot, IInventoryItem item)
        {
            if (_equippedItems.TryGetValue(slot, out IInventoryItem equippedItem))
            {
                _inventoryItems.Add(equippedItem);
                //todo after this check if the character is encumbered. if so, don't let him walk etc.
            }

            _equippedItems.TryAdd(slot, item);
        }

        public List<IInventoryItem> GetInventoryItemsData()
        {
            List<IInventoryItem> inventoryItems = new List<IInventoryItem>();

            foreach (var item in _inventoryItems)
            {
                inventoryItems.Add(item);
            }

            return inventoryItems;
        }
        
        //todo here is a problem. we actually do not need entities for inventory right?
        [Button]
        public void AddItemForTest(int indexFromRegistry = 0)
        {
            var registry = DataManager.GetWeaponsRegistry();
            MeleeWeaponData weaponTemplate = registry.GetMeeleeWeaponDataAtIndex(0);
            MeleeWeaponData weaponTemplate2 = registry.GetMeeleeWeaponDataAtIndex(1);
            var weapon = new ItemMeleeWeapon(weaponTemplate, false);
            
           EquipItem(EntityEquipSlots.RightHand, weapon);

            for (int i = 0; i < 4; i++)
            {
                var item = new ItemMeleeWeapon(weaponTemplate2, true);
                _inventoryItems.Add(item);  
            }
        }
    }
}
