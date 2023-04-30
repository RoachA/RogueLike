using UnityEngine;
using Game.Entities;

namespace Game.Tiles
{
    public class TileFloor : TileBase
    {
        public bool CheckIfHasNpc(out EntityNpc npc)
        {
            npc = null;
            
            if (_entitiesOnTile == null)
                return false;
            
            foreach (var entity in _entitiesOnTile)
            {
                if (entity.GetType() == typeof(EntityNpc))
                {
                    npc = (EntityNpc) entity.Value;
                    Debug.Log("tile : " + _tilePosId + " has an NPC a" + npc.name + " on it!");
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// if it finds an npc on the tile and if it is hostile, outs the npc and returns true.
        /// </summary>
        /// <param name="hostileNpc"></param>
        /// <returns></returns>
        public bool CheckIfHasEnemy(out EntityNpc hostileNpc)
        {
            if (CheckIfHasNpc(out EntityNpc npc))
            {
                if (npc.GetDemeanor() == EntityDemeanor.hostile)
                {
                    Debug.Log(npc.name + " is hostile!");
                    hostileNpc = npc;
                    return true;
                }
                
                Debug.Log(npc.name + " is neutral!");
            }
            
            hostileNpc = null;
            return false;
        }
    }
} 

