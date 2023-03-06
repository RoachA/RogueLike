using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game.Tiles
{
    [ExecuteAlways]
    public class TileBase : MonoBehaviour
    {
        [SerializeField] protected bool IsWalkable = true;
        [SerializeField] protected List<Sprite> _spriteVariants;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Vector2Int _tilePosId;

        protected virtual void SetRandomSprite()
        {
            if (_spriteVariants.Count < 1)
                return;
            
            _spriteRenderer.sprite = _spriteVariants[Random.Range(0, _spriteVariants.Count)];
        }

        public virtual void SetTilePosId(int x, int y)
        {
            _tilePosId.x = x;
            _tilePosId.y = y;
        }

        public virtual bool CheckIfWalkable(out TileFloor floor)
        {
            floor = IsWalkable ? (TileFloor) this : null;
            return IsWalkable;
        }

        public virtual bool CheckIfWalkable()
        {
            return IsWalkable;
        }

        public Vector2Int GetTilePosId()
        {
            return _tilePosId;
        }

        protected virtual void Start()
        {
            SetRandomSprite();
        }
    }
}

