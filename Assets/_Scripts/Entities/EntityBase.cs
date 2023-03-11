using System;
using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;
using Game.Utils;

namespace Game.Entites
{
   [Serializable]
   public class EntityBase : MonoBehaviour
   {
      [SerializeField] protected String _identifier;
      [SerializeField] protected SpriteRenderer _spriteRenderer;
      [SerializeField] protected Sprite _corpseSprite;
      [SerializeField] protected TileBase _occupiedTile;
      [SerializeField] protected EntityType _entityType { get; set; }
      [SerializeField] private List<TileBase> _pathFindingPaths;

      [Sirenix.OdinInspector.ReadOnly]

      protected virtual void SetSprite(Sprite sprite)
      {
         _spriteRenderer.sprite = sprite;
      }

      public Vector2Int GetEntityPos()
      {
         return _occupiedTile.GetTilePosId();
      }

      public void SetEntityPos(TileBase targetTile)
      {
         transform.localPosition = targetTile.GetTilePosId().ConvertVectorToVector3(0);
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

      public void SetOccupiedTile(TileBase targetTile)
      {
         _occupiedTile = targetTile;
      }
      
      public virtual List<TileBase> FindPathToTargetTile(TileBase tile) 
      {
         _pathFindingPaths = Pathfinding.Pathfinding.FindPath(_occupiedTile, tile);
         return _pathFindingPaths;
      }
   }
}
