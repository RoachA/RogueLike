using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Tiles;
using UnityEngine;
using Game.Entites;
using Game.Entites.Actions;
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
        [SerializeField] private DynamicEntityScriptableDataSet _entityScriptableRegistry;

        private GridManager _gridManager;

        void Awake()
        {
            Instance = this;
            _entityScriptableRegistry = DataManager.GetNpcRegistries();
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

        public DynamicEntityScriptableDataSet GetEntityRegistry()
        {
            return _entityScriptableRegistry;
        }

        public DynamicEntityScriptableData GetEntityDataWithIndex(int index)
        {
            return _entityScriptableRegistry._npcDefinitions[index];
        }

        private void SetSubscriptions()
        {
            EntityStatsView._entityDiesEvent += OnEntityDies;
            EntityStatsView._entityHPUpdatedEvent += OnEntityHpChanges;
        }
        private void ReleaseSubscriptions()
        {
            EntityStatsView._entityDiesEvent -= OnEntityDies;
            EntityStatsView._entityHPUpdatedEvent -= OnEntityHpChanges;
        }

        #region getters

        public EntityPlayer GetPlayerEntity()
        {
            return _player;
        }

        public void SetPlayerEntity(EntityPlayer player)
        {
            _player = player;
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

        public void PlayEntityOffense()
        {
            foreach (var entity in ReturnEntityList<EntityNpc>(false))
            {
                if (entity.GetAliveStatus() == false)
                    return;
                
                bool tryAttackPlayer = entity.GetDemeanor() == EntityDemeanor.hostile && entity.CheckForAggro(_player.GetOccupiedTile());

                bool canMeeleeAttack = entity.GetDistanceToTargetTile(_player.GetOccupiedTile()) <= 1.42f; //hypotenuse

                if (canMeeleeAttack)
                {
                    var Attack = new AttackAction<EntityDynamic>(entity, _player);
                    return;
                }

                if (tryAttackPlayer) //move towards player but shouldn't move actually.
                {
                    entity.GetPathToTarget(_player.GetOccupiedTile());
                    var action = new WalkAction<EntityNpc>(entity, entity._pathNodes[entity._pathNodes.Count - 1]);
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

        public void InstantiatePlayerEntity(Vector2Int pos, PlayerEntityData data)
        {
            if (_gridManager.CheckPosInBounds(pos.x, pos.y) == false) return;
            
            var entityResource = Resources.Load(ResourceManager.PlayerEntityPath) as GameObject;
            var newEntityObj = Instantiate(entityResource, transform);
            
            newEntityObj.transform.localPosition =
                new Vector3(pos.x, pos.y, 0); 
            
            var newEntity = newEntityObj.GetComponent<EntityPlayer>();
            var tile = _gridManager.GetTileAtPosition(pos);
            
            newEntity.SetEntityPos(tile);
            newEntity.Init(data.BaseStatsData, data.DefinitionData);
            newEntity.SetEntityType(EntityType.player);
            _entitiesList.Add(newEntity);
            SetPlayerEntity(newEntity);
        }
        
        //todo feed it with data here! entityData:!! since things are procedural we would pass rules here.
        // and select entity types depending on rules.
        public void InstantiateNpcEntity(Vector2Int pos, DynamicEntityScriptableData definition)
        {
            if (_gridManager.CheckPosInBounds(pos.x, pos.y) == false) return;
            
            var entityResource = Resources.Load(ResourceManager.NpcEntityPath) as GameObject;
            var newEntityObj = Instantiate(entityResource, transform);
            
            //todo check z's from a const or something.
            newEntityObj.transform.localPosition =
                new Vector3(pos.x, pos.y, 0); 

            var newEntity = newEntityObj.GetComponent<EntityDynamic>();
            
            var tile = _gridManager.GetTileAtPosition(pos);
            
            newEntity.SetEntityPos(tile);
            newEntity.Init(definition);
            newEntity.SetEntityType(EntityType.npc);
            _entitiesList.Add(newEntity);
            
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