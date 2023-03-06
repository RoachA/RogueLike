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
   [Sirenix.OdinInspector.ReadOnly] [SerializeField] protected Vector2Int _entityPos;

   public Vector2Int GetEntityPos()
   {
      return _entityPos;
   }

   public void SetEntityPos(Vector2Int newPos)
   {
      _entityPos = newPos;
      transform.localPosition = new Vector3(_entityPos.x, _entityPos.y, transform.localPosition.z);
   }

   public void SetEntityType(EntityType entityType)
   {
      _entityType = entityType;
   }
}
