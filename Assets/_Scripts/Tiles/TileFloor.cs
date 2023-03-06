using UnityEngine;
using System.Collections.Generic;

namespace Game.Tiles
{
    public class TileFloor : TileBase
    {
        [SerializeField] protected List<EntityBase> _entitiesOnTile;

        public void AddEntityToTile(EntityBase entity)
        {
            _entitiesOnTile.Add(entity);
        }

        public void AddEntityListToTile(List<EntityBase> entities)
        {
            foreach (var entity in entities)
            {
                _entitiesOnTile.Add(entity);
            }
        }

        /// <summary>
        /// returns false if tile has no entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool QueryForEntities(out List<EntityBase> entities)
        {
            if (_entitiesOnTile.Count == 0)
            {
                entities = null;
                return false;
            }

            entities = _entitiesOnTile;
            return true;
        }

        public bool CheckIfHasNpc(out EntityNpc npc)
        {
            npc = null;
            
            if (_entitiesOnTile == null)
                return false;
            
            foreach (var entity in _entitiesOnTile)
            {
                if (entity.GetType() == typeof(EntityNpc))
                {
                    npc = (EntityNpc) entity;
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
                if (npc.CheckIfHostile())
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

