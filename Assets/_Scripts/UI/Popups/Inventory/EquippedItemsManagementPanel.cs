using System.Collections.Generic;
using Game.Data;
using Game.Entities;
using UnityEngine;

namespace Game.UI
{
    public class EquippedItemsManagementPanel : MonoBehaviour
    {
        [Header("Equip References")]
        [SerializeField] private GameObject _equippedInfoContainer;
        [SerializeField] private List<EquipSlotView> _equipSlots;
        public Dictionary<EntityEquipSlots, IInventoryItem> EquippedItems;
        
        public void Init(Dictionary<EntityEquipSlots, IInventoryItem> equippedItems)
        {
            EquippedItems = equippedItems;
            UpdateSlotViews();
        }

        public void UpdateSlotViews()
        {
            foreach (var view in _equipSlots)
            {
                foreach (var item in EquippedItems)
                {
                    if (view != null && item.Value != null && view.Slot == item.Key)
                        view.InitEquipSlotView(item.Value.GetItemData<ScriptableItemData>());
                    else
                        view.InitEquipSlotView();
                }
            }
        }
    }
}
