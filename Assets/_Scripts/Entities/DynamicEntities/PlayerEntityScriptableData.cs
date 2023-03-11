using UnityEngine;

namespace Game.Entites.Data
{
    public class PlayerEntityScriptableData
    {
        private BaseStatsData _dynamicEntityStatsData;
        private DynamicEntityDefinitionData _dynamicEntityDefinitionData;
        /// <summary>
        /// This is a debug constructor. for testing. later on we should read data, and apply, preferably depending
        /// clans, classes etc.
        /// </summary>
        ///
        public BaseStatsData GenerateStarterStats()
        {
            _dynamicEntityStatsData.AV = 10;
            _dynamicEntityStatsData.HP = 10;
            _dynamicEntityStatsData.LVL = 1;
            _dynamicEntityStatsData.MHP = 10;
            _dynamicEntityStatsData.ATK = 10;

            return _dynamicEntityStatsData;
        }

        public DynamicEntityDefinitionData GenerateStarterDefinition()
        {
            _dynamicEntityDefinitionData.clan = Clans.gorgonian;
            _dynamicEntityDefinitionData.race = Races.other;
            _dynamicEntityDefinitionData._entityName = "player vakkas";
            _dynamicEntityDefinitionData.characterClass = classes.dallama;
            _dynamicEntityDefinitionData.Genders = Genders.gay;
            _dynamicEntityDefinitionData.Sprite = Resources.Load<Sprite>(ResourcesHelper.ScriptableNpcPath);
            return _dynamicEntityDefinitionData;
        }

        public void SetStat(BaseStatTypes stat, int newVal)
        {
            _dynamicEntityStatsData.SetStat(stat, newVal);
        }
    }
}
