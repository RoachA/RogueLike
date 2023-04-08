using Game.Interfaces;

namespace Game.Entites
{
    public class PropEntity : EntityStatic, IInteractable
    {
        public PropEntityData Data;
        public string InteractionResultLog { get; set; }

        public void InitProp()
        {
            _spriteRenderer.sprite = Data.Sprite[0];
            _entityType = EntityType.item;
            _identifier = Data.Identifier;
        }
        
        public string InteractWithThis()
        {
            return "Volley.";
        }
    }
}
