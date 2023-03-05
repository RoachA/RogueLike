using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    [SerializeField] private List<EntityBase> _entitiesList;
    [SerializeField] private EntityPlayer _player;

    private void Awake()
    {
        Instance = this;
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

    public void InstantiateEntity(EntityBase.EntityType entityType, Vector2 pos)
    {
        var entityResource = Resources.Load(ResourcesHelper.EntitiesPath) as GameObject;
        
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
        newEntityObj.transform.localPosition = pos;
        
        var newEntity = newEntityObj.GetComponent<EntityBase>();
        newEntity.SetEntityPos(pos);
        newEntity.SetEntityType(entityType);
        _entitiesList.Add(newEntity);

        if (entityType == EntityBase.EntityType.player)
            _player = newEntity as EntityPlayer;
    }
}