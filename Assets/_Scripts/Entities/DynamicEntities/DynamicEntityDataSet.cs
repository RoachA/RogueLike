using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entites.Data
{
   [CreateAssetMenu(fileName = "D_ENTITY_DATA_SET", menuName = "Data Set/Dynamic Entities Data Set", order = 1)]
   [Serializable]
   public class DynamicEntityDataSet : ScriptableObject
   {
      [SerializeField] public List<DynamicEntityData> _npcDefinitions;

      [Button]
      private void LoadEntitiesFromResources()
      {
         var npcDataSet = Resources.LoadAll<DynamicEntityData>(ResourcesHelper.ScriptableNpcPath).ToList();
         _npcDefinitions.Clear();
         _npcDefinitions = npcDataSet;
      }
      
      //todo could add data managers and modifiers here perhaps? or need a helper outside... or in entity manager.
   }
}
