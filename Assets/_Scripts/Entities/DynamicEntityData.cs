using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites.Data
{
    [Serializable]
    public class DynamicEntityData : ScriptableObject
    {
        [PropertyOrder(-1)]
        public string Name = "npc";

        [Multiline(10)]
        [PropertyOrder(-1)] public string Description = "";
        [PropertyOrder(-1)] public Sprite Sprite;

        [BoxGroup("Stats Section")] [PropertyOrder(2)] [SerializeField]
        public StatsData StatsData;

        /*public void CreateNewStatsData()
        {
            StatsData = new StatsData();
        }*/
    }
}