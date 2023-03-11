using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Tiles;
using UnityEngine;
using Game.Entites;
using Game.Entites.Data;

namespace Game.Managers
{
    /// <summary>
    /// Entity manager holds: All entity data and references of the level.
    /// Entity manager handles: Entity spawning, Entity Removal, Getting Entities, Setting Entity Data,
    /// Updating Entity States.
    /// </summary>
    
    public class EntityManager : MonoBehaviour
    {
        //todo PATH FINDING LOGIC MUST WORK HERE -- maybe not.

        public static EntityManager Instance;

        [SerializeField] private List<EntityBase> _entitiesList;
        [SerializeField] private EntityPlayer _player;
        [SerializeField] private DynamicEntityDataSet _entityRegistry;

        private GridManager _gridManager;

        void Awake()
        {
            Instance = this;
            _entityRegistry = DataManager.GetNpcRegistries();
            Debug.Log("Entity registry is set.");
        }

        void Start()
        {
            _gridManager = GridManager.Instance;
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ReleaseSubscriptions();
        }

        public DynamicEntityDataSet GetEntityRegistry()
        {
            return _entityRegistry;
        }

        public DynamicEntityData GetEntityDataWithIndex(int index)
        {
            return _entityRegistry._npcDefinitions[index];
        }

        private void SetSubscriptions()
        {
            EntityStatsView._entityDiesEvent += OnEntityDies;
            EntityStatsView._entityHPUpdatedEvent += OnEntityHpChanges;
        }
        private void ReleaseSubscriptions()
        {
            EntityStatsView._entityDiesEvent -= OnEntityDies;
        }

        #region getters

        public EntityPlayer GetPlayerEntity()
        {
            return _player;
        }
        
        public List<EntityBase> GetAllEntities()
        {
            Debug.LogError(_entitiesList.Count);
            return _entitiesList;
        }
        
        public List<T> ReturnEntityList<T>(bool getInheritors) where T : EntityBase
        {
           return _entitiesList.Where(e => getInheritors ? e is T : e.GetType() == typeof(T)).Cast<T>().ToList();
        }

    
        public void DrawPathFromEntityToTargetTile(EntityBase entity, TileBase tile)
        {
            entity.FindPathToTargetTile(tile);
        }

        public void PlayEntityMoves()
        {
            foreach (var entity in ReturnEntityList<EntityNpc>(false))
            {
                if (entity.GetAliveStatus() == false)
                    return;
                
                bool tryAttackPlayer = entity.GetDemeanor() == EntityDemeanor.hostile && entity.CheckForAggro(_player.GetOccupiedTile());

                bool canMeeleeAttack = entity.GetDistanceToTargetTile(_player.GetOccupiedTile()) <= 1.42f; //hypotenuse

                if (canMeeleeAttack)
                {
                    Debug.LogError("HITS TO THE PLAYER!!");
                    return;
                }

                if (tryAttackPlayer) //move towards player
                {
                    entity.GetPathToTarget(_player.GetOccupiedTile());
                    entity.MoveEntityToTile(entity._pathNodes[entity._pathNodes.Count - 1]);
                }
            }
        }

        #endregion

        public void InitiateEntities<T>(List<T> entitylist) where T : EntityBase
        {
            
        }

        public EntityBase GetEntityWithIndex(int index)
        {
            return _entitiesList[index];
        }

        //todo feed it with data here! entityData:!! since things are procedural we would pass rules here.
        // and select entity types depending on rules.
        public void InstantiateDynamicEntity(EntityType entityType, Vector2Int pos, DynamicEntityData definition)
        {
            var entityResource = Resources.Load(ResourcesHelper.EntitiesPath) as GameObject;

            if (_gridManager.CheckPosInBounds(pos.x, pos.y) == false) return;

            switch (entityType)
            {
                case (EntityType.player):
                    entityResource = Resources.Load(ResourcesHelper.PlayerEntityPath) as GameObject;
                    if (_player != null)
                        return;
                    break;
                case (EntityType.npc):
                    entityResource = Resources.Load(ResourcesHelper.NpcEntityPath) as GameObject;
                    break;
            }

            var newEntityObj = Instantiate(entityResource, transform);
            
            //todo check z's from a const or something.
            newEntityObj.transform.localPosition =
                new Vector3(pos.x, pos.y, 0); 

            var newEntity = newEntityObj.GetComponent<EntityDynamic>();
            //newEntity.SetEntityData(_dynamicEntityDataSet._npcDefinitions[0]);
            var tile = _gridManager.GetTileAtPosition(pos);
            newEntity.SetEntityPos(tile);
            
            if (entityType == EntityType.npc)
                newEntity.Init(definition);
            
            newEntity.SetEntityType(entityType);
            _entitiesList.Add(newEntity);

            if (entityType == EntityType.player)
                _player = newEntity as EntityPlayer;

            if (_gridManager.GetTile(pos.x, pos.y).CheckIfWalkable(out var targetTile))
            {
                targetTile.AddEntityToTile(newEntity);
            }
        }

        #region EventListeners

        private void OnEntityDies(EntityDynamic entity)
        {
            Debug.Log(entity.name + " dies!");
            entity.SetAliveState(false);
        }

        private void OnEntityHpChanges(EntityDynamic entity, int hpChange)
        {
            if (hpChange > 0)
                Debug.Log(entity.name + " gains " + hpChange + " HP!");
            
            if (hpChange < 0)
                Debug.Log(entity.name + " loses " + hpChange + " HP!");
        }

        #endregion
    }
}