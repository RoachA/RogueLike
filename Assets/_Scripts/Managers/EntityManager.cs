using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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