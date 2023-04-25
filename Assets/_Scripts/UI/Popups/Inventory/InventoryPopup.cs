using System;
using System.Collections.Generic;
using Game.Data;
using Game.Entites;
using UnityEngine;

namespace Game.UI
{
    public class InventoryUIProperties : UIProperties
    {
        public List<IInventoryItem> InventoryItems;
        public Dictionary<EntityEquipSlots, IInventoryItem> EquippedItems;
        
        public InventoryUIProperties(List<IInventoryItem> inventoryItems, Dictionary<EntityEquipSlots, IInventoryItem> equippedItems)
        {
            InventoryItems = inventoryItems;
            EquippedItems = equippedItems;
        }
    }
    
    public class InventoryPopup : UIElement
    {
        [SerializeField] private InventoryManagementPanel _inventoryPanel;
        [SerializeField] private EquippedItemsManagementPanel _equippedItemsPanel;
        
        public override void Init<T>(T uiProperties = default)
        {
            base.Init(uiProperties);
            GetReferences();
            SetInactive();
        }

        public override void Open<T, T1>(Type uiType, T1 property)
        {
            base.Open<T, T1>(uiType, property);
            
            if (property is InventoryUIProperties data)
            {
                _inventoryPanel.gameObject.SetActive(true);
                _equippedItemsPanel.gameObject.SetActive(true);
                
                _inventoryPanel.Init(data.InventoryItems);
                _equippedItemsPanel.Init(data.EquippedItems);

                _inventoryPanel.UpdateLayout();
            }
        }

        public override void Close<T>(Type uiElement)
        {
            _inventoryPanel.ResetViewsForDisabling();
            base.Close<T>(uiElement);
            SetInactive();
        }

        private void GetReferences()
        {
            
        }

        private void LoadInventoryView()
        {
            
        }

        private void LoadEquippedItemView()
        {
            
        }
        
        public virtual void SetInactive()
        {
            _inventoryPanel.gameObject.SetActive(false);
            _equippedItemsPanel.gameObject.SetActive(false);
        }

    }
}
