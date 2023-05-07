using System;
using Game.Data;
using Game.Entities;
using Game.Entities.Data;
using Game.UI.Helper;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class LookUpListItemView : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _frame;
        [SerializeField] private Color _activeColor, _inactiveColor;
        //todo keep data here as well!
        
        private object _cachedData;
        private LookableType _cachedType;

        public static Action<EntityDynamic> _lookActor;
        public static Action<WearableScriptableItemData> _lookGear;
        public static Action<MeleeWeaponScriptableData> _lookMeleeWeapon;
        public static Action<TileTypeData> _lookTile;
        public static Action<PropEntityData> _lookProp;
        
        public void InitView(ILookable lookableItem)
        {
            FlushView();
            _cachedType = lookableItem.MyLookableType;
            
            if (lookableItem.MyLookableType == LookableType.Actor)
            {
                DynamicEntityScriptableData data;
                data = LookupHelper.GetActorData<DynamicEntityScriptableData>(lookableItem);
                SetView(data._dynamicEntityDefinitionData.Sprite, data._dynamicEntityDefinitionData._entityName);
                _cachedData = lookableItem as EntityDynamic;
            }
            
            if (lookableItem.MyLookableType == LookableType.Gear)
            {
                WearableScriptableItemData data;
                data = LookupHelper.GetItemData<WearableScriptableItemData>(lookableItem);
                SetView(data._itemSprite, data._itemName);
                _cachedData = data;
            }
            
            if (lookableItem.MyLookableType == LookableType.Weapon)
            {
                MeleeWeaponScriptableData data;
                data = LookupHelper.GetItemData<MeleeWeaponScriptableData>(lookableItem);
                SetView(data._itemSprite, data._itemName);
                _cachedData = data;
            }
            
            if (lookableItem.MyLookableType == LookableType.Tile)
            {
                TileTypeData data;
                data = LookupHelper.GetTileData(lookableItem);
                SetView(data.TileSprite_A, data.TileName);
                _cachedData = data;
            }

            if (lookableItem.MyLookableType == LookableType.Consumable)
            {
            }
            
            if (lookableItem.MyLookableType == LookableType.Prop)
            {
                PropEntityData data;
                data = LookupHelper.GetPropData(lookableItem);
                SetView(data.Sprite[0], data.Name); //todo why is this an array?
                _cachedData = data;
            }
            
            if (lookableItem.MyLookableType == LookableType.Generic)
            {
            }
        }

        private void SetView(Sprite itemSprite, string itemName)
        {
            _image.sprite = itemSprite;
            _name.text = itemName;
        }

        public void FlushView()
        {
            _image.sprite = null;
            _name.text = default;
            _cachedData = default;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_cachedType)
            {
                case LookableType.Generic:
                    break;
                case LookableType.Weapon:
                    _lookMeleeWeapon?.Invoke(_cachedData as MeleeWeaponScriptableData);
                    break;
                case LookableType.Gear:
                    _lookGear?.Invoke(_cachedData as WearableScriptableItemData);
                    break;
                case LookableType.Consumable:
                    break;
                case LookableType.Actor:
                    _lookActor?.Invoke(_cachedData as EntityDynamic);
                    break;
                case LookableType.Tile:
                    _lookTile?.Invoke(_cachedData as TileTypeData);
                    break;
                case LookableType.Prop:
                    _lookProp?.Invoke(_cachedData as PropEntityData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _frame.color = _activeColor;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _frame.color = _inactiveColor;   
        }
    }
}
