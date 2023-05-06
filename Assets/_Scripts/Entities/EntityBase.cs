using System;
using Game.Data;
using Game.Tiles;
using Game.UI;
using UnityEngine;
using Game.Utils;

namespace Game.Entities
{
   [Serializable]
   public class EntityBase : MonoBehaviour, IEquatable<EntityBase>, ILookable
   {
      public Guid Id { get; set; }
      [SerializeField] protected string _identifier; //todo not used mostly
      [SerializeField] protected SpriteRenderer _spriteRenderer;
      [SerializeField] protected TileBase _occupiedTile;
      [SerializeField] protected EntityType _entityType { get; set; }
      public LookableType MyLookableType { get; set; }

      protected void Start()
      {
         Init();
      }

      public virtual void Init(Guid guid = default)
      {
         if (Id == Guid.Empty)
         {
            if (guid == default)
               GenerateHashId();
            else
               Id = guid;
         }
      }
      
      public virtual void Init(ScriptableItemData data, Guid guid = default)
      {
         if (Id == Guid.Empty)
         {
            if (guid == default)
               GenerateHashId();
            else
               Id = guid;
         }
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
