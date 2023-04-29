using System;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public struct InventoryItemViewData
    {
        public Sprite ItemSprite;
        public string ItemName;
        public string ItemInfo;
        public string ItemWeight;
        public string ItemCount;
        public ItemDefinitionData DefinitionData;
    }
    
    public class InventoryItemView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform _viewTransform;
        [SerializeField] private Image _viewFrame;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private TextMeshProUGUI _itemInfo;
        [SerializeField] private TextMeshProUGUI _itemWeight;
        [SerializeField] private TextMeshProUGUI _itemCount;
        [SerializeField] private TextMeshProUGUI _isOpenText;
        [Header("Buttons")]
        [SerializeField] private Button _lookButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _dropButton;
        [SerializeField] private Button _useButton; //todo replace equip with this if the object is usable/consumable
        [Header("View Parameters")]
        [SerializeField] private Vector3 _idleSize;
        [SerializeField] private Vector3 _activeSize;
        [SerializeField] private Color _idleColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _hoverColor;
        

        private readonly string _closedStateTxt = "[ + ]";
        private readonly string _openStateTxt = "[ - ]";
        private bool _isSelected = false;
        private InventoryCategoryView _itemCategory;
        private LayoutGroup _categoryLayout;
        
        public static Action<InventoryItemView> InventoryItemViewWasSelectedEvent;
        public static Action RequestInventoryLayoutUpdateEvent;

        private void Start()
        {
            InventoryItemViewWasSelectedEvent += OnAnInventoryItemWasSelected;
        }

        private void OnAnInventoryItemWasSelected(InventoryItemView view)
        {
            if (view.GetInstanceID() != this.GetInstanceID())
            {
                SetSelectedState(false);
                RequestInventoryLayoutUpdateEvent?.Invoke();
            }
            else
                SetSelectedState(true);
        }

        public void InitItemView(InventoryItemViewData data)
        {
            _itemImage.sprite = data.ItemSprite;
            _itemName.text = data.ItemName;
            _itemInfo.text = data.ItemInfo;
            _itemCount.text = data.ItemCount;

            _itemCategory = GetComponentInParent<InventoryCategoryView>();
            _categoryLayout = _itemCategory.GetComponent<LayoutGroup>();
            SetSubscriptions();
        }

        private void OnDisable()
        {
            ReleaseSubscriptions();
        }

        public void SetSelectedState(bool isSelected)
        {
            //todo do how it looks when selected.
            _isOpenText.text = isSelected ? _openStateTxt : _closedStateTxt;
            _viewTransform.sizeDelta = isSelected ? _activeSize : _idleSize;
            _viewFrame.color = isSelected ? _activeColor : _idleColor;
            
            _lookButton.gameObject.SetActive(isSelected);
            _equipButton.gameObject.SetActive(isSelected);
            _dropButton.gameObject.SetActive(isSelected);

            _isSelected = isSelected;
        }

        private void SetSubscriptions()
        {
            _lookButton.onClick.AddListener(OnLookClicked);
            _dropButton.onClick.AddListener(OnDropClicked);
            _equipButton.onClick.AddListener(OnEquipClicked);
        }
        
        private void ReleaseSubscriptions()
        {
            _lookButton.onClick.RemoveListener(OnLookClicked);
            _dropButton.onClick.RemoveListener(OnDropClicked);
            _equipButton.onClick.RemoveListener(OnEquipClicked);
        }
        
        private void OnEquipClicked()
        {
            //todo - if the equip slot is free directly equip this and remove from inventory
            //todo - if the equip slot is not free replace that item with this item and update inventory
            
        }

        private void OnDropClicked()
        {
        }

        private void OnLookClicked()
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isSelected)
            {
                SetSelectedState(false);
                RequestInventoryLayoutUpdateEvent?.Invoke();
                return;
            }
            
            InventoryItemViewWasSelectedEvent?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isSelected == false)
                _viewFrame.color = _hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected == false)
                _viewFrame.color = _idleColor;
        }
    }
}
