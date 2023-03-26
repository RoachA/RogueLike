using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Game.Managers;
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
        
        [SerializeField] protected string _tileTypeName; // todo later on will be read from data.

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

        public virtual void SetWalkable(bool isWalkable)
        {
            IsWalkable = isWalkable;
        }

        public virtual Vector2Int GetTilePosId()
        {
            return _tilePosId;
        }
        
        public virtual string GetTileType()
        {
            return _tileTypeName;
        }

        protected virtual void Start()
        {
            SetRandomSprite();
        }

#region PATHFINDING_STUFFS

        public ICoords Coords;
        private bool _selected;
        public List<TileBase> Neighbors { get; protected set; }
        public float GetDistance(TileBase other) => Coords.GetDistance(other.Coords);
        public TileBase Connection { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F => G + H;
        

        private static readonly List<Vector2Int> Dirs = new List<Vector2Int>() 
        {
            new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1)
        };
        
        public virtual void Init(ICoords coords) 
        {
            /*
            Walkable = walkable;
            _renderer.color = walkable ? _walkableColor.Evaluate(Random.Range(0f, 1f)) : _obstacleColor;
            _defaultColor = _renderer.color;

            OnHoverTile += OnOnHoverTile;
            */

            Coords = coords;
        }

        public void CacheNeighbors() 
        {
            Neighbors = new List<TileBase>();

            foreach (var tile in Dirs.Select(dir => GridManager.Instance.GetTileAtPosition(Coords.Pos + dir)).Where(tile => tile != null)) 
            {
                Neighbors.Add(tile);
            }
        }
        
        public void SetConnection(TileBase nodeBase) 
        {
            Connection = nodeBase;
        }

        public void SetG(float g) 
        {
            G = g;
            SetText();
        }

        public void SetH(float h) 
        {
            H = h;
            SetText();
        }

        private void SetText() 
        {
            if (_selected) return;
        }

        public void SetColor(Color color) => _spriteRenderer.color = color;

        public void RevertTile() 
        {
            _spriteRenderer.color = Color.white;
        }

        
        [Serializable]
        public struct TileCoords : ICoords
        {
            public float GetDistance(ICoords other)
            {
                var dist = new Vector2Int(Mathf.Abs((int) Pos.x - (int) other.Pos.x),
                    Mathf.Abs((int) Pos.y - (int) other.Pos.y));

                var lowest = Mathf.Min(dist.x, dist.y);
                var highest = Mathf.Max(dist.x, dist.y);

                var horizontalMovesRequired = highest - lowest;

                return lowest * 14 + horizontalMovesRequired * 10;
            }

            public Vector2Int Pos { get; set; }
        }

        public interface ICoords 
        {
            public float GetDistance(ICoords other);
            public Vector2Int Pos { get; set; }
        }
        
#endregion
    }
}

