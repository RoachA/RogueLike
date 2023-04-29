using System;
using UnityEngine;

namespace Game.Data
{
    public interface IInventoryItem : IEquatable<IInventoryItem>
    {
        public Guid Id { get; set; }

        public T GetItemData<T>() where T : ItemDefinitionData;
    }
    
    public class Item : ItemBase, IInventoryItem
    {
        public Guid Id { get; set; }
        [Header("Data")]
        [SerializeField] protected ItemDefinitionData ItemDefinitionData;
        [SerializeField] protected bool _isContained = true;

        public Item(ItemDefinitionData definitionData, bool isContained)
        {
            ItemDefinitionData = definitionData;
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
        
        public void SetItemData(ItemDefinitionData definitionData)
        {
            ItemDefinitionData = definitionData;
        }
        
        public T GetItemData<T>() where T : ItemDefinitionData
        {
            if (ItemDefinitionData == null)
            {
                Debug.LogError("no item data was found in this entity!");
                return null;
            }
            
            return (T) ItemDefinitionData;
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