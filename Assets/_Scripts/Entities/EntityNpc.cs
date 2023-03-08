using Game.Tiles;
using UnityEditor;
using UnityEngine;

namespace Game.Entites
{
   public class EntityNpc : EntityDynamic
   {
      public enum EntityDemeanor
      {
         docile = 0,
         natural = 1,
         hostile = 2,
         friendly = 3,
      }

      [SerializeField] protected EntityDemeanor _demeanor;
      [SerializeField] protected int _aggroDistance;
      
      #region debug_fields

      private readonly Color _hostileColor = Color.red;
      private readonly Color _nautralColor = Color.white;

      #endregion

      public bool CheckIfHostile()
      {
         bool isHostile = _demeanor == EntityDemeanor.hostile;
         _spriteRenderer.color = isHostile ? _hostileColor : _nautralColor;
         return isHostile;
      }

      protected void SetDemeanor(EntityDemeanor demeanor)
      {
         _demeanor = demeanor;
      }
      
      public bool CheckForAggro(TileBase targetTile)
      {
         var aggro = GetDistanceToTargetTile(targetTile) <= _aggroDistance;
         Debug.LogError("Aggro? " + aggro);
         _aggroDebug = aggro;
         return aggro;
      }

      public override void SetEntityData(DynamicEntityData data)
      {
         _aggroDistance = data._aggroRadius;
         _demeanor = data._demeanor;
         base.SetEntityData(data);
      }

#if UNITY_EDITOR
      private void OnValidate()
      {
         CheckIfHostile();
      }

      private bool _aggroDebug;
      private Vector3 _targetLabelPos;
      private void OnDrawGizmos()
      {
         Gizmos.color = _aggroDebug ? new Color(1f, 0f, 0f, 0.15f) : new Color(0f, 1f, 0f, 0.1f);
         Gizmos.DrawSphere(transform.position, _aggroDistance / 2);
         Handles.Label(_targetLabelPos, _detectedDistance.ToString());
      }
#endif
   }
}
