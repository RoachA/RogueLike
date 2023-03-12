using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites.Data
{
    [Serializable]
    public class DynamicEntityScriptableData : ScriptableObject
    {
        [BoxGroup("Stats Section")]
        [PropertyOrder(2)]
        public DynamicEntityStatsData _dynamicEntityStatsData;
        [PropertyOrder(2)]
        public DynamicEntityDefinitionData _dynamicEntityDefinitionData;
    }
}