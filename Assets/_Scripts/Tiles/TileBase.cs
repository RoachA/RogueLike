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
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Vector2Int _tilePosId;

        protected virtual void SetRandomSprite()
        {
            if (_spriteVariants.Count < 1)
                return;
            
            _spriteRenderer.sprite = _spriteVariants[Random.Range(0, _spriteVariants.Count)];
        }

        protected virtual void SetTilePosId()
        {
            Vector3 thisPos = transform.position;
            _tilePosId.x = (int) thisPos.x;
            _tilePosId.y = (int) thisPos.y;
        }

        public Vector2Int GetTilePosId()
        {
            return _tilePosId;
        }

        protected virtual void Start()
        {
            SetRandomSprite();
            SetTilePosId();
        }
    }
}

