using System.Collections.Generic;
using Game.Entities;
using TMPro;
using UnityEngine;
namespace Game.UI
{
    public class InventoryCategoryView : MonoBehaviour
    {
        [SerializeField] private InventoryItemTypes _inventoryItem;
        [SerializeField] private TextMeshProUGUI _categoryMainLabel;
        [SerializeField] private TextMeshProUGUI _subCagetoryLabel;
        
        [SerializeField] private List<InventoryItemView> _items; //todo hide laters
        
        // Start is called before the first frame update

        public InventoryItemTypes GetCategoryType()
        {
            return _inventoryItem;
        }

        public List<InventoryItemView> GetRegisteredItems()
        {
            return _items;
        }

        public void ResetViews()
        {
            foreach (var item in _items)
            {
                item.FlushView();
                item.gameObject.SetActive(false);
            }
        }

        public void AddItemToCategory(InventoryItemView view, InventoryItemViewData data)
        {
            InventoryItemView newItemView = null;
            
            foreach (var registeredItem in _items)
            {
                if (_items == null || _items.Count == 0)
                {
                    _items = new List<InventoryItemView>();
                    continue;
                }
                
                if (registeredItem.gameObject.activeSelf == false)
                {
                    newItemView = registeredItem;
                    newItemView.InitItemView(data);
                    newItemView.gameObject.SetActive(true);
                    return;
                }
            }

            newItemView = Instantiate(view, transform);
            _items.Add(newItemView);
            newItemView.InitItemView(data);
        }

        public void DisableAllViews()
        {
            foreach (var item in _items)
            {
                ResetViews();
            }
        }
        
        public void Init()
        {
        }
    }
}
