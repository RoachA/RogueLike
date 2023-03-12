using System;
using UnityEngine;

namespace Game.Entites.Data
{
    [CreateAssetMenu(fileName = "WEAPON_DATA", menuName = "Item Data/Weapon Data", order = 1)]
    public class MeleeWeaponData : ItemData
    {
        public MeleeWeaponStats WeaponStats;
        
        [Serializable]
        public class MeleeWeaponStats
        {
            public int ArmorPenetration;
            public Dice.Dice BaseDmg;
        }
    }
}
