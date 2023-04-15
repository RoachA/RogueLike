using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Game.Entites;
using Game.Interfaces;
using Game.Managers;

namespace Game.Tiles
{
    [ExecuteAlways]
    public class TileBase : MonoBehaviour
    {
        [SerializeField] protected bool IsWalkable = true;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Vector2Int _tilePosId;
        [SerializeField] protected List<EntityBase> _entitiesOnTile;
        [SerializeField] protected List<TileTypeData> _tileTypeData;

        protected TileTypeData _currentTileData;
        protected bool _wasMarked; // this is used for internal room detection algorithms.
        protected bool _isVisibleToPlayer;
        
        public virtual void Init(ICoords coords, List<TileTypeData> data) 
        {
            /*
            Walkable = walkable;
            _renderer.color = walkable ? _walkableColor.Evaluate(Random.Range(0f, 1f)) : _obstacleColor;
            _defaultColor = _renderer.color;

            OnHoverTile += OnOnHoverTile;
            */
            SetTileTypeData(data);
            Coords = coords;
            _currentTileData = data[0];
        }

        protected virtual void SetRandomSprite()
        {
            if (_tileTypeData.Count < 1)
                return;
            
            _spriteRenderer.sprite = _tileTypeData[UnityEngine.Random.Range(0, _tileTypeData.Count)].TileSprite_A;
        }

        public Sprite GetSprite()
        {
            return _spriteRenderer.sprite;
        }
        
        protected virtual void SetSpriteWithIndex(int index)
        {
            if (index < 0 || index > _tileTypeData.Count)
                return;

            _spriteRenderer.sprite = _tileTypeData[index].TileSprite_A;
        }
        
        public virtual void SetTileTypeData(List<TileTypeData> data, int index = 0)
        {
            _tileTypeData = data;
            _currentTileData = _tileTypeData[index];
            SetSpriteWithIndex(index);
        }

        public virtual TileTypeData GetCurrentTileData()
        {
            return _currentTileData;
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
        
        protected virtual void Start()
        {
            //SetRandomSprite();
        }

        public bool GetMarkedState()
        {
            return _wasMarked;
        }

        public void SetMarkedState(bool state)
        {
            _wasMarked = state;
        }
        
        #region LIGHT
        
        public void SetLight(float lightVal)
        {
            var darkColor = 0.15f;
            var multiplier = 1 - 0.005f * lightVal;
            multiplier = multiplier < darkColor ? 0.3f : multiplier;
            float finalValue = 1;
            _isVisibleToPlayer = lightVal >= 1f;

            if (_isVisibleToPlayer)
                finalValue = multiplier;
            else
                finalValue = darkColor;

           
            
            var finalColor = new Color(finalValue, finalValue, finalValue, 1);
            _spriteRenderer.color = finalColor;
            
            foreach (var entity in _entitiesOnTile)
            {
                entity.SetLight(finalColor);
            }
            //_spriteRenderer.color = new Color(1 / lightVal, 1 / lightVal, 1 / lightVal, 1);
            //Debug.LogWarning(_tilePosId + " : " + lightVal);
        }

        public bool IsTileVisible()
        {
            return _isVisibleToPlayer;
        }
        
        #endregion

        #region PATHFINDING_STUFFS

        public ICoords Coords;
        private bool _selected;
        public List<TileBase> Neighbors;
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
        
        public List<TileBase> GetCardinalNeighbours()
        {
            var cardinalNeighbours = new List<TileBase>();

            foreach (var neighbor in Neighbors)
            {
                if (neighbor.GetTilePosId() == _tilePosId + Vector2.up)
                    cardinalNeighbours.Add(neighbor);
                if (neighbor.GetTilePosId() == _tilePosId + Vector2.down)
                    cardinalNeighbours.Add(neighbor);
                if (neighbor.GetTilePosId() == _tilePosId + Vector2.right)
                    cardinalNeighbours.Add(neighbor);
                if (neighbor.GetTilePosId() == _tilePosId + Vector2.left)
                    cardinalNeighbours.Add(neighbor);
            }
            
            return cardinalNeighbours;
        }
        
        public int CheckHowManyNeighboursAreOfType<T>() where T : TileBase
        {
            var count = 0;
            
            foreach (var neighbor in Neighbors)
            {
                if (neighbor is T)
                {
                    count++;
                }
            }

            return count;
        }

        public void CacheNeighbors() 
        {
            if (Neighbors != null)
                Neighbors.Clear();
            
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
        
        #endregion
        
        #region ENTITY_OPERATIONS
        
        public interface ICoords 
        {
            public float GetDistance(ICoords other);
            public Vector2Int Pos { get; set; }
        }
        
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
        
        public List<IInteractable> GetInteractables()
        {
            List<IInteractable> interactables = new List<IInteractable>();
            
            IInteractable interactable = GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                interactables.Add(interactable);
            }
      
            foreach (var entity in _entitiesOnTile)
            {
                IInteractable interactableEntity = entity.GetComponent<IInteractable>();
                if (interactableEntity != null)
                    interactables.Add(interactableEntity);
            }

            return interactables;
        }
        
        #endregion
    }
}

