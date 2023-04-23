using System.Collections.Generic;
using Game.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    public class EntityInventoryView : MonoBehaviour
    {
        [SerializeField] List<ItemEntity> _inventoryItems;
        [SerializeField] private Dictionary<EntityEquipSlots, ItemEntity> _equippedItems;

        [SerializeField] private int _inventorySize;

        private void Start()
        {
            _equippedItems = new Dictionary<EntityEquipSlots, ItemEntity>();
            _inventoryItems = new List<ItemEntity>();
            AddItemForTest();
        }

        public void AddItemToInventory(ItemEntity item)
        {
            _inventoryItems.Add(item);
        }

        public Dictionary<EntityEquipSlots, ItemEntity> GetEquippedItems()
        {
            return _equippedItems;
        }
        
        public void InitInventory(ItemEntity[] items)
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
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.RightHand, out ItemEntity equippedItem_r))
            {
                if (equippedItem_r.GetType() == typeof(ItemMeleeWeaponEntity))
                    equippedWeapons[0] = (ItemMeleeWeaponEntity) equippedItem_r;
            }
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.LeftHand, out ItemEntity equippedItem_l))
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

        public List<ItemEntity> GetInventoryItemsData()
        {
            List<ItemEntity> inventoryItems = new List<ItemEntity>();

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
            var weaponTemplate = DataManager.GetItemEntityWithData<ItemMeleeWeaponEntity>(registry.GetMeeleeWeaponDataAtIndex(0));
            var weaponTemplate2 = DataManager.GetItemEntityWithData<ItemMeleeWeaponEntity>(registry.GetMeeleeWeaponDataAtIndex(1));
            var instance = Instantiate(weaponTemplate, transform);
            EquipItem(EntityEquipSlots.RightHand, instance);
            instance.SetAsContained(false);
            
            for (int i = 0; i < 4; i++)
            { 
                var instance2 = Instantiate(weaponTemplate2, transform);
                instance2.SetAsContained(true);
                _inventoryItems.Add(instance2);  
            }
        }
    }
}
