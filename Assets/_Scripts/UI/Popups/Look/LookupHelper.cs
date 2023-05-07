using Game.Data;
using Game.Entities;
using Game.Entities.Data;
using UnityEngine;
using TileBase = Game.Tiles.TileBase;

namespace Game.UI.Helper
{
    public static class LookupHelper
    {
        public static T GetItemData<T>(ILookable item) where T : ScriptableItemData
        {
            T returnData = default;
    
            if (item is ItemEntityView container)
            {
                returnData = container._itemData as T;
            }
    
            if (returnData == null)
                Debug.LogError($"{typeof(T).Name} could not be read!");

            return returnData;
        }

        public static T GetActorData<T>(ILookable actor) where T : DynamicEntityScriptableData
        {
            T returnData = default;

            if (actor is EntityDynamic container)
            {
                returnData = container.GetEntityData() as T;
            }
            
            if (returnData == null)
                Debug.LogError($"{typeof(T).Name} could not be read!");

            return returnData;
        }

        public static TileTypeData GetTileData(ILookable tile)
        {
            TileTypeData returnData = default;

            if (tile is TileBase container)
            {
                returnData = container.GetCurrentTileData();
            }

            if (returnData == default)
            {
                Debug.LogError(tile.MyLookableType + " could not be read!");
            }

            return returnData;
        }

        public static PropEntityData GetPropData(ILookable prop)
        {
            PropEntityData returnData = default;
            
            if (prop is PropEntity container)
            {
                returnData = container.Data;
            }

            if (returnData == default)
            {
                Debug.LogError(prop.MyLookableType + " could not be read!");
            }

            return returnData;
        }
    }
}
