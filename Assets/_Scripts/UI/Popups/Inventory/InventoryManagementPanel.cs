using System.Collections;
using System.Collections.Generic;
using Game.Entites;
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
        
        void Start()
        {
            InventoryItemView.RequestInventoryLayoutUpdateEvent += UpdateLayout; 
        }

        public void Init(List<ItemEntity> items)
        {
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
        }

        public void ResetViewsForDisabling()
        {
            foreach (var categoryList in _registeredCategories)
            {
                categoryList.Value.DisableAllViews();
            }
        }

        private void RegisterItemsToInventory(List<ItemEntity> items)
        {
            //todo check how many views already exist. reuse if any blank. don't delete.
            foreach (var item in items)
            {
                var itemData = item.GetItemData<ItemData>();
                var itemType = itemData._itemType;
                
                if (_registeredCategories.TryGetValue(itemType, out var categoryList))
                {
                    categoryList.AddItemToCategory(_inventoryItemViewRef, ConvertItemDataToUIData(itemData));
                }
            }
        }

        private InventoryItemViewData ConvertItemDataToUIData<T>(T data) where T : ItemData
        {
            var uiData = new InventoryItemViewData();
            uiData.ItemName = data._itemName;
            uiData.ItemInfo = data._itemDesc;
            uiData.ItemSprite = data._itemSprite;
            uiData.ItemWeight = "undefined weight";
            uiData.ItemCount = "1"; //todo 

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