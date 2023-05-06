using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Data;
using Game.Entities;

namespace Game.UI
{
    public class ContainerPopupProperties : UIProperties
    {
        public List<ScriptableItemData> ContainedItemsList;
        public PropEntityData PropData;

        public ContainerPopupProperties(List<ScriptableItemData> containedItemsList, PropEntityData propData)
        {
            ContainedItemsList = containedItemsList;
            PropData = propData;
        }
    }
    
    public class ContainerPopup : PopUpBase
    {
        [Header("Container Assets")]
        [SerializeField] private ContainerUIItemView _containerItemViewTemplate;
        [SerializeField] private List<ContainerUIItemView> _availableTemplates;
        [SerializeField] private Transform _templatesHolder;
        [SerializeField] private Button _takeAllBtn;
        [SerializeField] private Button _storeItemBtn;

        public override void Open<T, T1>(Type uiType, T1 property)
        {
            base.Open<T, T1>(uiType, property);
            
            _takeAllBtn.onClick.AddListener(OnTakeAll);
            _storeItemBtn.onClick.AddListener(OnStoreItem);

            if (property is ContainerPopupProperties data)
            {
                SetHeaderText(data.PropData.Name);
                SetInfoText(data.PropData.Desc);
                SetSprite(data.PropData.Sprite[0]);
                SetContainerViews(data.ContainedItemsList);
            }
        }

        public override void Close<T>(Type uiElement)
        {
            //todo the other texts cause noise when closed, must be disabled.
            base.Close<T>(uiElement);
            _takeAllBtn.onClick.RemoveListener(OnTakeAll);
            _storeItemBtn.onClick.RemoveListener(OnStoreItem);
        }

        private void SetContainerViews(List<ScriptableItemData> items)
        {
            var availabeCount = _availableTemplates.Count;
            var neededCount = items.Count;
            
            if (availabeCount < neededCount)
            {
                for (int i = availabeCount; i <= neededCount; i++)
                {
                    var newView = Instantiate(_containerItemViewTemplate, _templatesHolder);
                    _availableTemplates.Add(newView);
                }
            }
            
            for (int i = 0; i < _availableTemplates.Count; i++)
            {
                if (i >= items.Count)
                {
                    _availableTemplates[i].gameObject.SetActive(false);
                    continue;
                }
                
                var item = items[i];
                _availableTemplates[i].gameObject.SetActive(true);
                _availableTemplates[i].Init(new InventoryDefinition(item._itemName, 1, item._itemSprite));
            }
        }

        private void OnStoreItem()
        {
            
        }

        private void OnTakeAll()
        {
         
        }
    }
}
