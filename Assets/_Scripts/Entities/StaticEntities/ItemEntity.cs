using System.Collections;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

namespace Game.Entites
{
    public class ItemEntity : StaticEntityBase
    {
        [SerializeField] protected ItemData _itemData;

        public void SetItemData(ItemData data)
        {
            _itemData = data;
        }
        
        public T GetItemData<T>() where T : ItemData
        {
            if (_itemData == null)
            {
                Debug.LogError("no item data was found in this entity!");
                return null;
            }
            
            return (T) _itemData;
        }
    }
}