using System.Collections;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

namespace Game.Entites
{
    public class ItemEntity : StaticEntityBase
    {
        [SerializeField] protected ItemData _itemData;
        [SerializeField] protected string _itemName;
        [SerializeField] protected float _weight;

        public void SetItemData(ItemData data)
        {
            _itemData = data;
        }

        public ItemData GetItemData()
        {
            if (_itemData == null)
            {
                Debug.LogError("no item data was found in this entity!");
                return null;
            }
            
            return _itemData;
        }
    }
}