using System;
using UnityEngine;

namespace Game.Entites.Data
{
    [CreateAssetMenu(fileName = "WEAPON_DATA", menuName = "Item Data/Weapon Data", order = 1)]
    public class MeeleeWeaponData : ItemData
    {
        public MeeleeWeaponStats WeaponStats;
        
        [Serializable]
        public class MeeleeWeaponStats
        {
            public int ArmorPenetration;
            public Dice.Dice BaseDmg;
        }
    }
}
