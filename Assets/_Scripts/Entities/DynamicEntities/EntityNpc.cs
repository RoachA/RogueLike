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
      
      public bool CheckForAggro(TileBase targetTile)
      {
         var aggro = GetDistanceToTargetTile(targetTile) <= _aggroDistance;
         _aggroDebug = aggro;
         return aggro;
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

      private bool _aggroDebug;
      private Vector3 _targetLabelPos;
      private void OnDrawGizmos()
      {
         Gizmos.color = _aggroDebug ? new Color(1f, 0f, 0f, 0.15f) : new Color(0f, 1f, 0f, 0.1f);
         Gizmos.DrawSphere(transform.position, _aggroDistance / 2);
         Handles.Label(_targetLabelPos, _detectedDistance.ToString());

         Gizmos.color = Color.green;

         if (_pathNodes.Count == 0 || _pathNodes == null)
         {
           // Debug.LogWarning("entity cannot find a way to reach its destination!");
            return;
         }
         
         for (var i = 0; i < _pathNodes.Count; i++)
         {
            var index = i;
            var node = _pathNodes[i].GetTilePosId();
            Vector2Int nextNode = _pathNodes[_pathNodes.Count - 1].GetTilePosId();
            
            if (i < _pathNodes.Count - 1)
                nextNode = _pathNodes[index + 1].GetTilePosId();
            
            Debug.DrawLine(node.ConvertVectorToVector3(1), nextNode.ConvertVectorToVector3(0));
         }
      }
#endif
   }
}
