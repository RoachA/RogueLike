using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites
{
    [CreateAssetMenu(fileName = "PROP_DATA", menuName = "Environment Data/Prop", order = 1)]
    public class PropEntityData : ScriptableObject
    {
        [Header("Identification")]
        public string Identifier;
        public string Desc;
        public PropInteraction InteractionType;
        public PropOrientationType OrientationType;
        [Header("Looks")]
        public Sprite[] Sprite;
        public GameObject AdditionalItem;
        [Header("Stats")]
        public float BaseValue;
        public float BaseWeight;

        public PropEntityData(string identifier, string desc, PropInteraction interactionType, PropOrientationType orientationType, Sprite[] sprite, GameObject additionalItem, float baseValue, float baseWeight)
        {
            Identifier = identifier;
            Desc = desc;
            InteractionType = interactionType;
            OrientationType = orientationType;
            Sprite = sprite;
            AdditionalItem = additionalItem;
            BaseValue = baseValue;
            BaseWeight = baseWeight;
        }
    }

    [Serializable]
    public class PropInteraction
    {
        public PropInteractionType InteractionType;
        [ShowIf("InteractionType", PropInteractionType.FuncFeedback)]
        public string[] TxtFeedbacks;
        [ShowIf("InteractionType", PropInteractionType.FuncContainer)]
        public int ContainerSize;
        
        public PropInteraction(PropInteractionType interactionType)
        {
            InteractionType = interactionType;
        }
        
        public PropInteraction(PropInteractionType interactionType, string[] txtFeedbacks)
        {
            InteractionType = interactionType;
            TxtFeedbacks = txtFeedbacks;
        }
        
        public PropInteraction(PropInteractionType interactionType, int containerSize)
        {
            InteractionType = interactionType;
            ContainerSize = containerSize;
        }
    }

    public enum PropOrientationType
    {
        WallBound,
        Free,
    }

    public enum PropInteractionType
    {
        FuncSwitch,
        FuncFeedback,
        FuncInterface,
        FuncContainer,
        FuncNone,
    }
}