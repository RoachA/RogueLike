using System;
using System.Collections.Generic;
using Game.Data;
using Game.Entities;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Button _closeBtn;

        private GameManager _gameManager;
        
        public override void Init<T>(T uiProperties = default)
        {
            base.Init(uiProperties);
            GetReferences();
            SetState(false);
        }

        public override void Open<T, T1>(Type uiType, T1 property)
        {
            _closeBtn.onClick.AddListener(OnClose);
            base.Open<T, T1>(uiType, property);
            
            if (property is InventoryUIProperties data)
            {
                SetState(true);
                
                _inventoryPanel.Init(data.InventoryItems);
                _equippedItemsPanel.Init(data.EquippedItems);

                _inventoryPanel.UpdateLayout();
                _gameManager = GameManager.Instance;
            }
        }

        public override void ForceUpdateUI<T, T1>(Type uiType, T1 property)
        {
            base.ForceUpdateUI<T, T1>(uiType, property);
            _inventoryPanel.ResetViewsForDisabling();

            if (property is InventoryUIProperties data)
            {
                _inventoryPanel.Init(data.InventoryItems);
                _equippedItemsPanel.Init(data.EquippedItems);
                
                _inventoryPanel.UpdateLayout();
                _gameManager = GameManager.Instance;
            }
        }

        private void OnClose()
        {
            _closeBtn.onClick.RemoveListener(OnClose);
        }

        public override void Close<T>(Type uiElement)
        {
            base.Close<T>(uiElement);
            _inventoryPanel.ResetViewsForDisabling();
            SetState(false);
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
        
        public virtual void SetState(bool state)
        {
            _inventoryPanel.gameObject.SetActive(state);
            _equippedItemsPanel.gameObject.SetActive(state);
            _closeBtn.gameObject.SetActive(state);
        }
    }
}
