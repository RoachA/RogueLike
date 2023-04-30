using System;
using Game.Entities;
using UnityEngine;

namespace Game.Data
{
    public class ScriptableItemData : ScriptableObject
    {
        [SerializeField] public InventoryItemTypes _itemType;
        [SerializeField] public string _itemIdentifier;
        [SerializeField] public Sprite _itemSprite;
        [SerializeField] public string _itemName;
        [SerializeField] public string _itemDesc;
        [SerializeField] public float _baseValue;
        [SerializeField] public int _durability;
    }

    [Serializable] // todo migrate laters
    public class RangedWeaponData
    {
        public Dice.Dice BaseDmg;
        public int ShotsPerAction;
        public int AmmoPerAction;
        public int AmmoType;
        public int MaxAmmo;
        public int Accuracy;
    }
}
