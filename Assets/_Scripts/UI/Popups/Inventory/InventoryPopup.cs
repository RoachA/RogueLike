using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Entites;

namespace Game.UI
{
    public class InventoryUIProperties : UIProperties
    {
        public List<ItemEntity> Items;
        
        public InventoryUIProperties(List<ItemEntity> items)
        {
            Items = items;
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
                
                _inventoryPanel.Init(data.Items);

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
