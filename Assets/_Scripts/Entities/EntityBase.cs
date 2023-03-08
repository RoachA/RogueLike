using System;
using System.Collections.Generic;
using Game;
using Game.Tiles;
using Unity.VisualScripting;
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

      public TileBase GetOcccupiedTile()
      {
         return _occupiedTile;
      }

      public void SetOccupiedTile(TileBase tile)
      {
         _occupiedTile = tile;
      }

      public virtual List<TileBase>
         FindPathToTargetTile(
            TileBase tile) //todo this would be handled better on higher level. designed path for the entity there then move it.
      {
         var path = Pathfinding.FindPath(_occupiedTile, tile);
         return path;
      }
   }
}
