using System.Linq;
using Game.Entites;
using UnityEngine;

namespace Game.Managers
{
   public static class ResourceHelper
   {
      public static string EntitiesPath = "Entities";
      public static string PlayerEntityPath = "Entities/PlayerEntity";
      public static string NpcEntityPath = "Entities/NpcEntity";
      public static string ItemEntityPath = "Entities/NpcEntity";
      public static string ScriptableNpcPath = "ScriptableObjects/NPC";
      public static string WeaponsPath = "ScriptableObjects/Items/Weapons/Meelee/";
      public static string SpritesPath = "Sprites";
      public static string PlayerSpriteTmp = "/game_sprites";
      public static string WeaponsRegistryPath = "ScriptableObjects/Items/Weapons/";
      public static string MeleeWeaponsPath = "ScriptableObjects/Items/Weapons/Melee";
      public static string ItemEntityTemplate = "/ItemEntity";
      public static string MeeleeItemEntity = "/MeeleeWeaponEntity";

      public static Sprite GetPlayerSprite(int index)
      {
         var sprites = Resources.LoadAll<Sprite>(SpritesPath + PlayerSpriteTmp);
         return sprites.FirstOrDefault(s => s.name == "player_" + index);
      }

      public static WeaponRegistriesData GetWeaponsRegistry()
      {
         var registry = Resources.LoadAll<WeaponRegistriesData>(WeaponsRegistryPath);
         return registry[0]; //todo only the first registry for now.
      }

      public static ItemEntity GetItemEntityTemplate()
      {
         var itemTemplate = Resources.Load<ItemEntity>(EntitiesPath + ItemEntityTemplate);
         return itemTemplate;
      }
      
      public static ItemEntity GetItemEntityWithData<T>(ItemData data) where T : ItemEntity
      {
         string itemPath = "";

         if (typeof(T) == typeof(ItemMeleeWeaponEntity))
            itemPath = MeeleeItemEntity;
         
         if (typeof(T) == typeof(ItemEntity))
            itemPath = ItemEntityPath;
         
         var itemTemplate = Resources.Load<T>(EntitiesPath + itemPath);
         itemTemplate.SetItemData(data);
         return itemTemplate;
      }
   }
}
