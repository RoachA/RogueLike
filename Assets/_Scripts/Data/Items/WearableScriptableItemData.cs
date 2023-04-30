using System;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "WEARABLE_DATA", menuName = "Item Data/Wearable Data", order = 1)]
    public class WearableScriptableItemData : ScriptableItemData
    {
        public WearableItemStats Stats;
        
        [Serializable]
        public class WearableItemStats
        {
            public int AV;
            public int DV;
        }
    }
}
