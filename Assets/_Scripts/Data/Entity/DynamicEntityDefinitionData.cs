using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entities.Data
{
    [Serializable]
    public class DynamicEntityDefinitionData
    {
        [PropertyOrder(-3)] public string Identifier = "npc";
        [PropertyOrder(-2)] public string _entityName;
        [Multiline(10)] [PropertyOrder(-1)] public string Description = "";
        [PropertyOrder(-1)] public Sprite Sprite;
        [PropertyOrder(-1)] public Races race;
        [PropertyOrder(-1)] public Genders Genders;
        [PropertyOrder(-1)] public Clans clan;
        [PropertyOrder(-1)] public Classes characterClass;

        public DynamicEntityDefinitionData()
        {
        }

        public DynamicEntityDefinitionData(DynamicEntityDefinitionData data)
        {
            Identifier = data.Identifier;
            _entityName = data._entityName;
            Description = data.Description;
            Sprite = data.Sprite;
            race = data.race;
            Genders = data.Genders;
            clan = data.clan;
            characterClass = data.characterClass;
        }
    }
}