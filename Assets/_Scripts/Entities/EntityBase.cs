using System;
using System.Collections;
using System.Collections.Generic;
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
   
}
