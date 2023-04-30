using System;
using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entities
{
    public class EntityInventoryView : MonoBehaviour
    {
        [SerializeField] Dictionary<Guid, IInventoryItem> _inventoryItems;
        [SerializeField] private Dictionary<EntityEquipSlots, IInventoryItem> _equippedItems;
        [SerializeField] private int _inventorySize;

        private EntityDynamic _entityWithInventory;

        private void Start()
        {
            _equippedItems = new Dictionary<EntityEquipSlots, IInventoryItem>();
            _inventoryItems = new Dictionary<Guid, IInventoryItem>();
            AddItemForTest();
            SetSubscriptions();
        }

        private void SetSubscriptions()
        {
            _entityWithInventory = GetComponent<EntityDynamic>();
            InventoryItemView.InventoryItemOperatedEvent += OnInventoryEvent;
        }

        private void OnInventoryEvent(Guid uniqueId, InventoryAction actionType)
        {
            if (actionType == InventoryAction.Drop)
                DropItem(uniqueId);
        }

        public void DropItem(Guid uniqueId)
        {
            var itemToDrop = FindItemWithUniqueId(uniqueId);
            GenerateEntityView(itemToDrop);
            RemoveFromInventory(uniqueId);
           UpdateUI();
        }

        private void UpdateUI()
        {
            List<IInventoryItem> inventoryList = _inventoryItems.Values.ToList();
            UIElement.ForceUpdateUiSignal(typeof(InventoryPopup), new InventoryUIProperties(inventoryList, _equippedItems));  
        }

        private void GenerateEntityView(IInventoryItem item)
        {
            var itemData = (ItemData) item;
            var entityPos = _entityWithInventory.GetEntityPos();
            var tile = _entityWithInventory.GetOccupiedTile();
            
            Vector3 worldPos = new Vector3(entityPos.x, entityPos.y, 1);
            
            var view = DataManager.GetItemEntity();
            
            var entity = Instantiate(view, worldPos, Quaternion.identity);
            entity._itemData = itemData.ScriptableItemData;
            entity.Init(itemData.Id);
            entity.SetOccupiedTile(tile);
            tile.AddEntityToTile(entity);
        }

        /*private void DropItemEntityOnTile(ItemEntityView<ScriptableItemData> item)
        {
        }*/

        private IInventoryItem FindItemWithUniqueId(Guid id)
        {
            if (_inventoryItems.TryGetValue(id, out var item) == false)
            {
                Debug.LogWarning("No item with guid exists");
                return null;
            }
            
            return item;
        }

        private void RemoveFromInventory(Guid id)
        {
            if (_inventoryItems.ContainsKey(id))
                _inventoryItems.Remove(id);
        }

        public void AddItemToInventory(IInventoryItem item)
        {
           _inventoryItems.Add(item.Id, item);
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
                    dvSum += item.Value.GetItemData<WearableScriptableItemData>().Stats.DV;
                }
            }
            
            return dvSum;
        }

        public IInventoryItem[] GetEquippedWeapons()
        {
            var equippedWeapons = new IInventoryItem[2];
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.RightHand, out IInventoryItem equippedItem_r))
            {
                if (equippedItem_r.GetType() == typeof(ScriptableItemDataMeleeWeapon))
                    equippedWeapons[0] = equippedItem_r;
            }
            
            if (_equippedItems.TryGetValue(EntityEquipSlots.LeftHand, out IInventoryItem equippedItem_l))
            {
                if (equippedItem_l.GetType() == typeof(IInventoryItem))
                    equippedWeapons[1] = equippedItem_l;
            }

            return equippedWeapons;
        }
        
        public void EquipItem<T>(EntityEquipSlots slot, T item) where T : IInventoryItem
        {
            if (_equippedItems.TryGetValue(slot, out IInventoryItem equippedItem))
            {
                AddItemToInventory(equippedItem);
                //todo after this check if the character is encumbered. if so, don't let him walk etc.
            }

            _equippedItems.TryAdd(slot, item);
        }

        public List<IInventoryItem> GetInventoryItems()
        {
            List<IInventoryItem> inventoryItems = new List<IInventoryItem>();

            foreach (var item in _inventoryItems)
            {
                inventoryItems.Add(item.Value);
            }

            return inventoryItems;
        }
        
        //todo here is a problem. we actually do not need entities for inventory right?
        [Button]
        public void AddItemForTest(int indexFromRegistry = 0)
        {
            var registry = DataManager.GetWeaponsRegistry();
            var weapon1 = new ItemData(registry.GetMeleeWeaponDataAtIndex(0), true);
            
           EquipItem(EntityEquipSlots.RightHand, weapon1);

            for (int i = 0; i < 4; i++)
            {
                var weapon2 = new ItemData(registry.GetMeleeWeaponDataAtIndex(1), false);
                AddItemToInventory(weapon2);
            }
        }
    }
}
