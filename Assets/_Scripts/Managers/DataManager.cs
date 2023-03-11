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
      public static DynamicEntityDataSet GetNpcRegistries()
      {
         var entityResource = (Resources.LoadAll<DynamicEntityDataSet>(ResourcesHelper.ScriptableNpcPath));

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
