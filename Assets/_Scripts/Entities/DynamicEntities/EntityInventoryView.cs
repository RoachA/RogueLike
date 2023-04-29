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
            if (_equippedItems == null) return default;
            
            if (_equippedItems.Count == 0) return default;
            
            return _equippedItems;
        }

        [Button]
        private void DebugInventoryItems()
        {
            foreach (var item in _inventoryItems)
            {
                Debug.Log(item.GetType() + " is in inventory"); 
            }

            foreach (var item in _equippedItems)
            {
                var typed = item.Value as ItemMeleeWeapon;
                Debug.Log(item.Value.GetType().Name + " is equipped in the slot " + item.Key); 
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
                    dvSum += item.Value.GetItemData<WearableItemDefinitionData>().Stats.DV;
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
        
        public void EquipItem<T>(EntityEquipSlots slot, T item) where T : IInventoryItem
        {
            if (_equippedItems.TryGetValue(slot, out IInventoryItem equippedItem))
            {
                AddItemToInventory(equippedItem as Item);
                //todo after this check if the character is encumbered. if so, don't let him walk etc.
            }

            _equippedItems.TryAdd(slot, item);
        }

        public List<IInventoryItem> GetInventoryItems()
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
            MeleeWeaponDefinitionData weaponDefinitionTemplate = registry.GetMeeleeWeaponDataAtIndex(0);
            MeleeWeaponDefinitionData weaponDefinitionTemplate2 = registry.GetMeeleeWeaponDataAtIndex(1);
            Item weapon = new Item(weaponDefinitionTemplate, false);
            
           EquipItem(EntityEquipSlots.RightHand, weapon);

            for (int i = 0; i < 4; i++)
            {
                Item item = new Item(weaponDefinitionTemplate2, true);
                AddItemToInventory(item);  
            }
        }
    }
}
