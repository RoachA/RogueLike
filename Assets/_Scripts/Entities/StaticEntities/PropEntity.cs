using System.Collections.Generic;
using Game.Data;
using Game.Interfaces;
using Game.UI;
using UnityEngine;

namespace Game.Entites
{
    public class PropEntity : EntityStatic, IInteractable
    {
        public PropEntityData Data;
        public List<ItemData> ContainedItems;
        public string InteractionResultLog { get; set; }

        private PointLightView _pointLightView;
        private bool _isContainer;

        public void InitProp()
        {
            _spriteRenderer.sprite = Data.Sprite[0];
            _entityType = EntityType.item;
            _identifier = Data.Identifier;

            if (Data.GetType() == typeof(ContainerEntityData))
            {
                InitContainer();
                _isContainer = true;
            }

            _pointLightView = GetComponentInChildren<PointLightView>();
        }

        public override void SetLight(Color color)
        {
            base.SetLight(color);
            bool isActive = color.r > 0.5f; // this depends on shadow casting's color bottom limit. hard coded now.
            
           if (_pointLightView != null)
               _pointLightView.SetLightState(isActive);
        }

        public string InteractWithThis()
        {
            if (_isContainer)
            {
                UIElement.OpenUiSignal(typeof(ContainerPopup),
                new ContainerPopupProperties(ContainedItems, Data));
            }

            return Data.InteractionFeedback;
        }

        public void InitContainer()
        {
            ContainerEntityData data = Data as ContainerEntityData;
            var randomWearables = new List<WearableItemData>();
            var rndCount = UnityEngine.Random.Range(1, 5);

            if (data.ContainerType == ContainerTypes.Clothes)
            {
                randomWearables = ItemSpawnHelper.GetRandomWearableItems(rndCount);
                foreach (var item in randomWearables)
                {
                    Debug.Log("adds item: " + item.name);
                    ContainedItems.Add(item);
                }
            }
        }
    }
}
