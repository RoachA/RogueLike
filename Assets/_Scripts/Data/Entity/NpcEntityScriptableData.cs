using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites.Data
{
    [CreateAssetMenu(fileName = "NPC_DATA", menuName = "Entity Data/Npc Data", order = 1)]
    [Serializable]
    public class NpcEntityScriptableData : DynamicEntityScriptableData
    {
        [PropertyOrder(0)] public EntityDemeanor _demeanor;
        [PropertyOrder(0)] public EntityBehaviorTypes _behaviorType;
        [PropertyOrder(1)] public int _aggroRadius;
    }
}
