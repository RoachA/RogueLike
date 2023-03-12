using System;
using Game.Dice;
using Game.Entites;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField] public ItemTypes _itemType;
    [SerializeField] public string _itemIdentifier;
    [SerializeField] public string _itemName;
    [SerializeField] public Sprite _itemSprite;
    [SerializeField] public float _baseValue;
    [SerializeField] public int _durability;
}

[Serializable]
public class RangedWeaponData
{
    public Dice BaseDmg;
    public int ShotsPerAction;
    public int AmmoPerAction;
    public int AmmoType;
    public int MaxAmmo;
    public int Accuracy;
}
