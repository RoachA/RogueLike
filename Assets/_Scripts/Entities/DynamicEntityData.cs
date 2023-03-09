using System;
using UnityEngine;

namespace Game.Entites
{
    [CreateAssetMenu(fileName = "dynamicEntity_DATA", menuName = "ScriptableEntities/Dynamic Entity Data", order = 1)]
    [Serializable]
    public class DynamicEntityData : ScriptableObject
    {
        public string _npcName = "npc";
        public EntityNpc.EntityDemeanor _demeanor;
        public int _aggroRadius;
        [Space] public int _hp;
        public int _energy;
        [Space] public int _str;
        public int _agi;
        public int _toughness;
        public int _int;
        public int _chr;
        public int _wp;
    }
}

