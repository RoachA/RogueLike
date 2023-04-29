using System;
using Game.Entites;
using UnityEngine;

namespace Game.Data
{
    public class ItemDefinitionData : ScriptableObject
    {
        [SerializeField] public InventoryItemTypes _itemType;
        [SerializeField] public string _itemIdentifier;
        [SerializeField] public Sprite _itemSprite;
        [SerializeField] public string _itemName;
        [SerializeField] public string _itemDesc;
        [SerializeField] public float _baseValue;
        [SerializeField] public int _durability;
    }

    [Serializable]
    public class RangedWeaponData
    {
        public Dice.Dice BaseDmg;
        public int ShotsPerAction;
        public int AmmoPerAction;
        public int AmmoType;
        public int MaxAmmo;
        public int Accuracy;
    }

    //todo replace inventory data and ui data are replaced with this one. keep guid for comparisons.
    public class ItemData<T> : IEquatable<ItemData<T>> where T : ItemDefinitionData
    {
        public T DefinitionData;
        public Guid Id;

        public ItemData(T definitionData) 
        {
            DefinitionData = definitionData;
            GenerateHashId();
        }
        
        public bool Equals(ItemData<T> other)
        {
            if (other == null)
                return false;
            
            bool isEqual = other.Id == Id;

            return isEqual;
        }
        
        protected void GenerateHashId()
        {
            Id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return Id;
        }
    }
}
