using System.Collections.Generic;
using System.Linq;
using Game.Entites;
using Game.Entites.Data;
using Game.Managers;
using Game.Tiles;
using UnityEngine;

namespace Game.Data
{
   public static class DataManager
   {
      public static List<PropEntityData> PropsData;
      static DataManager()
      {
         GetNpcRegistries();
      }

      public static T GetTileResource<T>() where T : TileBase
      {
         Debug.Log("checking for type: " + typeof(T));
         var tiles = Resources.LoadAll<T>(ResourceHelper.TilesPath);
         T foundTile = null;

         foreach (var tile in tiles)
         {
            if (tile.GetType() == typeof(T))
               foundTile = tile;
         }

         if (foundTile == null)
         {
            Debug.LogError("could not find the tile asset in resources!");
         }

         return foundTile;
      }
      
      public static PlayerEntityData GenerateStarterPlayerData()
      {
         var newPlayerData = new PlayerEntityData();
         newPlayerData.BaseStatsData = newPlayerData.GenerateStarterStats();
         newPlayerData.DefinitionData = newPlayerData.GenerateStarterDefinition();

         return newPlayerData;
      }

      public static List<PropEntityData> GetAllPropData()
      {
         if (PropsData == null || PropsData.Count == 0)
            PropsData = Resources.LoadAll<PropEntityData>(ResourceHelper.PropsDataPath).ToList();

         if (PropsData.Count != 0)
            return PropsData;
         
         Debug.LogError("No prop entity data was found in its path");
         return null;
      }
      
      
      public static DynamicEntityScriptableDataSet GetNpcRegistries()
      {
         var entityResource = (Resources.LoadAll<DynamicEntityScriptableDataSet>(ResourceHelper.ScriptableNpcPath));

         if (entityResource != null)
            return entityResource[0];
         else
         {
            Debug.LogError("No registries found in: " + ResourceHelper.ScriptableNpcPath);
            return null;
         }
      }
      
      public static Sprite GetPlayerSprite(int index)
      {
         var sprites = Resources.LoadAll<Sprite>(ResourceHelper.SpritesPath + ResourceHelper.PlayerSpriteTmp);
         return sprites.FirstOrDefault(s => s.name == "player_" + index);
      }

      public static WeaponRegistriesData GetWeaponsRegistry()
      {
         var registry = Resources.LoadAll<WeaponRegistriesData>(ResourceHelper.WeaponsRegistryPath);
         return registry[0]; //todo only the first registry for now.
      }

      public static ItemEntity GetItemEntityTemplate()
      {
         var itemTemplate = Resources.Load<ItemEntity>(ResourceHelper.EntitiesPath + ResourceHelper.ItemEntityTemplate);
         return itemTemplate;
      }
      
      public static ItemEntity GetItemEntityWithData<T>(ItemData data) where T : ItemEntity
      {
         string itemPath = "";

         if (typeof(T) == typeof(ItemMeleeWeaponEntity))
            itemPath = ResourceHelper.MeeleeItemEntity;
         
         if (typeof(T) == typeof(ItemEntity))
            itemPath = ResourceHelper.ItemEntityPath;
         
         var itemTemplate = Resources.Load<T>(ResourceHelper.EntitiesPath + itemPath);
         itemTemplate.SetItemData(data);
         return itemTemplate;
      }
      
      public static PropEntity GetPropEntity()
      {
         var props = Resources.Load<PropEntity>(ResourceHelper.PropEntityPath);

         if (props != null)
            return props;

         Debug.LogError("No prob entity was found in resources!");
         return null;
      }

      public static List<WearableItemData> GetWearableItems()
      {
         var wearableItems = Resources.LoadAll<WearableItemData>(ResourceHelper.WearableItemsData).ToList();
         
         if (wearableItems.Count != 0)
            return wearableItems;
         
         Debug.LogError("No wearables were found!");
         return null;
      }
   }
}
