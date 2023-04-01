using System;
using System.Collections.Generic;
using Game.Entites.Actions;
using Game.Entites.Data;
using Game.Tiles;
using UnityEditor;
using UnityEngine;

namespace Game.Entites
{
    //todo STUDY INTERFACES IMPLEMENT I=DAMAGABLE, 
    public abstract class EntityDynamic : EntityBase
    {
        protected EntityInventoryView _inventoryView;
        [SerializeField] protected EntityStatsView _statsView;
        [SerializeField] public DynamicEntityFeedbacks _feedbacks; //todo laters better be instantiated on start
        [SerializeField] protected bool _isAlive;

        public List<TileBase> _pathNodes;

        protected int _detectedDistance; //debug
        protected DynamicEntityScriptableData EntityScriptableData;

        protected void Start()
        {
            MeleeAttackAction<EntityDynamic>.DamageDealtEvent += OnReceiveDamage;
            MeleeAttackAction<EntityDynamic>.AttackHappenedEvent += OnAttack;
        }

        protected void OnDestroy()
        {
            RemoveListeners();
        }

        public virtual void Init(DynamicEntityScriptableData entityScriptableData) //for npc
        {
            Debug.Log(gameObject.name + " was initialized!");

            _inventoryView = GetComponent<EntityInventoryView>() == false
                ? gameObject.AddComponent<EntityInventoryView>()
                : GetComponent<EntityInventoryView>();

            _statsView = GetComponent<EntityStatsView>() == false
                ? gameObject.AddComponent<EntityStatsView>()
                : GetComponent<EntityStatsView>();

            EntityScriptableData = entityScriptableData;
            _statsView.SetData(entityScriptableData._dynamicEntityStatsData.BaseStats,
                entityScriptableData._dynamicEntityDefinitionData);

            SetSprite(entityScriptableData._dynamicEntityDefinitionData.Sprite);
        }

        public virtual void Init(BaseStatsData stats, DynamicEntityDefinitionData definition) //for player
        {
            Debug.Log(gameObject.name + " was initialized!");
            _inventoryView = GetComponent<EntityInventoryView>() == false
                ? gameObject.AddComponent<EntityInventoryView>()
                : GetComponent<EntityInventoryView>();

            _statsView = GetComponent<EntityStatsView>() == false
                ? gameObject.AddComponent<EntityStatsView>()
                : GetComponent<EntityStatsView>();

            _statsView.SetData(stats, definition);
            SetSprite(definition.Sprite);
        }

        #region Movement----------------------------------------------------------------

        public virtual void MoveEntityToTile(TileBase targetTile)
        {
            var targetPosV3 = new Vector3(targetTile.GetTilePosId().x, targetTile.GetTilePosId().y, 0);
            transform.localPosition = targetPosV3;
            SetEntityPos(targetTile);
            _occupiedTile = targetTile;
        }

        public int GetDistanceToTargetTile(TileBase targetTile)
        {
            var distance = (int) Vector2Int.Distance(_occupiedTile.GetTilePosId(), targetTile.GetTilePosId());
            _detectedDistance = distance;
            return distance;
        }

        public void GetPathToTarget(TileBase targetTile)
        {
            _pathNodes.Clear();
            _pathNodes = Pathfinding.Pathfinding.FindPath(_occupiedTile, targetTile);
        }
        
        #endregion

        #region get-set----------------------------------------------------------------

        public BaseStatsData GetStats()
        {
            return _statsView.GetBaseStats();
        }

        public DynamicEntityDefinitionData GetDefinitionData()
        {
            return _statsView._definition;
        }

        public EntityInventoryView GetInventoryView()
        {
            return _inventoryView;
        }

        public Dictionary<EntityEquipSlots, ItemEntity> GetEquippedItems()
        {
            return _inventoryView.GetEquippedItems();
        }

        public ItemMeleeWeaponEntity[] GetEquippedWeapons()
        {
            return _inventoryView.GetEquippedWeapons();
        }

        public bool GetAliveStatus()
        {
            return _isAlive;
        }

        public virtual void SetAliveState(bool isAlive)
        {
            _isAlive = isAlive;
            _spriteRenderer.sprite = _corpseSprite;
        }

        #endregion

        #region EventListeners

        public void OnReceiveDamage(EntityDynamic entity, int dmg) //todo define critical and pass here laters
        {
            bool isAlive = true;
            
            if (entity.gameObject == gameObject)
            {
                isAlive = _statsView.AddHp(-dmg);
                _feedbacks.OnReceiveDamage();
            }

            if (isAlive == false)
            {
               RemoveListeners();
            }
        }
        
        private void OnAttack(EntityDynamic entity, Vector2Int targetPos)
        {
            if (entity.gameObject == gameObject)
            {
                _feedbacks.AttackActionAnim(new Vector3(targetPos.x, targetPos.y, 0));
            }
        }

        private void RemoveListeners()
        {
            MeleeAttackAction<EntityDynamic>.DamageDealtEvent -= OnReceiveDamage;
            MeleeAttackAction<EntityDynamic>.AttackHappenedEvent -= OnAttack;  
        }
        
        #endregion
    }
}

