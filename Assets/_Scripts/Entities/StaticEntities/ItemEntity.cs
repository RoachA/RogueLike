using UnityEngine;

namespace Game.Entites
{
    public class ItemEntity : StaticEntityBase
    {
        [Header("Data")]
        [SerializeField] protected ItemData _itemData;
        [SerializeField] protected bool _isContained = true;

        public ItemEntity(ItemEntity init)
        {
            _itemData = init._itemData;
            _isContained = init._isContained;
        }

        protected ItemEntity()
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