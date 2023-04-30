using System;
using UnityEngine;

namespace Game.Data
{
    public interface IInventoryItem : IEquatable<IInventoryItem>
    {
        public Guid Id { get; set; }

        public T GetItemData<T>() where T : ScriptableItemData;
    }
    
    public class ItemData : IInventoryItem
    {
        public Guid Id { get; set; }
        [Header("Data")]
        [SerializeField] public ScriptableItemData ScriptableItemData;
        [SerializeField] public bool _isContained = true;

        public ItemData(ScriptableItemData data, bool isContained)
        {
            ScriptableItemData = data;
            _isContained = isContained;
            GenerateHashId();
        }

        public ItemData()
        {
        }
        
        public void SetAsContained(bool isContained)
        {
            _isContained = isContained;
        }

        public bool GetIsContained()
        {
            return _isContained;
        }
        
        public void SetItemData(ScriptableItemData data)
        {
            ScriptableItemData = data;
        }
        
        public T GetItemData<T>() where T : ScriptableItemData
        {
            if (ScriptableItemData == null)
            {
                Debug.LogError("no item data was found in this entity!");
                return null;
            }
            
            return (T) ScriptableItemData;
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
    }
}