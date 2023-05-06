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
    
            if (item.GetType() == typeof(ItemEntityView))
            {
                ItemEntityView container = item as ItemEntityView;
                if (container == null)
                    return default;
        
                returnData = container._itemData as T;
            }
    
            if (returnData == null)
                Debug.LogError($"{typeof(T).Name} could not be read!");

            return returnData;
        }

        public static T GetActorData<T>(ILookable actor) where T : DynamicEntityScriptableData
        {
            T returnData = default;

            if (actor.GetType() == typeof(EntityDynamic))
            {
                EntityDynamic container = actor as EntityDynamic;

                if (container == null)
                    return default;

                returnData = container.GetEntityData() as T;
            }
            
            if (returnData == null)
                Debug.LogError($"{typeof(T).Name} could not be read!");

            return returnData;
        }

        public static T GetTileData<T>(ILookable tile) where T : TileTypeData
        {
            T returnData = default;
            
            if (tile.GetType() == typeof(TileBase))
            {
                TileBase container = tile as TileBase;

                if (container == null)
                    return default;

                returnData = container.GetCurrentTileData() as T;
            }
            
            if (returnData == null)
                Debug.LogError($"{typeof(T).Name} could not be read!");

            return returnData;
        }
    }
}
