using System.Collections.Generic;
using System.Linq;
using Game.Entites.Data;
using Game.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "WEAPON_REGISTRY", menuName = "Item Data/WeaponData", order = 1)]
public class WeaponRegistriesData : ScriptableObject
{
   [SerializeField] private List<MeleeWeaponData> _weaponRegistry;
   
   [Button]
   private void LoadEntitiesFromResources()
   {
      var weaponRegistry = Resources.LoadAll<MeleeWeaponData>(ResourceHelper.MeleeWeaponsPath).ToList();
      _weaponRegistry.Clear();
      _weaponRegistry = weaponRegistry;
   }

   public MeleeWeaponData GetMeeleeWeaponDataAtIndex(int index)
   {
      return _weaponRegistry[index];
   }
}
