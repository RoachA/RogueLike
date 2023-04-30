using System;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Entities
{
   public class ItemEntityView : EntityStatic, IInteractable
   {
      [SerializeField] public ScriptableItemData _itemData;

      public override void Init(Guid guid = default)
      {
         _spriteRenderer.sprite = _itemData._itemSprite;
         base.Init(guid);
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
