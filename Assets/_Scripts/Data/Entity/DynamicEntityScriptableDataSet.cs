using System;
using System.Collections.Generic;
using System.Linq;
using Game.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Entities.Data
{
   [CreateAssetMenu(fileName = "D_ENTITY_DATA_SET", menuName = "Data Set/Dynamic Entities Data Set", order = 1)]
   [Serializable]
   public class DynamicEntityScriptableDataSet : ScriptableObject
   {
      [SerializeField] public List<DynamicEntityScriptableData> _npcDefinitions;

      [Button]
      private void LoadEntitiesFromResources()
      {
         var npcDataSet = Resources.LoadAll<DynamicEntityScriptableData>(ResourceHelper.ScriptableNpcPath).ToList();
         _npcDefinitions.Clear();
         _npcDefinitions = npcDataSet;
      }
      
      //todo could add data managers and modifiers here perhaps? or need a helper outside... or in entity manager.
   }
}
