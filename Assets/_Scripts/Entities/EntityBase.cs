using System;
using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;

namespace Game.Entites
{
   [Serializable]
   public class EntityBase : MonoBehaviour
   {
      public enum EntityType
      {
         player,
         npc,
         item,
         container,
      }

      [SerializeField] protected SpriteRenderer _spriteRenderer;
      [SerializeField] protected TileBase _occupiedTile;
      [SerializeField] protected EntityType _entityType { get; set; }
      [SerializeField] private List<TileBase> _pathFindingPaths;

      [Sirenix.OdinInspector.ReadOnly] [SerializeField]
      protected Vector2Int _entityPos;

      public Vector2Int GetEntityPos()
      {
         return _entityPos;
      }

      public void SetEntityPos(TileBase targetTile)
      {
         _entityPos = targetTile.GetTilePosId();
         transform.localPosition = new Vector3(_entityPos.x, _entityPos.y, transform.localPosition.z);
         SetOccupiedTile(targetTile);
      }

      public void SetEntityType(EntityType entityType)
      {
         _entityType = entityType;
      }

      public TileBase GetOccupiedTile()
      {
         return _occupiedTile;
      }

      public void SetOccupiedTile(TileBase tile)
      {
         _occupiedTile = tile;
      }
      
      public virtual List<TileBase> FindPathToTargetTile(TileBase tile) 
      {
         _pathFindingPaths = Pathfinding.Pathfinding.FindPath(_occupiedTile, tile);
         return _pathFindingPaths;
      }
   }
}
