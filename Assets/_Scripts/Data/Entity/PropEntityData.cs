using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    [CreateAssetMenu(fileName = "PROP_DATA", menuName = "Environment Data/Prop", order = 1)]
    public class PropEntityData : ScriptableObject
    {
        [Header("Identification")]
        public string Identifier;
        public string Name;
        public string Desc;
        public string InteractionFeedback;
        public PropOrientationType OrientationType;
        [Header("Looks")]
        public Sprite[] Sprite;
        public GameObject AdditionalItem;
        [Header("Stats")]
        public float BaseValue;
        public float BaseWeight;

        public PropEntityData(string identifier, string interactionFeedback, string name, string desc, PropOrientationType orientationType, Sprite[] sprite, GameObject additionalItem, float baseValue, float baseWeight)
        {
            Identifier = identifier;
            Name = name;
            Desc = desc;
            InteractionFeedback = interactionFeedback;
            OrientationType = orientationType;
            Sprite = sprite;
            AdditionalItem = additionalItem;
            BaseValue = baseValue;
            BaseWeight = baseWeight;
        }
    }
    
    public enum PropOrientationType
    {
        WallBound,
        Free,
    }
}