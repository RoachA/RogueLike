using System;
namespace Game.Entites.Data
{
    [Serializable]
    public class DynamicEntityBaseData
    {
        public BaseStatsData BaseStatsData;
        public DynamicEntityDefinitionData DefinitionData;

        public DynamicEntityBaseData(BaseStatsData stats, DynamicEntityDefinitionData definition)
        {
            BaseStatsData = stats;
            DefinitionData = definition;
        }
    }
}
