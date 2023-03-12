using System.Collections.Generic;
using System.Linq;
using Game.Entites.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using ResourceManager = Game.Managers.ResourceManager;

[CreateAssetMenu(fileName = "WEAPON_REGISTRY", menuName = "Item Data/WeaponData", order = 1)]
public class WeaponRegistriesData : ScriptableObject
{
   [SerializeField] private List<MeleeWeaponData> _weaponRegistry;
   
   [Button]
   private void LoadEntitiesFromResources()
   {
      var weaponRegistry = Resources.LoadAll<MeleeWeaponData>(ResourceManager.MeleeWeaponsPath).ToList();
      _weaponRegistry.Clear();
      _weaponRegistry = weaponRegistry;
   }
}
