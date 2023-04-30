using UnityEngine;
using System.Collections.Generic;
using Game.Data;

namespace Game.Entities
{
    [CreateAssetMenu(fileName = "CONTAINER_DATA", menuName = "Environment Data/Container", order = 1)]
    public class ContainerEntityData : PropEntityData
    {
        public int ContainerSize;
        public List<ScriptableItemData> ContainedItems;
        public ContainerTypes ContainerType;

        public ContainerEntityData(string identifier, string name, string desc, PropOrientationType orientationType,
            Sprite[] sprite, GameObject additionalItem, float baseValue, float baseWeight, int containerSize,
            List<ScriptableItemData> containedItems, ContainerTypes containerType, string interactionFeedback) : base(identifier, interactionFeedback, name, desc, orientationType, sprite, additionalItem, baseValue,
            baseWeight)
        {
            ContainerSize = containerSize;
            ContainedItems = containedItems;
            ContainerType = containerType;
        }
    }

    public enum ContainerTypes
    {
        Mixed,
        Clothes,
        Weapons,
        Food,
    }
}