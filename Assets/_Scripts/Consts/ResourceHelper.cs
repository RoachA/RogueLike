using System.Linq;
using Game.Entites;
using UnityEngine;

namespace Game.Managers
{
   public static class ResourceHelper
   {
      public static string EntitiesPath = "Entities";
      public static string PlayerEntityPath = "Entities/PlayerEntity";
      public static string TilesPath = "Tiles";
      public static string NpcEntityPath = "Entities/NpcEntity";
      public static string ItemEntityPath = "Entities/ItemEntity";
      public static string PropEntityPath = "Entities/PropEntity";
      public static string ScriptableNpcPath = "ScriptableObjects/NPC";
      public static string WeaponsPath = "ScriptableObjects/Items/Weapons/Meelee/";
      public static string SpritesPath = "Sprites";
      public static string PlayerSpriteTmp = "/game_sprites";
      public static string WeaponsRegistryPath = "ScriptableObjects/Items/Weapons/";
      public static string MeleeWeaponsPath = "ScriptableObjects/Items/Weapons/Melee";
      public static string ItemEntityTemplate = "/ItemEntity";
      public static string MeeleeItemEntity = "/MeeleeWeaponEntity";
      public static string PropsDataPath = "ScriptableObjects/Items/Props";
   }
}
