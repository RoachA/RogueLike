using Game.Entites.Data;
using Game.Managers;
using UnityEngine;

namespace Game.Data
{
   public static class DataManager
   {
      static DataManager()
      {
         GetNpcRegistries();
      }
      

      public static PlayerEntityData GenerateStarterPlayerData()
      {
         var newPlayerData = new PlayerEntityData();
         newPlayerData.BaseStatsData = newPlayerData.GenerateStarterStats();
         newPlayerData.DefinitionData = newPlayerData.GenerateStarterDefinition();

         return newPlayerData;
      }
      
      
      public static DynamicEntityScriptableDataSet GetNpcRegistries()
      {
         var entityResource = (Resources.LoadAll<DynamicEntityScriptableDataSet>(ResourceManager.ScriptableNpcPath));

         if (entityResource != null)
            return entityResource[0];
         else
         {
            Debug.LogError("No registries found in: " + ResourceManager.ScriptableNpcPath);
            return null;
         }
      }
   }
}
