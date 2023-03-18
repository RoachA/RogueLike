using Game.Tiles;

namespace Game.Entites.Actions
{
    public sealed class WalkAction<T> : ActionsBase where T : EntityDynamic
    {
        public TileBase TargetTile;

        public WalkAction(T entity, TileBase targetTile)
        {
            ActionId = "Walk";
            ActionVerb = "walks through";
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
            
            Target = TargetTile;
            base.Do();
        }
    }
}