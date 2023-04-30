using System.Collections.Generic;
using System.Linq;
using Game.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data
{
   [CreateAssetMenu(fileName = "WEAPON_REGISTRY", menuName = "Item Data/WeaponData", order = 1)]
   public class WeaponRegistriesData : ScriptableObject
   {
      [SerializeField] private List<MeleeWeaponScriptableData> _weaponRegistry;

      [Button]
      private void LoadEntitiesFromResources()
      {
         var weaponRegistry = Resources.LoadAll<MeleeWeaponScriptableData>(ResourceHelper.MeleeWeaponsPath).ToList();
         _weaponRegistry.Clear();
         _weaponRegistry = weaponRegistry;
      }

      public MeleeWeaponScriptableData GetMeleeWeaponDataAtIndex(int index)
      {
         return _weaponRegistry[index];
      }
   }
}
