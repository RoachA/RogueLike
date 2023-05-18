using System;
using Game.Data;
using Game.Interfaces;
using Game.UI;
using UnityEngine;

namespace Game.Entities
{
   public class ItemEntityView : EntityStatic, IInteractable
   {
      [SerializeField] public ScriptableItemData _itemData;

      public override void Init(ScriptableItemData data, Guid guid = default)
      {
         _spriteRenderer.sprite = data._itemSprite;
         base.Init(guid);
         _itemData = data;
         SetLookableType();
      }

      private void SetLookableType()
      {
         if (_itemData.GetType() == typeof(MeleeWeaponScriptableData) || (_itemData.GetType() == typeof(RangedWeaponData)))
            MyLookableType = LookableType.Weapon;
         if (_itemData.GetType() == typeof(WearableScriptableItemData))
            MyLookableType = LookableType.Gear;
         
         //todo add other types here in the future
      }

      public string InteractionResultLog { get; set; }
      
      public string InteractWithThis()
      {
         var log = "interacted with " + _itemData._itemName;
         //todo call simple popup and prompt if player wants to grab it or equip it etc.
         return log;
      }
   }
}
