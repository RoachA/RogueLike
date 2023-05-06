using Game.Data;
using Game.Entities.Data;
using Game.Tiles;
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
        private IPointerDownHandler _pointerDownHandlerImplementation;
        //todo keep data here as well!

        public void InitView(ILookable lookableItem)
        {
            FlushView();
            
            if (lookableItem.MyLookableType == LookableType.Actor)
            {
                DynamicEntityScriptableData data;
                data = LookupHelper.GetActorData<DynamicEntityScriptableData>(lookableItem);
                SetView(data._dynamicEntityDefinitionData.Sprite, data._dynamicEntityDefinitionData._entityName);
            }
            
            if (lookableItem.MyLookableType == LookableType.Gear)
            {
                WearableScriptableItemData data;
                data = LookupHelper.GetItemData<WearableScriptableItemData>(lookableItem);
                SetView(data._itemSprite, data._itemName);
            }
            
            if (lookableItem.MyLookableType == LookableType.Weapon)
            {
                MeleeWeaponScriptableData data;
                data = LookupHelper.GetItemData<MeleeWeaponScriptableData>(lookableItem);
                SetView(data._itemSprite, data._itemName);
            }
            
            if (lookableItem.MyLookableType == LookableType.Tile)
            {
                TileTypeData data;
                data = LookupHelper.GetTileData<TileTypeData>(lookableItem);
                SetView(data.TileSprite_A, data.TileName);
            }

            if (lookableItem.MyLookableType == LookableType.Consumable)
            {
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
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //show details view for this view
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
