using System;
using System.Collections.Generic;
using Game.Data;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;


namespace Game.UI
{
    public class InventoryManagementPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InventoryCategoryView _categoryContainerRef;
        [SerializeField] private InventoryItemView _inventoryItemViewRef;
        [SerializeField] private LayoutGroup _inventoryLayoutGroup;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Inventory Categories")]
        [SerializeField] private List<InventoryCategoryView> _categories;

        private Dictionary<InventoryItemTypes, InventoryCategoryView> _registeredCategories;
        private Dictionary<Guid, IInventoryItem> _registeredInventoryItems;

        void Start()
        {
            InventoryItemView.RequestInventoryLayoutUpdateEvent += UpdateLayout;
        }

        public void Init(List<IInventoryItem> items)
        {
            _registeredInventoryItems = new Dictionary<Guid, IInventoryItem>();
            
            //register categories to dictionary if null
            if (_registeredCategories == null)
            {
                _registeredCategories = new Dictionary<InventoryItemTypes, InventoryCategoryView>();

                foreach (var category in _categories)
                {
                    _registeredCategories.Add(category.GetCategoryType(), category);
                }
            }

            //initialize items
            RegisterItemsToInventory(items);
            SetSubscriptions();
        }

        private void SetSubscriptions()
        {
        }

        private void ReleaseSubscriptions()
        {
        }


        public void ResetViewsForDisabling()
        {
            foreach (var category in _registeredCategories)
            {
                category.Value.DisableAllViews();
            }
        }

        private void RegisterItemsToInventory(List<IInventoryItem> items)
        {
            foreach (var item in items)
            {
                var itemData = item.GetItemData<ScriptableItemData>();
                var itemType = itemData._itemType;

                if (_registeredCategories.TryGetValue(itemType, out var categoryList))
                {
                    if (CheckIfItemAlreadyExists(item.Id))
                        return;
                    
                    categoryList.AddItemToCategory(_inventoryItemViewRef, ConvertItemDataToUIData(itemData, item.Id));
                    _registeredInventoryItems.Add(item.Id, item);
                }
            }
        }

        private void RemoveItemWithGuid(Guid itemId)
        {
            _registeredInventoryItems.Remove(itemId);
        }

        private InventoryItemViewData ConvertItemDataToUIData<T>(T data, Guid id) where T : ScriptableItemData
        {
            var uiData = new InventoryItemViewData();
            uiData.ItemName = data._itemName;
            uiData.ItemInfo = data._itemDesc;
            uiData.ItemSprite = data._itemSprite;
            uiData.ItemWeight = "undefined weight";
            uiData.ItemCount = "x1"; //todo 
            uiData.Data = data; //may not be needed.
            uiData.UniqueId = id;

            return uiData;
        }

        public InventoryCategoryView GetCategory(InventoryItemTypes itemType)
        {
            InventoryCategoryView categoryView = null;

            if (_registeredCategories.TryGetValue(itemType, out var foundCategoryView))
            {
                categoryView = foundCategoryView;
            }
            else
            {
                Debug.LogWarning("category type is null");
            }

            return categoryView;
        }

        public bool GetItemsOfTheCategory(InventoryItemTypes itemType, out List<InventoryItemView> listedItems)
        {
            var categoryView = GetCategory(itemType);
            listedItems = null;

            if (categoryView == null)
                return false;

            listedItems = categoryView.GetRegisteredItems();

            if (listedItems.Count == 0) return false;
            else return true;
        }

        private bool CheckIfItemAlreadyExists(Guid itemId)
        {
            return _registeredInventoryItems.ContainsKey(itemId);
        }

        public void UpdateLayout()
        {
            //todo optimize this laters

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

            /*layout.CalculateLayoutInputVertical();
            layout.CalculateLayoutInputHorizontal();
            layout.SetLayoutVertical();
            layout.SetLayoutHorizontal();*/

            _inventoryLayoutGroup.CalculateLayoutInputVertical();
            _inventoryLayoutGroup.CalculateLayoutInputHorizontal();
            _inventoryLayoutGroup.SetLayoutVertical();
            _inventoryLayoutGroup.SetLayoutHorizontal();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_inventoryLayoutGroup.GetComponent<RectTransform>());
        }
    }
}