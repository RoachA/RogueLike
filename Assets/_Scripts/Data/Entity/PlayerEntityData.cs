using Game.Managers;
using UnityEngine;

namespace Game.Entites.Data
{
    /// <summary>
    /// Don't use this for player loading now
    /// </summary>
    public class PlayerEntityData : DynamicEntityBaseData
    {
        /// <summary>
        /// all set to 10 for now
        /// </summary>
        /// <returns></returns>
        public BaseStatsData GenerateStarterStats()
        {
            var newStatsData = new BaseStatsData
            (1, 0, 10, 10, 3, 2, 1, 1, 3, 1);

            return newStatsData;
        }

        public DynamicEntityDefinitionData GenerateStarterDefinition()
        {
            var definitionData = new DynamicEntityDefinitionData();
            
            definitionData.clan = Clans.gorgonian;
            definitionData.race = Races.other;
            definitionData._entityName = "player vakkas";
            definitionData.characterClass = classes.dallama;
            definitionData.Genders = Genders.gay;
            definitionData.Sprite = ResourceHelper.GetPlayerSprite(0);
            return definitionData;
        }
    }
}