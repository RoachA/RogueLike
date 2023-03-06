using System;
using System.Collections.Generic;
using System.Linq;
using Game.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    [SerializeField] private List<EntityBase> _entitiesList;
    [SerializeField] private EntityPlayer _player;

    private GridManager _gridManager;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gridManager = GridManager.Instance;
    }

    #region getters
    
    public EntityPlayer GetPlayerEntity()
    {
        return _player;
    }

    [Button]
    public List<EntityBase> GetAllEntities()
    {
        Debug.LogError(_entitiesList.Count);
        return _entitiesList;
    }

    [Button]
    public List<EntityDynamic> GetAllDynamicEntities()
    {
        var dynamicEntities = _entitiesList.Cast<EntityDynamic>().ToList();
        Debug.LogError(dynamicEntities.Count);
        return dynamicEntities;
    }
    
    [Button]
    public List<EntityNpc> GetAllNpcEntities()
    {
        var npcEntities = new List<EntityNpc>();

        foreach (var entity in _entitiesList)
        {
            if (entity.GetType() == typeof(EntityNpc))
                npcEntities.Add(entity as EntityNpc);
        }
        
        Debug.LogError(npcEntities.Count);
        return npcEntities;
    }
    
    #endregion 
    
    public void InitiateEntities()
    {
    }

    //todo feed it with data here! entityData:!!
    public void InstantiateEntity(EntityBase.EntityType entityType, Vector2Int pos)
    {
        var entityResource = Resources.Load(ResourcesHelper.EntitiesPath) as GameObject;
        
        //todo check if entity can be spawned on this tile - does the tile exist?
        
        switch (entityType)
        {
            case (EntityBase.EntityType.player):
                entityResource = Resources.Load(ResourcesHelper.PlayerEntityPath) as GameObject;
                if (_player != null)
                    return;
                break;
            case (EntityBase.EntityType.npc):
                entityResource = Resources.Load(ResourcesHelper.NpcEntityPath) as GameObject;
                break;
        }
        
        var newEntityObj = Instantiate(entityResource, transform);
        newEntityObj.transform.localPosition = new Vector3(pos.x, pos.y, 0); //todo check z's from a const or something.
        
        var newEntity = newEntityObj.GetComponent<EntityBase>();
        newEntity.SetEntityPos(pos);
        newEntity.SetEntityType(entityType);
        _entitiesList.Add(newEntity);

        if (entityType == EntityBase.EntityType.player)
            _player = newEntity as EntityPlayer;
        
        if (_gridManager.GetTile(pos.x, pos.y).CheckIfWalkable(out var targetTile))
        {
            targetTile.AddEntityToTile(newEntity);
        }
    }
}