using Game.Tiles;

namespace Game.Entites.Actions
{
    public sealed class MoveAction<T> : ActionsBase where T : EntityDynamic
    {
        public TileBase TargetTile;

        public MoveAction(T entity, TileBase targetTile)
        {
            ActionId = "Move";
            Actor = entity;
            TargetTile = targetTile;

            Do();
        }
        
        protected override void Do()
        {
            Actor.MoveEntityToTile(TargetTile);
            Actor.SetOccupiedTile(TargetTile);
            var floor = (TileFloor) TargetTile;
            floor.AddEntityToTile(Actor);
            base.Do();
        }
    }
}