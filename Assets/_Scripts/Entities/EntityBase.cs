using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class EntityBase : MonoBehaviour
{
   public enum EntityType
   {
      player,
      npc,
      item,
      container,
   }

   [SerializeField] protected EntityType _entityType { get; set; }
   [Sirenix.OdinInspector.ReadOnly] [SerializeField] protected Vector2 _entityPos;

   public Vector2 GetEntityPos()
   {
      return _entityPos;
   }

   public void SetEntityPos(Vector2 newPos)
   {
      _entityPos = newPos;
      transform.localPosition = newPos;
   }

   public void SetEntityType(EntityType entityType)
   {
      _entityType = entityType;
   }
}
