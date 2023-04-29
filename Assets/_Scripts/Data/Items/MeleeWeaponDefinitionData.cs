using System;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "MELEE_WEAPON_DATA", menuName = "Item Data/Melee Weapon Data", order = 1)]
    public class MeleeWeaponDefinitionData : ItemDefinitionData
    {
        public MeleeWeaponStats Stats;
        
        [Serializable]
        public class MeleeWeaponStats
        {
            public int ArmorPenetration;
            public Dice.Dice BaseDmg;
        }
    }
}
