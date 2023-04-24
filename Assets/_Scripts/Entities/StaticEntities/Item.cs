using UnityEngine;

namespace Game.Data
{
    public interface IInventoryItem
    {
        public T GetItemData<T>() where T : ItemData;
    }
    
    public class Item : ItemBase, IInventoryItem
    {
        [Header("Data")]
        [SerializeField] protected ItemData _itemData;
        [SerializeField] protected bool _isContained = true;

        public Item(Item init)
        {
            _itemData = init._itemData;
            _isContained = init._isContained;
        }

        protected Item()
        {
        }

        public void SetAsContained(bool isContained)
        {
            _isContained = isContained;
            _spriteRenderer.enabled = _isContained == false;
        }

        public bool GetIsContained()
        {
            return _isContained;
        }
        
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