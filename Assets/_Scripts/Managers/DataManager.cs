using System;
using Game.Entites.Data;
using UnityEngine;

namespace Game.Data
{
   public static class DataManager
   {
      static DataManager()
      {
         GetNpcRegistries();
      }

      public static PlayerEntityScriptableData GetPlayerData()
      {
         //todo this would pull the serialized data later on.
         return null;
      }

      public static PlayerEntityScriptableData GenerateMiscPlayerData()
      {
         var newplayer = new PlayerEntityScriptableData();
         newplayer.GenerateStarterPlayerData();
         return newplayer;
      }
      
      public static DynamicEntityScriptableDataSet GetNpcRegistries()
      {
         var entityResource = (Resources.LoadAll<DynamicEntityScriptableDataSet>(ResourcesHelper.ScriptableNpcPath));

         if (entityResource != null)
            return entityResource[0];
         else
         {
            Debug.LogError("No registries found in: " + ResourcesHelper.ScriptableNpcPath);
            return null;
         }
      }
   }
}
