using System.Collections;
using System.Collections.Generic;
using Game.Entites;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField] private ItemTypes _itemType;
    [SerializeField] private string _itemIdentifier;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private float _baseValue;
}

public class MeeleeWeaponStats
{
    public int ArmorPenetration;
    public int BaseDmg;
}

public class RangedWeaponStats
{
    public int ShotsPerAction;
    public int AmmoPerAction;
    public int AmmoType;
    public int MaxAmmo;
    public int Accuracy;
}
