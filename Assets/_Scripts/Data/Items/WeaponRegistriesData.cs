using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "WEAPON_REGISTRY", menuName = "Item Data/WeaponData", order = 1)]
public class WeaponRegistriesData : ScriptableObject
{
   [SerializeField] private List<MeleeWeaponDefinitionData> _weaponRegistry;
   
   [Button]
   private void LoadEntitiesFromResources()
   {
      var weaponRegistry = Resources.LoadAll<MeleeWeaponDefinitionData>(ResourceHelper.MeleeWeaponsPath).ToList();
      _weaponRegistry.Clear();
      _weaponRegistry = weaponRegistry;
   }

   public MeleeWeaponDefinitionData GetMeeleeWeaponDataAtIndex(int index)
   {
      return _weaponRegistry[index];
   }
}
