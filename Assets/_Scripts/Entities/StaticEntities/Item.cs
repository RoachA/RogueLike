using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Data
{
    public interface IInventoryItem : IEquatable<IInventoryItem>
    {
        public Guid Id { get; set; }

        public T GetItemData<T>() where T : ItemData;
    }
    
    public class Item : ItemBase, IInventoryItem
    {
        public Guid Id { get; set; }
        [Header("Data")]
        [SerializeField] protected ItemData _itemData;
        [SerializeField] protected bool _isContained = true;

        public Item(ItemData data, bool isContained)
        {
            _itemData = data;
            _isContained = isContained;
            GenerateHashId();
        }

        public Item()
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

        protected void GenerateHashId()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(IInventoryItem other)
        {
            if (other == null) return false;
            return (this.Id == other.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}