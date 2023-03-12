using System.Linq;
using UnityEngine;

namespace Game.Managers
{
   public static class ResourceManager
   {
      public static string EntitiesPath = "Entities";
      public static string PlayerEntityPath = "Entities/PlayerEntity";
      public static string NpcEntityPath = "Entities/NpcEntity";
      public static string ScriptableNpcPath = "ScriptableObjects/NPC";
      public static string WeaponsPath = "ScriptableObjects/Items/Weapons/Meelee/";
      public static string SpritesPath = "Sprites";
      public static string PlayerSpriteTmp = "/game_sprites";
      public static string WeaponsRegistryPath = "ScriptableObjects/Items/Weapons/";
      public static string MeleeWeaponsPath = "ScriptableObjects/Items/Weapons/Melee";

      public static Sprite GetPlayerSprite(int index)
      {
         var sprites = Resources.LoadAll<Sprite>(SpritesPath + PlayerSpriteTmp);
         return sprites.FirstOrDefault(s => s.name == "player_" + index);
      }

      public static WeaponRegistriesData GetWeaponsRegistry()
      {
         var registry = Resources.LoadAll<WeaponRegistriesData>(WeaponsRegistryPath);
         return registry[0];
      }
   }
}
