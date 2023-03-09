using System;
using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;

namespace Game.Entites
{
    public class EntityDynamic : EntityBase
    {
        [SerializeField] protected bool _isAlive;
        
        [Header("Main Stats")]
        [SerializeField] protected int _hp = 10;
        [SerializeField] protected int _energy = 10;
        
        [Header("Stats")]
        [SerializeField] protected int _str = 10;
        [SerializeField] protected int _agi = 10;
        [SerializeField] protected int _int = 10;
        [SerializeField] protected int _chr = 10;
        [SerializeField] protected int _wp = 10;

        public List<TileBase> _pathNodes;
        
        protected int _detectedDistance; //debug
        
        public virtual void MoveEntityToDirection(Vector2Int direction)
        {
            var targetVector = new Vector3(direction.x, direction.y, transform.localPosition.z);
            var newPos = transform.localPosition + targetVector;
            transform.localPosition = newPos;
            //todo check how to do this better.
        }

        public virtual void MoveEntityToTile(TileBase targetTile)
        {
            var targetPosV3 = new Vector3(targetTile.GetTilePosId().x, targetTile.GetTilePosId().y, 0);
            transform.localPosition = targetPosV3;
            SetEntityPos(targetTile);
            _occupiedTile = targetTile;
        } 

        public int GetDistanceToTargetTile(TileBase targetTile)
        {
            Debug.DrawLine(new Vector3(targetTile.GetTilePosId().x, targetTile.GetTilePosId().y, 1), transform.position, Color.green, 5);
            var distance = (int) Vector2Int.Distance(_occupiedTile.GetTilePosId(), targetTile.GetTilePosId());
            _detectedDistance = distance;
            return distance;
        }
        
        public void GetPathToTarget(TileBase targetTile)
        {
            _pathNodes.Clear();
            _pathNodes = Pathfinding.Pathfinding.FindPath(_occupiedTile, targetTile);
        }

        public virtual void SetEntityData(DynamicEntityData data)
        {
            _hp = data._hp;
            _energy = data._energy;
            _str = data._str;
            _agi = data._agi;
            _int = data._int;
            _chr = data._chr;
            _wp = data._wp;
        }
    }
}
