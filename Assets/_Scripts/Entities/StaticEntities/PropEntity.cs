using Game.Interfaces;
using UnityEngine;

namespace Game.Entites
{
    public class PropEntity : EntityStatic, IInteractable
    {
        public PropEntityData Data;
        public string InteractionResultLog { get; set; }

        private PointLightView _pointLightView;

        public void InitProp()
        {
            _spriteRenderer.sprite = Data.Sprite[0];
            _entityType = EntityType.item;
            _identifier = Data.Identifier;

            _pointLightView = GetComponentInChildren<PointLightView>();
        }

        public override void SetLight(Color color)
        {
            base.SetLight(color);
            bool isActive = color.r > 0.5f; // this depends on shadow casting's color bottom limit. hard coded now.
            
           if (_pointLightView != null)
               _pointLightView.SetLightState(isActive);
        }

        public string InteractWithThis()
        {
            return "Volley.";
        }
    }
}
