using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Game.UI
{
    public enum LookableType
    {
        Generic,
        Weapon,
        Gear,
        Consumable,
        Actor,
        Tile,
    }
    
    public interface ILookable
    {
        public LookableType MyLookableType { get; set; }
    }
    
    public class LookPopupProperties : UIProperties
    {
        public List<ILookable> Lookables;

        public LookPopupProperties(List<ILookable> lookables)
        {
            Lookables = lookables;
        }
    }

    public class LookPopup : UIElement
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Button _closeBtn;
        [SerializeField] private LookupDetailsView _detailsView;
        [SerializeField] private LookUpListView _listView;
        
        public override void Init<T>(T uiProperties = default)
        {
            base.Init(uiProperties);
            _container.SetActive(false);
        }

        public override void Open<T, T1>(Type uiType, T1 property)
        {
            base.Open<T, T1>(uiType, property);
            _closeBtn.onClick.AddListener(OnClose);
            
            if (property is LookPopupProperties data)
            {
                _container.SetActive(true);
                _listView.InitLookUpItems(data.Lookables);
            }
        }

        public override void ForceUpdateUI<T, T1>(Type uiType, T1 property)
        {
            if (property is LookPopupProperties data)
            {
            }
        }

        private void OnClose()
        {
            _closeBtn.onClick.RemoveListener(OnClose);
            _container.SetActive(false);
        }

        public override void Close<T>(Type uiElement)
        {
            base.Close<T>(uiElement);
        }
    }
}
