using System;
using UnityEngine;

public class EntityNpc : EntityDynamic
{
   [SerializeField] protected bool _isHostile = false;
   
   #region debug_fields
   private readonly Color _hostileColor = Color.red;
   private readonly Color _nautralColor = Color.white;
   #endregion

   public bool CheckIfHostile()
   {
      _spriteRenderer.color = _isHostile ? _hostileColor : _nautralColor;
      return _isHostile;
   }

   public void SwapHostility()
   {
      _isHostile = !_isHostile;
   }

#if UNITY_EDITOR
   private void OnValidate()
   {
      CheckIfHostile();
   }
#endif
}
