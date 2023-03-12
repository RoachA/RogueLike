using Game.Tiles;

namespace Game.Entites.Actions
{
    public sealed class MoveAction<T> : ActionsBase where T : EntityDynamic
    {
        public T Entity;
        public TileBase TargetTile;

        public MoveAction(T entity, TileBase targetTile)
        {
            ActionId = "Move";
            Entity = entity;
            TargetTile = targetTile;

            Do();
        }
        
        protected override void Do()
        {
            Entity.MoveEntityToTile(TargetTile);
            Entity.SetOccupiedTile(TargetTile);
            Entity.SetOccupiedTile(TargetTile);
            base.Do();
        }
    }
}