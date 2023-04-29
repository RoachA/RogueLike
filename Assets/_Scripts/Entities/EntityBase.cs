using System;
using Game.Tiles;
using UnityEngine;
using Game.Utils;

namespace Game.Entites
{
   [Serializable]
   public class EntityBase : MonoBehaviour, IEquatable<EntityBase>
   {
      public Guid Id { get; set; }
      [SerializeField] protected string _identifier;
      [SerializeField] protected SpriteRenderer _spriteRenderer;
      [SerializeField] protected TileBase _occupiedTile;
      [SerializeField] protected EntityType _entityType { get; set; }

      protected void Start()
      {
         Init();
      }

      public void Init()
      {
         if (Id == Guid.Empty)
            GenerateHashId();
      }

      public Sprite GetSprite()
      {
         return _spriteRenderer.sprite;
      }

      protected virtual void SetSprite(Sprite sprite)
      {
         _spriteRenderer.sprite = sprite;
      }

      public virtual void SetLight(Color color)
      {
         _spriteRenderer.color = color;
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

      protected void GenerateHashId()
      {
         Id = Guid.NewGuid();
      }

      public bool Equals(EntityBase other)
      {
         if (other == null) return false;
         return (this.Id == other.Id);
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
   }
}
