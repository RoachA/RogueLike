using Game.Entites.Data;
using Game.Tiles;
using UnityEditor;
using UnityEngine;
using Game.Utils;


namespace Game.Entites
{
   public class EntityNpc : EntityDynamic
   {
      [SerializeField] protected EntityDemeanor _demeanor;
      [SerializeField] protected EntityBehaviorTypes _behaviorType;
      [SerializeField] protected int _aggroDistance;
      
      protected NpcEntityScriptableData NpcScriptableData;
      protected bool _aggroStatus;

      #region debug_fields
      private readonly Color _hostileColor = Color.red;
      private readonly Color _nautralColor = Color.white;
      #endregion


      public override void Init(DynamicEntityScriptableData entityScriptableData)
      {
         base.Init(entityScriptableData);
         
         NpcScriptableData = entityScriptableData as NpcEntityScriptableData;

         if (NpcScriptableData == null)
         {
            Debug.LogError("Npc data was null!");
            return;
         }

         SetDemeanor(NpcScriptableData._demeanor);
         SetBehaviorType(NpcScriptableData._behaviorType);
         UpdateAggroDistance(NpcScriptableData._aggroRadius);
      }
      
      public EntityDemeanor GetDemeanor()
      {
         return _demeanor;
      }
      
      protected void SetDemeanor(EntityDemeanor demeanor)
      {
         _demeanor = demeanor;
         _spriteRenderer.color = _demeanor == EntityDemeanor.hostile ? _hostileColor : _nautralColor; // would be hidden laters or somethng.
      }

      protected void SetBehaviorType(EntityBehaviorTypes type)
      {
         _behaviorType = type;
      }

      protected void UpdateAggroDistance(int distance)
      {
         _aggroDistance = distance;
      }

      public bool GetCurrentAggroStatus()
      {
         return _aggroStatus;
      }
      
      public bool CheckForAggro(TileBase targetTile)
      {
         var aggroRadius = NpcScriptableData._aggroRadius;
         var aggro = GetDistanceToTargetTile(targetTile) <= aggroRadius &&
                     CheckIfDetectsWithRay(GetEntityPos(), targetTile.GetTilePosId(), aggroRadius);
         
         _aggroStatus = aggro;
         return aggro;
      }
      
      public static bool CheckIfDetectsWithRay(Vector2Int origin, Vector2Int target, float dist = 10)
      {
         RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, dist);
         Vector3 debugOrigin = new Vector3(origin.x, origin.y, 1);
         Vector3 debugTarget = new Vector3(target.x, target.y, 1) - debugOrigin;
         
         if (hit.collider.CompareTag("Player"))
         {
            Debug.DrawRay(debugOrigin, debugTarget, Color.green, 2);
            return true;
         }

         if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Door"))
         {
            Debug.DrawRay(debugOrigin, debugTarget, Color.red , 2);
            return false;
         }

         return false;
      }

      public override void SetAliveState(bool isAlive)
      {
         base.SetAliveState(isAlive);
         if (_isAlive == false)
            SetDemeanor(EntityDemeanor.natural);
      }
      
#if UNITY_EDITOR
      private void OnValidate()
      {
         GetDemeanor();
      }
      
      private Vector3 _targetLabelPos;
      private void OnDrawGizmos()
      {
         Gizmos.color = _aggroStatus ? new Color(1f, 0f, 0f, 0.15f) : new Color(0f, 1f, 0f, 0.1f);
         Gizmos.DrawSphere(transform.position, NpcScriptableData._aggroRadius / 2);
         Handles.Label(_targetLabelPos, _detectedDistance.ToString());

         Gizmos.color = Color.green;
         
         if (_pathNodes == null)
            return;

         if (_pathNodes.Count == 0)
         {
           // Debug.LogWarning("entity cannot find a way to reach its destination!");
            return;
         }
         
         /*for (var i = 0; i < _pathNodes.Count; i++)
         {
            var index = i;
            var node = _pathNodes[i].GetTilePosId();
            Vector2Int nextNode = _pathNodes[_pathNodes.Count - 1].GetTilePosId();
            
            if (i < _pathNodes.Count - 1)
                nextNode = _pathNodes[index + 1].GetTilePosId();
            
            Debug.DrawLine(node.ConvertVectorToVector3(1), nextNode.ConvertVectorToVector3(0));
         }*/
      }
#endif
   }
}
