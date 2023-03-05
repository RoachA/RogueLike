using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    [SerializeField] private List<EntityBase> _entitiesList;

    private void Awake()
    {
        Instance = this;
    }

    public void InitiateEntities()
    {
    }

    public void InstantiateEntity(EntityBase.EntityType entityType, Vector2 pos)
    {
        _entitiesList = new List<EntityBase>();
        Debug.LogError("Entity was called for instantiation");

        var entityObj = Resources.Load(ResourcesHelper.PlayerEntityPath) as GameObject;
        var newEntity = Instantiate(entityObj, transform);
        newEntity.transform.localPosition = pos;
        _entitiesList.Add(newEntity.GetComponent<EntityBase>());
    }
}